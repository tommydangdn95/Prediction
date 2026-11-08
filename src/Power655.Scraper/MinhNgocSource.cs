using Microsoft.Extensions.Logging;
using Power655.Core;

namespace Power655.Scraper;

/// <summary>
/// Nguồn dự phòng minhngoc.net.vn cho Power 6/55 và Mega 6/45. Trang tổng hợp chỉ có ~10 kỳ gần nhất và
/// không phân trang, nên chỉ trang 1 có dữ liệu; trang &gt; 1 trả rỗng.
/// Lotto 5/35: trang minhngoc nạp kết quả bằng JS (HTML không có kỳ nào) nên không hỗ trợ.
/// </summary>
public sealed class MinhNgocSource(HttpClient http, ILogger<MinhNgocSource> logger) : IDrawSource
{
    public const string BaseUrl = "https://www.minhngoc.net.vn/ket-qua-xo-so/dien-toan-vietlott/";

    public string Name => DrawSources.MinhNgoc;

    public bool Supports(Game game) => PagePath(game) is not null;

    public static string? PagePath(Game game) => game.Code switch
    {
        "655" => "power-6x55.html",
        "645" => "mega-6x45.html",
        _ => null,
    };

    public async Task<IReadOnlyList<Draw>> FetchPageAsync(Game game, int page, CancellationToken ct)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(page, 1);
        var path = PagePath(game) ?? throw new DrawSourceException($"minhngoc không hỗ trợ {game.Name}.");
        if (page > 1)
        {
            logger.LogInformation("minhngoc không phân trang; bỏ qua trang {Page}.", page);
            return [];
        }

        var parsed = MinhNgocHtmlParser.Parse(await http.GetStringAsync(new Uri(BaseUrl + path), ct), game);
        foreach (var e in parsed.Errors) logger.LogWarning("{Error}", e);
        return parsed.Draws;
    }
}
