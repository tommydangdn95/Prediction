using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using Power655.Core;

namespace Power655.Scraper;

/// <summary>
/// Tham số trang kết quả vietlott.vn của một game (xem docs/vietlott-endpoint.md).
/// <paramref name="Bands"/> × <paramref name="BandWidth"/> là kích thước mảng ArrayNumbers mà webpart đòi
/// (sai kích thước → server trả "Index was outside the bounds of the array").
/// </summary>
public sealed record VietlottGameEndpoint(string ResultsPath, string DrawResultAjaxPath, int Bands, int BandWidth)
{
    public static VietlottGameEndpoint For(Game game) => game.Code switch
    {
        "655" => Create("655", 5, 18),
        "645" => Create("645", 6, 18),
        "535" => Create("535", 5, 35),
        _ => throw new ArgumentException($"vietlott chưa hỗ trợ {game.Name}.", nameof(game)),
    };

    private static VietlottGameEndpoint Create(string code, int bands, int width) => new(
        $"/vi/trung-thuong/ket-qua-trung-thuong/winning-number-{code}",
        $"/ajaxpro/Vietlott.PlugIn.WebParts.Game{code}CompareWebPart,Vietlott.PlugIn.WebParts.ashx",
        bands, width);
}

/// <summary>
/// Nguồn chính vietlott.vn cho cả 3 game. Trang 1..n lấy qua AjaxPro <c>ServerSideDrawResult</c>
/// (PageIndex 0-based, 8 kỳ/trang — đã chạy thử 2026-11-08). Nếu AJAX lỗi ở trang 1 thì dùng HTML tĩnh
/// (có thể là cache cũ).
/// </summary>
public sealed class VietlottSource(HttpClient http, ILogger<VietlottSource> logger) : IDrawSource
{
    public const string BaseUrl = "https://vietlott.vn";
    public const string RenderInfoAjaxPath = "/ajaxpro/Vietlott.Utility.WebEnvironments,Vietlott.Utility.ashx";
    public const string SiteId = "main.frontend.vi";

    private readonly ConcurrentDictionary<string, string> _staticHtml = new();
    private readonly ConcurrentDictionary<string, string> _ajaxKey = new();
    private JsonNode? _renderInfo;

    public string Name => DrawSources.Vietlott;

    public bool Supports(Game game) => game.Code is "655" or "645" or "535";

    public async Task<IReadOnlyList<Draw>> FetchPageAsync(Game game, int page, CancellationToken ct)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(page, 1);
        var endpoint = VietlottGameEndpoint.For(game);
        try
        {
            var html = await FetchAjaxHtmlAsync(game, endpoint, page - 1, ct);
            var parsed = VietlottHtmlParser.Parse(html, game);
            LogParseErrors(parsed);
            if (parsed.Draws.Count > 0) return parsed.Draws;
            throw new DrawSourceException($"AJAX {game.Name} trang {page} không có kỳ nào.");
        }
        catch (Exception ex) when (page == 1 && IsRecoverable(ex, ct))
        {
            logger.LogWarning("vietlott {Game} AJAX lỗi ({Error}); dùng HTML tĩnh — có thể là dữ liệu cache cũ.",
                game.Name, ex.Message);
            var parsed = VietlottHtmlParser.Parse(await GetStaticHtmlAsync(game, endpoint, ct), game);
            LogParseErrors(parsed);
            return parsed.Draws;
        }
    }

    private async Task<string> FetchAjaxHtmlAsync(Game game, VietlottGameEndpoint endpoint, int pageIndex, CancellationToken ct)
    {
        var key = await GetAjaxKeyAsync(game, endpoint, ct);
        var renderInfo = await GetRenderInfoAsync(endpoint, ct);
        var emptyBand = new JsonArray(Enumerable.Range(0, endpoint.BandWidth).Select(_ => (JsonNode?)JsonValue.Create("")).ToArray());
        var body = new JsonObject
        {
            ["ORenderInfo"] = renderInfo.DeepClone(),
            ["Key"] = key,
            ["GameDrawId"] = "",
            ["ArrayNumbers"] = new JsonArray(Enumerable.Range(0, endpoint.Bands).Select(_ => (JsonNode?)emptyBand.DeepClone()).ToArray()),
            ["CheckMulti"] = false,
            ["PageIndex"] = pageIndex,
        };

        var value = await InvokeAjaxProAsync(endpoint, endpoint.DrawResultAjaxPath, "ServerSideDrawResult", body, ct);
        if (value["Error"]?.GetValue<bool>() == true)
            throw new DrawSourceException($"vietlott báo lỗi: {value["InfoMessage"]?.GetValue<string>()}");
        return value["HtmlContent"]?.GetValue<string>()
            ?? throw new DrawSourceException("Phản hồi AJAX thiếu HtmlContent.");
    }

    private async Task<JsonNode> GetRenderInfoAsync(VietlottGameEndpoint endpoint, CancellationToken ct) =>
        _renderInfo ??= await InvokeAjaxProAsync(
            endpoint, RenderInfoAjaxPath, "ServerSideFrontEndCreateRenderInfo", new JsonObject { ["SiteId"] = SiteId }, ct);

    /// <summary>Key truyền cho ServerSideDrawResult đổi theo từng lần tải trang, nên luôn đọc từ HTML trang của game.</summary>
    private async Task<string> GetAjaxKeyAsync(Game game, VietlottGameEndpoint endpoint, CancellationToken ct)
    {
        if (_ajaxKey.TryGetValue(game.Code, out var cached)) return cached;
        var key = VietlottHtmlParser.ExtractAjaxKey(await GetStaticHtmlAsync(game, endpoint, ct))
            ?? throw new DrawSourceException($"Không đọc được AJAX key từ trang {game.Name}.");
        return _ajaxKey[game.Code] = key;
    }

    private async Task<string> GetStaticHtmlAsync(Game game, VietlottGameEndpoint endpoint, CancellationToken ct)
    {
        if (_staticHtml.TryGetValue(game.Code, out var cached)) return cached;
        var html = await http.GetStringAsync(new Uri(BaseUrl + endpoint.ResultsPath), ct);
        return _staticHtml[game.Code] = html;
    }

    /// <summary>Gọi AjaxPro: POST JSON, header X-AjaxPro-Method; phản hồi dạng {"value": ...} (có thể kèm ";/*").</summary>
    private async Task<JsonNode> InvokeAjaxProAsync(
        VietlottGameEndpoint endpoint, string path, string method, JsonObject args, CancellationToken ct)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, new Uri(BaseUrl + path))
        {
            Content = new StringContent(args.ToJsonString(), Encoding.UTF8),
        };
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain") { CharSet = "utf-8" };
        request.Headers.Add("X-AjaxPro-Method", method);
        request.Headers.Referrer = new Uri(BaseUrl + endpoint.ResultsPath);

        using var response = await http.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
        var text = (await response.Content.ReadAsStringAsync(ct)).Trim();
        var end = text.LastIndexOf('}');
        if (end < 0) throw new DrawSourceException($"{method}: phản hồi không phải JSON.");
        var root = JsonNode.Parse(text[..(end + 1)]);
        return root?["value"] ?? throw new DrawSourceException($"{method}: phản hồi thiếu 'value'.");
    }

    private void LogParseErrors(ParseResult parsed)
    {
        foreach (var e in parsed.Errors) logger.LogWarning("{Error}", e);
    }

    internal static bool IsRecoverable(Exception ex, CancellationToken ct) =>
        !ct.IsCancellationRequested
        && ex is HttpRequestException or TaskCanceledException or JsonException
            or DrawSourceException or InvalidOperationException or Polly.Timeout.TimeoutRejectedException;
}
