using System.Globalization;
using Power655.Core;
using Spectre.Console;

namespace Power655.Cli;

/// <summary>Các khối hiển thị dùng chung.</summary>
internal static class Ui
{
    public static readonly CultureInfo Vi = CultureInfo.GetCultureInfo("vi-VN");

    public static string Ball(int n) => n.ToString("D2", CultureInfo.InvariantCulture);

    public static string Balls(IEnumerable<int> numbers) => string.Join(" ", numbers.Select(Ball));

    public static string Money(long? value) => value is null ? "-" : value.Value.ToString("N0", Vi) + "đ";

    public static void GameHeader(Game game) =>
        AnsiConsole.Write(new Rule($"[bold aqua]{Markup.Escape(game.Name)}[/]").LeftJustified());

    /// <summary>Bảng xác suất bắt buộc (spec mục 8) của game + chú thích.</summary>
    public static void WriteOdds(Game game)
    {
        var table = new Table().Border(TableBorder.Rounded)
            .Title($"[bold]Xác suất thật mỗi vé {Markup.Escape(game.Name)}[/]")
            .AddColumn("Giải").AddColumn("Điều kiện").AddColumn(new TableColumn("Xác suất / vé").RightAligned());
        foreach (var o in game.Odds.Table)
            table.AddRow(o.Prize, o.Condition, "1 / " + o.OneIn.ToString("N0", Vi));
        table.AddRow("[grey]Trúng giải bất kỳ[/]", "",
            $"[grey]1 / {(1 / game.Odds.AnyPrizeProbability).ToString("N1", Vi)}[/]");
        AnsiConsole.Write(table);
        if (game.Special == SpecialKind.BonusFromMainPool)
            AnsiConsole.MarkupLine("[grey]ĐB = số thứ 7 máy quay thêm; vé chỉ có 6 số, \"+ ĐB\" nghĩa là số còn lại của vé trùng số ĐB.[/]");
        if (game.TicketHasSpecial)
            AnsiConsole.MarkupLine($"[grey]Vé gồm {game.PickCount} số 01–{game.MaxNumber} + 1 số ĐB 01–{game.SpecialMax} (lồng riêng).[/]");
        WriteDisclaimer();
    }

    public static void WriteDisclaimer() =>
        AnsiConsole.MarkupLine($"[italic yellow]{Markup.Escape(Odds.Disclaimer)}[/]");

    public static void WriteDraws(Game game, IEnumerable<Draw> draws, string title)
    {
        var table = new Table().Border(TableBorder.Simple).Title(Markup.Escape(title))
            .AddColumn("Kỳ").AddColumn("Ngày").AddColumn("Bộ số");
        if (game.HasSpecial) table.AddColumn("ĐB");
        table.AddColumn(new TableColumn("Jackpot 1").RightAligned()).AddColumn("Nguồn");
        foreach (var d in draws.OrderByDescending(d => d.DrawId))
        {
            var cells = new List<string>
            {
                d.DrawId.ToString("D5", CultureInfo.InvariantCulture),
                d.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                Balls(d.Numbers),
            };
            if (game.HasSpecial) cells.Add(d.Special is { } s ? $"[yellow]{Ball(s)}[/]" : "-");
            cells.Add(Money(d.Jackpot1));
            cells.Add(d.Source);
            table.AddRow(cells.ToArray());
        }
        AnsiConsole.Write(table);
    }

    public static void Error(string message) =>
        AnsiConsole.MarkupLine($"[red]Lỗi:[/] {Markup.Escape(message)}");

    public static void Warn(string message) =>
        AnsiConsole.MarkupLine($"[yellow]Cảnh báo:[/] {Markup.Escape(message)}");

    public static void NoData(Game game) =>
        Error($"Chưa có dữ liệu {game.Name}. Chạy 'power655 fetch --game {game.Code}' (hoặc import CSV) trước.");
}
