using System.CommandLine;
using System.Globalization;
using Power655.Core;
using Power655.Core.Strategies;
using Spectre.Console;

namespace Power655.Cli.Commands;

internal static class SuggestCommand
{
    private static readonly string[] Names = [.. StrategyCatalog.Names, CoverPicker.Name];

    public static Command Create(GlobalOptions globals)
    {
        var strategy = new Option<string>("--strategy", "-s")
        {
            Description = "Chiến lược: " + string.Join("|", Names) + " (cover = phủ số: nhóm vé ít trùng nhau nhất)",
            DefaultValueFactory = _ => "balanced",
        };
        strategy.AcceptOnlyFromAmong(Names);
        var sets = new Option<int>("--sets") { Description = "Số bộ cần tạo mỗi game", DefaultValueFactory = _ => 3 };
        sets.Min(1);
        var seed = new Option<int?>("--seed") { Description = "Seed để tái lập kết quả (bỏ trống = ngẫu nhiên)" };
        var last = new Option<int>("--last") { Description = "Dùng N kỳ gần nhất làm lịch sử (0 = tất cả)", DefaultValueFactory = _ => 0 };
        last.Min(0);

        var command = new Command("suggest",
            "Sinh bộ số theo chiến lược cho từng game (luôn kèm bộ ngẫu nhiên đối chứng).") { strategy, sets, seed, last };
        command.SetAction(async (result, ct) =>
        {
            var name = result.GetValue(strategy)!;
            var seedValue = result.GetValue(seed) ?? Random.Shared.Next();
            var count = result.GetValue(sets);
            var exit = 0;

            foreach (var (game, history) in await globals.LoadDrawsAsync(result, result.GetValue(last), ct))
            {
                Ui.GameHeader(game);
                if (history.Count == 0 && name is not ("random" or CoverPicker.Name)) { Ui.NoData(game); exit = 1; continue; }
                // seed riêng mỗi game để kết quả một game không phụ thuộc game nào khác được chọn cùng
                var rng = new Random(GameSeed(seedValue, game));
                if (name == CoverPicker.Name) RenderCover(game, count, rng, seedValue);
                else Render(game, history, name, count, rng, seedValue);
            }
            return exit;
        });
        return command;
    }

    /// <summary>Seed tất định theo game (không dùng HashCode vì nó đổi giữa các lần chạy).</summary>
    internal static int GameSeed(int seed, Game game) =>
        unchecked(seed * 31 + int.Parse(game.Code, CultureInfo.InvariantCulture));

    /// <summary>Bộ phủ số kèm P(ít nhất một vé trúng) chính xác khi mua k vé đầu, so với k vé ngẫu nhiên.</summary>
    private static void RenderCover(Game game, int count, Random rng, int seedValue)
    {
        var tickets = CoverPicker.Pick(game, count, rng);
        var any = PortfolioOdds.AnyPrizeByPrefix(game, tickets);
        var table = new Table().Border(TableBorder.Rounded).AddColumn("#").AddColumn("Bộ số");
        if (game.TicketHasSpecial) table.AddColumn("ĐB");
        table.AddColumn(new TableColumn("Mua từ vé 1 đến vé này: có ≥1 vé trúng").RightAligned())
            .AddColumn(new TableColumn("Cùng số vé ngẫu nhiên").RightAligned());
        for (var k = 1; k <= tickets.Count; k++)
        {
            var t = tickets[k - 1];
            var cells = new List<string> { k.ToString(CultureInfo.InvariantCulture), $"[bold]{Ui.Balls(t.Numbers)}[/]" };
            if (game.TicketHasSpecial) cells.Add($"[bold yellow]{Ui.Ball(t.Special!.Value)}[/]");
            cells.Add($"[green]{Ui.Percent(any[k - 1])}[/]");
            cells.Add(Ui.Percent(PortfolioOdds.IndependentAnyPrize(game, k)));
            table.AddRow(cells.ToArray());
        }
        AnsiConsole.MarkupLine($"Bộ phủ số {count} vé · seed {seedValue} (không dùng lịch sử)");
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[grey]Cột %: khả năng có ít nhất 1 vé trúng nếu mua từ vé 1 đến vé ở dòng đó (mua 5 vé → xem dòng 5). " +
                               $"Mỗi vé riêng lẻ vẫn chỉ trúng {Ui.Percent(game.Odds.AnyPrizeProbability)}; tiền thưởng trung bình không đổi, " +
                               "phủ số chỉ giảm số kỳ cả nhóm vé cùng trắng tay.[/]");
        Ui.WriteOdds(game);
        AnsiConsole.WriteLine();
    }

    private static void Render(Game game, IReadOnlyList<Draw> history, string name, int count, Random rng, int seedValue)
    {
        var table = new Table().Border(TableBorder.Rounded)
            .AddColumn("Chiến lược").AddColumn("#").AddColumn("Bộ số");
        if (game.TicketHasSpecial) table.AddColumn("ĐB");
        table.AddColumn("Chẵn/Lẻ").AddColumn($"Nhỏ/Lớn (≤{game.LowHighBoundary})").AddColumn(new TableColumn("Tổng").RightAligned());

        var strategies = name == "random"
            ? new[] { StrategyCatalog.Create("random") }
            : [StrategyCatalog.Create(name), StrategyCatalog.Create("random")];
        foreach (var s in strategies)
        {
            for (var i = 1; i <= count; i++)
            {
                var ticket = s.Pick(game, history, rng);
                var pick = ticket.Numbers;
                var even = pick.Count(n => n % 2 == 0);
                var low = pick.Count(n => n <= game.LowHighBoundary);
                var label = s.Name == "random" && name != "random" ? "random (đối chứng)" : s.Name;
                var cells = new List<string> { label, i.ToString(CultureInfo.InvariantCulture), $"[bold]{Ui.Balls(pick)}[/]" };
                if (game.TicketHasSpecial) cells.Add($"[bold yellow]{Ui.Ball(ticket.Special!.Value)}[/]");
                cells.Add($"{even}/{pick.Count - even}");
                cells.Add($"{low}/{pick.Count - low}");
                cells.Add(pick.Sum().ToString(CultureInfo.InvariantCulture));
                table.AddRow(cells.ToArray());
            }
        }

        var range = history.Count > 0
            ? $" (#{history[0].DrawId:D5} → #{history[^1].DrawId:D5} {history[^1].Date:dd/MM/yyyy})"
            : "";
        AnsiConsole.MarkupLine($"Gợi ý từ {history.Count} kỳ lịch sử{range} · seed {seedValue}");
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[grey]Mọi bộ số — kể cả bộ ngẫu nhiên — có cùng xác suất trúng. Bộ gợi ý chỉ phản ánh đặc điểm dữ liệu quá khứ.[/]");
        Ui.WriteOdds(game);
        AnsiConsole.WriteLine();
    }
}
