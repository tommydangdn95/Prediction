using System.CommandLine;
using Power655.Core;
using Power655.Core.Strategies;
using Spectre.Console;

namespace Power655.Cli.Commands;

internal static class BacktestCommand
{
    public static Command Create(GlobalOptions globals)
    {
        var train = new Option<int>("--train") { Description = "Số kỳ đầu dùng làm lịch sử", DefaultValueFactory = _ => 20 };
        train.Min(1);
        var runs = new Option<int>("--runs") { Description = "Số lần lấy mẫu từng vé/kỳ", DefaultValueFactory = _ => 1000 };
        runs.Min(1);
        var seed = new Option<int>("--seed") { Description = "Seed", DefaultValueFactory = _ => 42 };
        var last = new Option<int>("--last") { Description = "Giới hạn lịch sử N kỳ gần nhất (0 = tất cả)", DefaultValueFactory = _ => 0 };
        last.Min(0);
        var size = new Option<int>("--group-size") { Description = "Số vé khác nhau mỗi phương án mua/kỳ", DefaultValueFactory = _ => 5 };
        size.Min(1);
        var groups = new Option<int>("--groups") { Description = "Số nhóm mô phỏng mỗi kỳ", DefaultValueFactory = _ => 50 };
        groups.Min(1);
        var from = new Option<int?>("--from-draw") { Description = "Đánh giá từ ID kỳ này; giữ lịch sử trước đó để chọn số" };
        from.Validators.Add(r => { if (r.GetValueOrDefault<int?>() is <= 0) r.AddError("--from-draw phải > 0."); });
        var command = new Command("backtest", "Đánh giá trúng giải và tiền giải cơ bản, cùng ngân sách, theo từng kỳ thật.")
            { train, runs, seed, last, size, groups, from };
        command.SetAction(async (result, ct) =>
        {
            var trainCount = result.GetValue(train);
            var data = await globals.LoadDrawsAsync(result, result.GetValue(last), ct);
            var exit = 0;
            foreach (var (game, draws) in data)
            {
                Ui.GameHeader(game);
                if (draws.Count <= trainCount)
                {
                    Ui.Error($"{game.Name}: cần nhiều hơn {trainCount} kỳ (có {draws.Count}).");
                    exit = 1;
                    continue;
                }
                var singles = new List<BacktestResult>();
                var portfolios = new List<GroupBacktestResult>();
                AnsiConsole.Status().Start($"Đang backtest {game.Name}...", _ =>
                {
                    foreach (var strategy in StrategyCatalog.All())
                    {
                        singles.Add(Backtester.Run(game, draws, strategy, trainCount, result.GetValue(runs),
                            result.GetValue(seed), result.GetValue(from)));
                        portfolios.Add(Backtester.RunGroups(game, draws, strategy, result.GetValue(size), trainCount,
                            result.GetValue(groups), result.GetValue(seed), result.GetValue(from)));
                    }
                    portfolios.Add(Backtester.RunCover(game, draws, result.GetValue(size), trainCount,
                        result.GetValue(groups), result.GetValue(seed), result.GetValue(from)));
                });
                BacktestTables.WriteConsole(singles, portfolios, data.Count);
                if (singles[0].TestDraws == 0) exit = 1;
                Ui.WriteOdds(game);
            }
            return exit;
        });
        return command;
    }
}