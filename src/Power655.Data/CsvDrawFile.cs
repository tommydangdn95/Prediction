using System.Globalization;
using Power655.Core;

namespace Power655.Data;

public sealed record CsvReadResult(IReadOnlyList<Draw> Draws, IReadOnlyList<string> Errors);

/// <summary>
/// CSV kỳ quay. Cột bắt buộc: draw_id,date,n1..n5 (date = yyyy-MM-dd).
/// Cột theo game: n6 (6/45, 6/55), special (6/55, 5/35) — ô trống khi game không dùng.
/// Cột tùy chọn: game (535|645|655, mặc định theo tham số), jackpot1, jackpot2, source. Thứ tự cột theo header.
/// </summary>
public static class CsvDrawFile
{
    public const string Header = "game,draw_id,date,n1,n2,n3,n4,n5,n6,special,jackpot1,jackpot2,source";

    private static readonly string[] Required = ["draw_id", "date", "n1", "n2", "n3", "n4", "n5"];

    /// <param name="defaultGame">Game cho các dòng không có cột/ô <c>game</c>; null = bắt buộc có cột game.</param>
    public static CsvReadResult Read(TextReader reader, Game? defaultGame, string defaultSource = DrawSources.Seed)
    {
        var draws = new List<Draw>();
        var errors = new List<string>();

        var header = reader.ReadLine();
        if (header is null) return new CsvReadResult(draws, ["File rỗng."]);

        var columns = header.Split(',').Select(c => c.Trim().ToLowerInvariant()).ToList();
        var missing = Required.Where(c => !columns.Contains(c)).ToList();
        if (missing.Count > 0)
            return new CsvReadResult(draws, [$"Thiếu cột: {string.Join(", ", missing)}."]);
        if (defaultGame is null && !columns.Contains("game"))
            return new CsvReadResult(draws, ["Thiếu cột game và không chỉ định game mặc định (--game)."]);
        int Col(string name) => columns.IndexOf(name);

        var lineNo = 1;
        while (reader.ReadLine() is { } line)
        {
            lineNo++;
            if (string.IsNullOrWhiteSpace(line)) continue;
            var cells = line.Split(',').Select(c => c.Trim()).ToArray();
            try
            {
                string Cell(string name) =>
                    Col(name) is var i and >= 0 && i < cells.Length ? cells[i] : "";
                long? OptionalLong(string name) =>
                    string.IsNullOrEmpty(Cell(name)) ? null : long.Parse(Cell(name), CultureInfo.InvariantCulture);
                int? OptionalInt(string name) =>
                    string.IsNullOrEmpty(Cell(name)) ? null : int.Parse(Cell(name), CultureInfo.InvariantCulture);

                var game = Cell("game") is { Length: > 0 } code
                    ? Games.TryGet(code) ?? throw new FormatException($"game không hợp lệ '{code}'")
                    : defaultGame ?? throw new FormatException("thiếu game");
                var numbers = Enumerable.Range(1, game.PickCount)
                    .Select(i => int.Parse(Cell($"n{i}"), CultureInfo.InvariantCulture)).ToArray();
                if (game.PickCount < 6 && !string.IsNullOrEmpty(Cell("n6")))
                    throw new FormatException($"{game.Name} chỉ có {game.PickCount} số nhưng n6 = {Cell("n6")}");
                var source = Cell("source") is { Length: > 0 } s ? s : defaultSource;
                draws.Add(new Draw(
                    game,
                    int.Parse(Cell("draw_id"), CultureInfo.InvariantCulture),
                    DateOnly.ParseExact(Cell("date"), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    numbers,
                    OptionalInt("special"),
                    OptionalLong("jackpot1"),
                    OptionalLong("jackpot2"),
                    source));
            }
            catch (Exception ex) when (ex is FormatException or OverflowException or InvalidDrawException)
            {
                errors.Add($"Dòng {lineNo}: {ex.Message}");
            }
        }
        return new CsvReadResult(draws, errors);
    }

    public static void Write(TextWriter writer, IEnumerable<Draw> draws)
    {
        static string S(long? v) => v?.ToString(CultureInfo.InvariantCulture) ?? "";

        writer.WriteLine(Header);
        foreach (var d in draws.OrderBy(d => d.Game.Code).ThenByDescending(d => d.DrawId))
        {
            var n = d.Numbers;
            writer.WriteLine(string.Join(',',
                d.Game.Code,
                d.DrawId.ToString(CultureInfo.InvariantCulture),
                d.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                string.Join(',', Enumerable.Range(0, 6).Select(i => i < n.Count ? S(n[i]) : "")),
                S(d.Special),
                S(d.Jackpot1),
                S(d.Jackpot2),
                d.Source));
        }
    }
}
