using System.CommandLine;
using System.Globalization;
using Power655.Core;
using Power655.Core.Strategies;
using Spectre.Console;

namespace Power655.Cli.Commands;

internal static class BacktestCommand
{
    public static Command Create(GlobalOptions globals)
    {
        var train = new Option<int>("--train") { Description = "Số kỳ đầu chỉ dùng làm lịch sử (kiểm tra từ kỳ train+1)", DefaultValueFactory = _ => 20 };
        train.Min(1);
        var runs = new Option<int>("--runs") { Description = "Số lần lặp mỗi kỳ cho mỗi chiến lược (lấy trung bình)", DefaultValueFactory = _ => 1000 };
        runs.Min(1);
        var seed = new Option<int>("--seed") { Description = "Seed", DefaultValueFactory = _ => 42 };
        var last = new Option<int>("--last") { Description = "Chỉ dùng N kỳ gần nhất (0 = tất cả)", DefaultValueFactory = _ => 0 };
        last.Min(0);

        var command = new Command("backtest", "Walk-forward backtest các chiến lược so với ngẫu nhiên và lý thuyết, từng game.") { train, runs, seed, last };
        command.SetAction(async (result, ct) =>
        {
            var trainCount = result.GetValue(train);
            var runCount = result.GetValue(runs);
            var seedValue = result.GetValue(seed);
            var exit = 0;

            foreach (var (game, draws) in await globals.LoadDrawsAsync(result, result.GetValue(last), ct))
            {
                Ui.GameHeader(game);
                if (draws.Count <= trainCount)
                {
                    Ui.Error($"{game.Name}: cần nhiều hơn {trainCount} kỳ để backtest (DB có {draws.Count}).");
                    exit = 1;
                    continue;
                }
                var results = new List<BacktestResult>();
                AnsiConsole.Status().Start($"Đang backtest {game.Name}...", _ =>
                {
                    foreach (var s in StrategyCatalog.All())
                        results.Add(Backtester.Run(game, draws, s, trainCount, runCount, seedValue));
                });
                Render(game, draws, trainCount, runCount, seedValue, results);
            }
            return exit;
        });
        return command;
    }

    private static void Render(
        Game game, IReadOnlyList<Draw> draws, int trainCount, int runCount, int seedValue, List<BacktestResult> results)
    {
        var inv = CultureInfo.InvariantCulture;
        var odds = game.Odds;
        var baseline = results.Single(r => r.Strategy == "random");
        var se = baseline.StandardError;
        var table = new Table().Border(TableBorder.Rounded)
            .Title($"Backtest {game.Name} {baseline.TestDraws} kỳ (#{draws[trainCount].DrawId}–#{draws[^1].DrawId}), {runCount} lần/kỳ, seed {seedValue}")
            .AddColumn("Chiến lược")
            .AddColumn(new TableColumn("Số trùng TB/kỳ").RightAligned())
            .AddColumn(new TableColumn("so với Random").RightAligned())
            .AddColumn(new TableColumn("Tỉ lệ ≥3 số").RightAligned());
        if (game.TicketHasSpecial) table.AddColumn(new TableColumn("Trùng ĐB").RightAligned());
        table.AddColumn(new TableColumn("Trúng giải").RightAligned());

        foreach (var r in results)
        {
            var diff = r.AverageMatches - baseline.AverageMatches;
            var cells = new List<string>
            {
                r.Strategy == "random" ? "random (baseline)" : r.Strategy,
                r.AverageMatches.ToString("F3", inv),
                r.Strategy == "random" ? "-" : diff.ToString("+0.000;-0.000;0.000", inv),
                r.RateAtLeast3.ToString("P2", inv),
            };
            if (game.TicketHasSpecial) cells.Add(r.SpecialHitRate!.Value.ToString("P2", inv));
            cells.Add(r.AnyPrizeRate.ToString("P2", inv));
            table.AddRow(cells.ToArray());
        }
        var theory = new List<string>
        {
            "[grey]lý thuyết[/]", $"[grey]{odds.ExpectedMatches.ToString("F3", inv)}[/]", "",
            $"[grey]{odds.ProbabilityAtLeast(3).ToString("P2", inv)}[/]",
        };
        if (game.TicketHasSpecial) theory.Add($"[grey]{(1.0 / game.SpecialMax).ToString("P2", inv)}[/]");
        theory.Add($"[grey]{odds.AnyPrizeProbability.ToString("P2", inv)}[/]");
        table.AddRow(theory.ToArray());
        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine(
            $"Với {baseline.TestDraws} kỳ kiểm tra, sai số chuẩn của số trùng TB ≈ [bold]{se.ToString("F2", inv)}[/] " +
            "(lặp nhiều lần trên cùng các kỳ không giảm sai số này).");
        var maxDiff = results.Max(r => Math.Abs(r.AverageMatches - baseline.AverageMatches));
        AnsiConsole.MarkupLine(maxDiff < 2 * se
            ? $"Chênh lệch lớn nhất so với Random là {maxDiff.ToString("F3", inv)} < 2×SE = {(2 * se).ToString("F2", inv)} → [yellow]không có lợi thế nào có ý nghĩa thống kê[/]."
            : $"Chênh lệch lớn nhất {maxDiff.ToString("F3", inv)} ≥ 2×SE trên mẫu nhỏ này; với nhiều chiến lược được thử, đây vẫn có thể là ngẫu nhiên.");
        Ui.WriteOdds(game);
        AnsiConsole.WriteLine();
    }
}
