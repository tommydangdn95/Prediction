using System.CommandLine;
using System.Globalization;
using Power655.Core;
using Power655.Core.Strategies;
using Spectre.Console;

namespace Power655.Cli.Commands;

internal static class SuggestCommand
{
    public static Command Create(GlobalOptions globals)
    {
        var strategy = new Option<string>("--strategy", "-s")
        {
            Description = "Chiến lược: " + string.Join("|", StrategyCatalog.Names),
            DefaultValueFactory = _ => "balanced",
        };
        strategy.AcceptOnlyFromAmong([.. StrategyCatalog.Names]);
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
                if (history.Count == 0 && name != "random") { Ui.NoData(game); exit = 1; continue; }
                // seed riêng mỗi game để kết quả một game không phụ thuộc game nào khác được chọn cùng
                var rng = new Random(GameSeed(seedValue, game));
                Render(game, history, name, count, rng, seedValue);
            }
            return exit;
        });
        return command;
    }

    /// <summary>Seed tất định theo game (không dùng HashCode vì nó đổi giữa các lần chạy).</summary>
    internal static int GameSeed(int seed, Game game) =>
        unchecked(seed * 31 + int.Parse(game.Code, CultureInfo.InvariantCulture));

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
