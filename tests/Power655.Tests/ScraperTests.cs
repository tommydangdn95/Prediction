using Microsoft.Extensions.Logging.Abstractions;
using Power655.Core;
using Power655.Scraper;
using static Power655.Tests.TestData;

namespace Power655.Tests;

/// <summary>Fixture là HTML thật lấy ngày 2026-11-07 (đã cắt gọn).</summary>
public class ParserTests
{
    [Fact]
    public void Vietlott_table_parses_8_draws_matching_seed()
    {
        var result = VietlottHtmlParser.Parse(File.ReadAllText(Fixture("vietlott_page1.html")), P);

        result.Errors.Should().BeEmpty();
        result.Draws.Should().HaveCount(8);
        var first = result.Draws[0];
        first.DrawId.Should().Be(1420);
        first.Date.Should().Be(new DateOnly(2026, 11, 5));
        first.Numbers.Should().Equal(6, 20, 34, 41, 49, 52);
        first.Special.Should().Be(36);
        first.Source.Should().Be("vietlott");

        var seed = Seed.ToDictionary(d => d.DrawId);
        result.Draws.Should().OnlyContain(d => seed[d.DrawId].HasSameResultAs(d) && seed[d.DrawId].Date == d.Date);
    }

    [Fact]
    public void MinhNgoc_blocks_parse_with_jackpots()
    {
        var result = MinhNgocHtmlParser.Parse(File.ReadAllText(Fixture("minhngoc_page1.html")), P);

        result.Errors.Should().BeEmpty();
        result.Draws.Select(d => d.DrawId).Should().Equal(1420, 1419, 1418);
        var first = result.Draws[0];
        first.Date.Should().Be(new DateOnly(2026, 11, 5));
        first.Numbers.Should().Equal(6, 20, 34, 41, 49, 52);
        first.Special.Should().Be(36);
        first.Jackpot1.Should().Be(42_432_448_800);
        first.Jackpot2.Should().Be(5_618_647_400);
        first.Source.Should().Be("minhngoc");

        var seed = Seed.ToDictionary(d => d.DrawId);
        result.Draws.Should().OnlyContain(d => seed[d.DrawId].HasSameResultAs(d));
    }

    [Fact]
    public void Malformed_row_is_skipped_with_error()
    {
        const string html = """
            <table><tr><td>05/11/2026</td><td><a>01420</a></td>
            <td><span class="bong_tron">06</span><span class="bong_tron">06</span></td></tr></table>
            """;
        var result = VietlottHtmlParser.Parse(html, P);
        result.Draws.Should().BeEmpty();
        result.Errors.Should().ContainSingle();
    }

    [Fact]
    public void Ajax_key_is_extracted_from_page_script()
    {
        VietlottHtmlParser.ExtractAjaxKey("x = Game655CompareWebPart.ServerSideDrawResult(RenderInfo, 'cae0f881', GameDrawId")
            .Should().Be("cae0f881");
        VietlottHtmlParser.ExtractAjaxKey("<html></html>").Should().BeNull();
    }
}

public class FetchServiceTests
{
    private static readonly DateOnly Day = new(2026, 11, 5);

    private static Draw V(int id, int special, params int[] n) => new(P, id, Day, n, special, source: DrawSources.Vietlott);
    private static Draw M(int id, int special, params int[] n) => new(P, id, Day, n, special, 100, 50, DrawSources.MinhNgoc);

    private static DrawFetchService Service(IDrawSource primary, IDrawSource fallback) =>
        new(primary, fallback, NullLogger<DrawFetchService>.Instance, new FetchOptions { PageDelay = TimeSpan.Zero });

    [Fact]
    public async Task Primary_pages_are_combined_and_fallback_fills_newer_draws()
    {
        var primary = new FakeSource("vietlott", [V(3, 9, 1, 2, 3, 4, 5, 6)], [V(2, 9, 1, 2, 3, 4, 5, 7)]);
        var fallback = new FakeSource("minhngoc", [M(4, 9, 1, 2, 3, 4, 5, 8), M(3, 9, 1, 2, 3, 4, 5, 6)]);

        var report = await Service(primary, fallback).FetchAsync(P, 2, null, default);

        report.Draws.Select(d => d.DrawId).Should().Equal(4, 3, 2);
        report.Draws.Single(d => d.DrawId == 3).Should().Match<Draw>(d => d.Source == "vietlott" && d.Jackpot1 == 100);
        report.Conflicts.Should().BeEmpty();
        fallback.RequestedPages.Should().Equal(1); // chỉ cross-check trang 1
    }

    [Fact]
    public async Task Primary_failure_switches_to_fallback()
    {
        var primary = new FakeSource("vietlott") { Fail = true };
        var fallback = new FakeSource("minhngoc", [M(3, 9, 1, 2, 3, 4, 5, 6)]);

        var report = await Service(primary, fallback).FetchAsync(P, 5, null, default);

        report.Draws.Should().ContainSingle(d => d.Source == "minhngoc");
        report.SourcesUsed.Should().Equal("minhngoc");
        report.Warnings.Should().Contain(w => w.Contains("chuyển sang nguồn dự phòng"));
    }

    [Fact]
    public async Task Primary_failure_after_first_page_still_switches_to_fallback()
    {
        var primary = new FakeSource("vietlott", [V(3, 9, 1, 2, 3, 4, 5, 6)])
            { FailPages = [2] };
        var fallback = new FakeSource("minhngoc", [M(4, 9, 1, 2, 3, 4, 5, 8), M(3, 9, 1, 2, 3, 4, 5, 6)]);

        var report = await Service(primary, fallback).FetchAsync(P, 3, null, default);

        report.Draws.Select(d => d.DrawId).Should().Equal(4, 3);
        fallback.RequestedPages.Should().Equal(1, 2);
        report.Warnings.Should().Contain(w => w.Contains("chuyển sang nguồn dự phòng"));
    }

    [Fact]
    public async Task Conflicting_numbers_keep_vietlott_and_report()
    {
        var primary = new FakeSource("vietlott", [V(3, 9, 1, 2, 3, 4, 5, 6)]);
        var fallback = new FakeSource("minhngoc", [M(3, 9, 1, 2, 3, 4, 5, 7)]);

        var report = await Service(primary, fallback).FetchAsync(P, 1, null, default);

        report.Draws.Single().Numbers.Should().Equal(1, 2, 3, 4, 5, 6);
        report.Conflicts.Should().ContainSingle();
    }

    [Fact]
    public async Task Cross_check_fills_missing_jackpot2_even_when_jackpot1_exists()
    {
        var primaryDraw = new Draw(P, 3, Day, [1, 2, 3, 4, 5, 6], 9,
            jackpot1: 100, jackpot2: null, source: DrawSources.Vietlott);
        var fallbackDraw = new Draw(P, 3, Day, [1, 2, 3, 4, 5, 6], 9,
            jackpot1: 100, jackpot2: 50, source: DrawSources.MinhNgoc);

        var report = await Service(new FakeSource("vietlott", [primaryDraw]),
            new FakeSource("minhngoc", [fallbackDraw])).FetchAsync(P, 1, null, default);

        report.Draws.Single().Jackpot2.Should().Be(50);
    }

    [Fact]
    public async Task Repeated_page_stops_paging()
    {
        var page = new[] { V(3, 9, 1, 2, 3, 4, 5, 6) };
        var primary = new FakeSource("vietlott", page, page, page);

        var report = await Service(primary, new FakeSource("minhngoc")).FetchAsync(P, 3, "vietlott", default);

        primary.RequestedPages.Should().Equal(1, 2);
        report.Warnings.Should().ContainSingle(w => w.Contains("trùng dữ liệu"));
    }

    internal sealed class FakeSource(string name, params Draw[][] pages) : IDrawSource
    {
        public bool Fail { get; init; }
        public HashSet<int> FailPages { get; init; } = [];
        public Func<Game, bool> SupportsGame { get; init; } = _ => true;
        public List<int> RequestedPages { get; } = [];
        public string Name => name;

        public bool Supports(Game game) => SupportsGame(game);

        public Task<IReadOnlyList<Draw>> FetchPageAsync(Game game, int page, CancellationToken ct)
        {
            RequestedPages.Add(page);
            if (Fail || FailPages.Contains(page)) throw new HttpRequestException("boom");
            IReadOnlyList<Draw> result = page <= pages.Length ? pages[page - 1] : [];
            return Task.FromResult(result);
        }
    }
}
