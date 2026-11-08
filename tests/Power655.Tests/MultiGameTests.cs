using Microsoft.Extensions.Logging.Abstractions;
using Power655.Core;
using Power655.Data;
using Power655.Scraper;
using static Power655.Tests.TestData;

namespace Power655.Tests;

public class GameDefinitionTests
{
    [Fact]
    public void Three_games_with_expected_rules()
    {
        Games.All.Select(g => g.Code).Should().Equal("535", "645", "655");

        var lotto = Games.Lotto535;
        (lotto.PickCount, lotto.MaxNumber, lotto.SpecialMax, lotto.TicketHasSpecial, lotto.LowHighBoundary)
            .Should().Be((5, 35, 12, true, 17));
        var mega = Games.Mega645;
        (mega.PickCount, mega.MaxNumber, mega.HasSpecial, mega.LowHighBoundary).Should().Be((6, 45, false, 22));
        var power = Games.Power655;
        (power.PickCount, power.MaxNumber, power.SpecialMax, power.TicketHasSpecial, power.LowHighBoundary)
            .Should().Be((6, 55, 55, false, 27));
    }

    [Theory]
    [InlineData("655", "655")]
    [InlineData("6/45", "645")]
    [InlineData("5x35", "535")]
    [InlineData("power655", "655")]
    public void Codes_are_parsed_leniently(string input, string code) => Games.Get(input).Code.Should().Be(code);

    [Fact]
    public void Unknown_game_throws() => ((Action)(() => Games.Get("649"))).Should().Throw<ArgumentException>();
}

public class MultiGameDrawTests
{
    private static readonly DateOnly Day = new(2026, 11, 8);

    [Fact]
    public void Mega645_has_no_special()
    {
        var d = new Draw(Games.Mega645, 1586, Day, [36, 1, 2, 14, 20, 23], null);
        d.Numbers.Should().Equal(1, 2, 14, 20, 23, 36);
        d.Special.Should().BeNull();
        d.ToString().Should().Be("645 #01586 08/11/2026: 01 02 14 20 23 36");

        ((Action)(() => new Draw(Games.Mega645, 1, Day, [1, 2, 3, 4, 5, 6], 7))).Should().Throw<InvalidDrawException>();
        ((Action)(() => new Draw(Games.Mega645, 1, Day, [1, 2, 3, 4, 5, 46], null))).Should().Throw<InvalidDrawException>();
    }

    [Fact]
    public void Lotto535_special_is_separate_pool_and_may_equal_a_main_number()
    {
        // kỳ thật #00996: 04 11 17 26 35 | 11
        var d = new Draw(Games.Lotto535, 996, Day, [4, 11, 17, 26, 35], 11);
        d.Special.Should().Be(11);
        d.ToString().Should().Be("535 #00996 08/11/2026: 04 11 17 26 35 | 11");
    }

    public static TheoryData<int[], int?> InvalidLotto => new()
    {
        { [1, 2, 3, 4], 1 },          // thiếu số
        { [1, 2, 3, 4, 5, 6], 1 },    // thừa số
        { [1, 2, 3, 4, 36], 1 },      // > 35
        { [1, 2, 3, 4, 5], 13 },      // ĐB > 12
        { [1, 2, 3, 4, 5], 0 },
        { [1, 2, 3, 4, 5], null },    // thiếu ĐB
    };

    [Theory]
    [MemberData(nameof(InvalidLotto))]
    public void Invalid_lotto_draw_throws(int[] numbers, int? special)
    {
        var act = () => new Draw(Games.Lotto535, 1, Day, numbers, special);
        act.Should().Throw<InvalidDrawException>();
    }

    [Fact]
    public void Same_id_in_different_games_is_not_same_result()
    {
        var a = new Draw(Games.Mega645, 1, Day, [1, 2, 3, 4, 5, 6], null);
        var b = new Draw(Games.Power655, 1, Day, [1, 2, 3, 4, 5, 6], 7);
        a.HasSameResultAs(b).Should().BeFalse();
    }
}

public class MultiGameStatisticsTests
{
    [Fact]
    public void Statistics_use_game_number_range()
    {
        var mega = Synthetic(Games.Mega645, 30, 1);
        DrawStatistics.Frequency(Games.Mega645, mega).Should().HaveCount(45);
        DrawStatistics.Frequency(Games.Mega645, mega).Values.Sum().Should().Be(180);
        DrawStatistics.SpecialFrequency(Games.Mega645, mega).Should().BeEmpty();
        DrawStatistics.ChiSquareUniform(Games.Mega645, mega).DegreesOfFreedom.Should().Be(44);
        DrawStatistics.OddEven(Games.Mega645, mega).Keys.Should().Equal(0, 1, 2, 3, 4, 5, 6);

        var lotto = Synthetic(Games.Lotto535, 30, 2);
        DrawStatistics.Frequency(Games.Lotto535, lotto).Should().HaveCount(35);
        DrawStatistics.SpecialFrequency(Games.Lotto535, lotto).Should().HaveCount(12);
        DrawStatistics.SpecialFrequency(Games.Lotto535, lotto).Values.Sum().Should().Be(30);
        DrawStatistics.SpecialWeightedScore(Games.Lotto535, lotto).Values.Sum().Should().BeApproximately(1, 1e-9);
        DrawStatistics.WeightedScore(Games.Lotto535, lotto).Values.Sum().Should().BeApproximately(1, 1e-9);
        DrawStatistics.HighLow(Games.Lotto535, lotto).Keys.Should().Equal(0, 1, 2, 3, 4, 5);
    }

    [Fact]
    public void Mixed_games_are_rejected()
    {
        var mixed = Seed.Concat(Synthetic(Games.Mega645, 2, 1)).ToList();
        var act = () => DrawStatistics.Frequency(P, mixed);
        act.Should().Throw<ArgumentException>();
    }
}

/// <summary>Fixture là HTML thật lấy ngày 2026-11-08 (HtmlContent AJAX trang 1; minhngoc cắt 3 khối).</summary>
public class MultiGameParserTests
{
    [Fact]
    public void Vietlott_645_page_parses_8_draws()
    {
        var result = VietlottHtmlParser.Parse(File.ReadAllText(Fixture("vietlott_645_page1.html")), Games.Mega645);

        result.Errors.Should().BeEmpty();
        result.Draws.Should().HaveCount(8);
        result.Draws.Select(d => d.DrawId).Should().Equal(1586, 1585, 1584, 1583, 1582, 1581, 1580, 1579);
        var first = result.Draws[0];
        first.Date.Should().Be(new DateOnly(2026, 11, 8));
        first.Numbers.Should().Equal(1, 2, 14, 20, 23, 36);
        first.Special.Should().BeNull();
    }

    [Fact]
    public void Vietlott_535_page_parses_two_draws_per_day_with_special()
    {
        var result = VietlottHtmlParser.Parse(File.ReadAllText(Fixture("vietlott_535_page1.html")), Games.Lotto535);

        result.Errors.Should().BeEmpty();
        result.Draws.Select(d => d.DrawId).Should().Equal(996, 995, 994, 993, 992, 991, 990, 989);
        result.Draws.Count(d => d.Date == new DateOnly(2026, 11, 8)).Should().Be(2);
        result.Draws[0].Numbers.Should().Equal(4, 11, 17, 26, 35);
        result.Draws[0].Special.Should().Be(11);
        result.Draws[1].Numbers.Should().Equal(2, 13, 16, 18, 22);
        result.Draws[1].Special.Should().Be(5);
    }

    [Fact]
    public void Parsing_with_wrong_game_reports_errors()
    {
        var result = VietlottHtmlParser.Parse(File.ReadAllText(Fixture("vietlott_535_page1.html")), Games.Power655);
        result.Draws.Should().BeEmpty();
        result.Errors.Should().HaveCount(8);
    }

    [Fact]
    public void MinhNgoc_645_blocks_parse_with_jackpot()
    {
        var result = MinhNgocHtmlParser.Parse(File.ReadAllText(Fixture("minhngoc_645_page1.html")), Games.Mega645);

        result.Errors.Should().BeEmpty();
        result.Draws.Select(d => d.DrawId).Should().Equal(1586, 1585, 1584);
        result.Draws[0].Numbers.Should().Equal(1, 2, 14, 20, 23, 36);
        result.Draws[0].Special.Should().BeNull();
        result.Draws[0].Jackpot1.Should().Be(15_740_836_000);
        result.Draws[0].Source.Should().Be("minhngoc");

        var vietlott = VietlottHtmlParser.Parse(File.ReadAllText(Fixture("vietlott_645_page1.html")), Games.Mega645)
            .Draws.ToDictionary(d => d.DrawId);
        result.Draws.Should().OnlyContain(d => vietlott[d.DrawId].HasSameResultAs(d) && vietlott[d.DrawId].Date == d.Date);
    }

    [Fact]
    public void Vietlott_endpoints_match_page_arrays()
    {
        VietlottGameEndpoint.For(Games.Power655).Should().Match<VietlottGameEndpoint>(e => e.Bands == 5 && e.BandWidth == 18);
        VietlottGameEndpoint.For(Games.Mega645).Should().Match<VietlottGameEndpoint>(e => e.Bands == 6 && e.BandWidth == 18);
        VietlottGameEndpoint.For(Games.Lotto535).Should().Match<VietlottGameEndpoint>(e => e.Bands == 5 && e.BandWidth == 35);
        VietlottGameEndpoint.For(Games.Lotto535).DrawResultAjaxPath.Should().Contain("Game535CompareWebPart");
    }
}

public class MultiGameFetchTests
{
    [Fact]
    public async Task Unsupported_fallback_is_skipped_for_lotto()
    {
        var game = Games.Lotto535;
        var draw = new Draw(game, 996, new DateOnly(2026, 11, 8), [4, 11, 17, 26, 35], 11, source: DrawSources.Vietlott);
        var primary = new FetchServiceTests.FakeSource("vietlott", [draw]);
        var fallback = new FetchServiceTests.FakeSource("minhngoc", [draw]) { SupportsGame = g => g != Games.Lotto535 };
        var service = new DrawFetchService(primary, fallback, NullLogger<DrawFetchService>.Instance,
            new FetchOptions { PageDelay = TimeSpan.Zero });

        var report = await service.FetchAsync(game, 1, null, default);

        report.Draws.Should().ContainSingle();
        report.SourcesUsed.Should().Equal("vietlott");
        fallback.RequestedPages.Should().BeEmpty();
        report.Warnings.Should().BeEmpty();

        var only = await service.FetchAsync(game, 1, "minhngoc", default);
        only.Draws.Should().BeEmpty();
        only.Warnings.Should().ContainSingle(w => w.Contains("không hỗ trợ"));
    }
}

public class MultiGameDataTests : IDisposable
{
    private readonly string _dbPath = Path.Combine(Path.GetTempPath(), $"power655-mg-{Guid.NewGuid():N}.db");

    [Fact]
    public void Csv_round_trip_all_games()
    {
        var draws = new[]
        {
            new Draw(Games.Lotto535, 996, new DateOnly(2026, 11, 8), [4, 11, 17, 26, 35], 11, source: "vietlott"),
            new Draw(Games.Mega645, 1586, new DateOnly(2026, 11, 8), [1, 2, 14, 20, 23, 36], null, 15_740_836_000, null, "minhngoc"),
            new Draw(P, 1420, new DateOnly(2026, 11, 5), [6, 20, 34, 41, 49, 52], 36),
        };
        var writer = new StringWriter();
        CsvDrawFile.Write(writer, draws);
        writer.ToString().Should().StartWith(CsvDrawFile.Header);
        writer.ToString().Should().Contain("535,996,2026-11-08,4,11,17,26,35,,11,,,vietlott");

        var read = CsvDrawFile.Read(new StringReader(writer.ToString()), null);
        read.Errors.Should().BeEmpty();
        read.Draws.Should().BeEquivalentTo(draws);
    }

    [Fact]
    public void Csv_without_game_column_needs_default_game()
    {
        const string csv = "draw_id,date,n1,n2,n3,n4,n5,n6\n1586,2026-11-08,1,2,14,20,23,36";
        CsvDrawFile.Read(new StringReader(csv), null).Errors.Should().ContainSingle();
        CsvDrawFile.Read(new StringReader(csv), Games.Mega645).Draws.Should().ContainSingle(d => d.Special == null);
    }

    [Fact]
    public async Task Same_draw_id_in_different_games_is_stored_separately()
    {
        await using var db = new Power655DbContext(Power655DbContext.CreateOptions(_dbPath));
        var repo = new DrawRepository(db);
        await repo.MigrateAsync();

        var day = new DateOnly(2026, 11, 8);
        var lotto = new Draw(Games.Lotto535, 100, day, [1, 2, 3, 4, 5], 5);
        var mega = new Draw(Games.Mega645, 100, day, [1, 2, 3, 4, 5, 6], null);
        var power = D(100, 7, 1, 2, 3, 4, 5, 6);

        (await repo.UpsertAsync([lotto, mega, power])).Inserted.Should().Be(3);
        (await repo.UpsertAsync([lotto, mega, power])).Unchanged.Should().Be(3);
        (await repo.CountAsync()).Should().Be(3);
        (await repo.CountAsync(Games.Mega645)).Should().Be(1);
        (await repo.GetAsync(Games.Lotto535)).Single().Should().Be(lotto);
        (await repo.GetAsync(Games.Mega645)).Single().Should().Be(mega);
        (await repo.GetAsync(P)).Single().Should().Be(power);
    }

    public void Dispose()
    {
        Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
        if (File.Exists(_dbPath)) File.Delete(_dbPath);
    }
}
