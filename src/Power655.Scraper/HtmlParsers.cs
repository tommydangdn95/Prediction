using System.Globalization;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Power655.Core;

namespace Power655.Scraper;

public sealed record ParseResult(IReadOnlyList<Draw> Draws, IReadOnlyList<string> Errors);

/// <summary>
/// Bảng kết quả vietlott.vn (trang tĩnh và HtmlContent của AJAX đều cùng cấu trúc, cả 3 game):
/// <c>tr &gt; td(Ngày dd/MM/yyyy) + td(a: Kỳ 01420) + td(span.bong_tron × n)</c>.
/// n = 7 với Power 6/55 (số thứ 7 là ĐB), 6 với Mega 6/45, 6 với Lotto 5/35 (5 số + ĐB 1–12).
/// Dấu "|" là <c>span.bong_tron-sperator</c>, không khớp selector.
/// </summary>
public static partial class VietlottHtmlParser
{
    public static ParseResult Parse(string html, Game game)
    {
        var expected = game.PickCount + (game.HasSpecial ? 1 : 0);
        var doc = new HtmlParser().ParseDocument(html);
        var draws = new List<Draw>();
        var errors = new List<string>();

        foreach (var row in doc.QuerySelectorAll("tr"))
        {
            var cells = row.QuerySelectorAll("td");
            if (cells.Length < 3) continue; // header / dòng khác
            try
            {
                var date = DateOnly.ParseExact(cells[0].TextContent.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var id = ParserUtil.ParseDrawId(cells[1].TextContent);
                var balls = cells[2].QuerySelectorAll("span.bong_tron").Select(ParserUtil.ParseBall).ToList();
                if (balls.Count != expected)
                    throw new FormatException($"{game.Name} cần {expected} số, thấy {balls.Count}");
                draws.Add(new Draw(game, id, date, balls[..game.PickCount],
                    game.HasSpecial ? balls[game.PickCount] : null, source: DrawSources.Vietlott));
            }
            catch (Exception ex) when (ex is FormatException or InvalidDrawException)
            {
                errors.Add($"vietlott {game.Code}: bỏ qua dòng '{ParserUtil.Snippet(row)}': {ex.Message}");
            }
        }
        return new ParseResult(draws, errors);
    }

    /// <summary>Key truyền cho ServerSideDrawResult, nhúng trong JS của trang.</summary>
    public static string? ExtractAjaxKey(string html) =>
        AjaxKeyRegex().Match(html) is { Success: true } m ? m.Groups[1].Value : null;

    [GeneratedRegex(@"ServerSideDrawResult\(\s*RenderInfo\s*,\s*'([^']+)'")]
    private static partial Regex AjaxKeyRegex();
}

/// <summary>
/// minhngoc.net.vn: mỗi kỳ là một khối (<c>div.bangkq6x55</c> với 6/55, <c>div.bangkq6x36</c> với 6/45) chứa
/// "Kỳ vé: #01420 | Ngày quay thưởng 05/11/2026", <c>ul.result-number div.bool</c> (6/55: số thứ 7 trong
/// <c>li.number_special</c>), và bảng giải (jackpot).
/// </summary>
public static partial class MinhNgocHtmlParser
{
    public static string? BoxSelector(Game game) => game.Code switch
    {
        "655" => "div.bangkq6x55",
        "645" => "div.bangkq6x36",
        _ => null,
    };

    public static ParseResult Parse(string html, Game game)
    {
        var selector = BoxSelector(game) ?? throw new ArgumentException($"minhngoc không hỗ trợ {game.Name}.", nameof(game));
        var doc = new HtmlParser().ParseDocument(html);
        var draws = new List<Draw>();
        var errors = new List<string>();

        foreach (var box in doc.QuerySelectorAll(selector))
        {
            try
            {
                var idEl = box.QuerySelector("[id$='_KY_VE']")
                    ?? throw new FormatException("không thấy Kỳ vé");
                var id = ParserUtil.ParseDrawId(idEl.TextContent);

                // "Kỳ vé: #01420 | Ngày quay thưởng 05/11/2026" nằm cùng ô <td> với span kỳ vé
                var timeText = idEl.ParentElement?.TextContent ?? box.TextContent;
                var dateMatch = DateRegex().Match(timeText);
                if (!dateMatch.Success) throw new FormatException("không thấy ngày quay");
                var date = DateOnly.ParseExact(dateMatch.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var numberList = box.QuerySelector("ul.result-number")
                    ?? throw new FormatException("không thấy dãy số");
                var main = numberList.Children
                    .Where(li => !li.ClassList.Contains("number_special") && !li.ClassList.Contains("number_device"))
                    .Select(li => li.QuerySelector(".bool"))
                    .OfType<IElement>()
                    .Select(ParserUtil.ParseBall)
                    .ToList();
                int? special = null;
                if (game.HasSpecial)
                {
                    var specialEl = numberList.QuerySelector("li.number_special .bool")
                        ?? throw new FormatException("không thấy số đặc biệt");
                    special = ParserUtil.ParseBall(specialEl);
                }

                draws.Add(new Draw(
                    game, id, date, main, special,
                    ParserUtil.ParseMoney(box.QuerySelector("[id$='_G_JACKPOT']")),
                    ParserUtil.ParseMoney(box.QuerySelector("[id$='_G_JACKPOT2']")),
                    DrawSources.MinhNgoc));
            }
            catch (Exception ex) when (ex is FormatException or InvalidDrawException)
            {
                errors.Add($"minhngoc {game.Code}: bỏ qua khối '{ParserUtil.Snippet(box)}': {ex.Message}");
            }
        }
        return new ParseResult(draws, errors);
    }

    [GeneratedRegex(@"\d{2}/\d{2}/\d{4}")]
    private static partial Regex DateRegex();
}

internal static partial class ParserUtil
{
    public static int ParseDrawId(string text)
    {
        var m = DigitsRegex().Match(text);
        if (!m.Success) throw new FormatException($"không đọc được số kỳ từ '{text.Trim()}'");
        return int.Parse(m.Value, CultureInfo.InvariantCulture);
    }

    public static int ParseBall(IElement el) =>
        int.TryParse(el.TextContent.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out var n)
            ? n
            : throw new FormatException($"số không hợp lệ '{el.TextContent.Trim()}'");

    public static long? ParseMoney(IElement? el)
    {
        if (el is null) return null;
        var digits = new string(el.TextContent.Where(char.IsAsciiDigit).ToArray());
        return digits.Length is > 0 and <= 18 ? long.Parse(digits, CultureInfo.InvariantCulture) : null;
    }

    public static string Snippet(IElement el)
    {
        var text = WhitespaceRegex().Replace(el.TextContent, " ").Trim();
        return text.Length > 60 ? text[..60] + "…" : text;
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex DigitsRegex();

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();
}
