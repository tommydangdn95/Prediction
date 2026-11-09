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
        var comparisons = results.Where(r => r.Strategy != "random")
            .Select(r => Backtester.Compare(r, baseline)).ToDictionary(c => c.Strategy);
        var correctedAlpha = comparisons.Count == 0 ? 0.05 : 0.05 / comparisons.Count;
        var table = new Table().Border(TableBorder.Rounded)
            .Title($"Backtest {game.Name} {baseline.TestDraws} kỳ (#{draws[trainCount].DrawId}–#{draws[^1].DrawId}), {runCount} lần/kỳ, seed {seedValue}")
            .AddColumn("Chiến lược")
            .AddColumn(new TableColumn("Số trùng TB/kỳ").RightAligned())
            .AddColumn(new TableColumn("so với Random").RightAligned())
            .AddColumn(new TableColumn("p ghép cặp").RightAligned())
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
                r.Strategy == "random" ? "-" : comparisons[r.Strategy].PValue.ToString("F3", inv),
                r.RateAtLeast3.ToString("P2", inv),
            };
            if (game.TicketHasSpecial) cells.Add(r.SpecialHitRate!.Value.ToString("P2", inv));
            cells.Add(r.AnyPrizeRate.ToString("P2", inv));
            table.AddRow(cells.ToArray());
        }
        var theory = new List<string>
        {
            "[grey]lý thuyết[/]", $"[grey]{odds.ExpectedMatches.ToString("F3", inv)}[/]", "", "",
            $"[grey]{odds.ProbabilityAtLeast(3).ToString("P2", inv)}[/]",
        };
        if (game.TicketHasSpecial) theory.Add($"[grey]{(1.0 / game.SpecialMax).ToString("P2", inv)}[/]");
        theory.Add($"[grey]{odds.AnyPrizeProbability.ToString("P2", inv)}[/]");
        table.AddRow(theory.ToArray());
        AnsiConsole.Write(table);

        var better = comparisons.Values.Where(c => c.MeanDifference > 0 && c.PValue < correctedAlpha).ToList();
        AnsiConsole.MarkupLine(
            $"p được tính từ chênh lệch ghép cặp theo {baseline.TestDraws} kỳ; ngưỡng Bonferroni 5% cho {comparisons.Count} so sánh: " +
            $"[bold]p < {correctedAlpha.ToString("F4", inv)}[/].");
        AnsiConsole.MarkupLine(better.Count == 0
            ? "[yellow]Không phát hiện chiến lược nào tốt hơn Random ở ngưỡng đã hiệu chỉnh[/]; " +
              (baseline.TestDraws < 500
                  ? "mẫu nhỏ nên kết quả chỉ mang tính thăm dò."
                  : $"trên {baseline.TestDraws} kỳ, chênh lệch so với Random đều rất nhỏ và không có ý nghĩa thống kê.")
            : $"[yellow]Có bằng chứng thăm dò rằng {Markup.Escape(string.Join(", ", better.Select(c => c.Strategy)))} tốt hơn Random trên mẫu này; cần kiểm chứng bằng dữ liệu mới.[/]");
        Ui.WriteOdds(game);
        AnsiConsole.WriteLine();
    }
}
