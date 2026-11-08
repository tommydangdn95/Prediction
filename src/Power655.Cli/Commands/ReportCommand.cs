using System.CommandLine;
using System.Globalization;
using System.Text;
using Power655.Core;
using Power655.Core.Strategies;
using Spectre.Console;

namespace Power655.Cli.Commands;

/// <summary>
/// Báo cáo Markdown cho các game được chọn: kỳ mới nhất, bộ số gợi ý mỗi chiến lược, backtest rút gọn,
/// bảng xác suất. Dùng cho workflow hằng ngày (file trong repo + GitHub step summary).
/// </summary>
internal static class ReportCommand
{
    private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

    public static Command Create(GlobalOptions globals)
    {
        var output = new Option<FileInfo?>("--out", "-o") { Description = "File Markdown đầu ra (bỏ trống = in ra màn hình)" };
        var sets = new Option<int>("--sets") { Description = "Số bộ mỗi chiến lược", DefaultValueFactory = _ => 2 };
        sets.Min(1);
        var seed = new Option<int?>("--seed") { Description = "Seed (mặc định: ngày hiện tại giờ VN dạng yyyyMMdd)" };
        var runs = new Option<int>("--runs") { Description = "Số lần lặp backtest mỗi kỳ (0 = bỏ backtest)", DefaultValueFactory = _ => 300 };
        runs.Min(0);
        var train = new Option<int>("--train") { Description = "Số kỳ train cho backtest", DefaultValueFactory = _ => 20 };
        train.Min(1);

        var command = new Command("report", "Xuất báo cáo gợi ý dạng Markdown cho các game được chọn.") { output, sets, seed, runs, train };
        command.SetAction(async (result, ct) =>
        {
            var now = DateTimeOffset.Now;
            var today = DateOnly.FromDateTime(DrawSchedule.ToVietnamTime(now));
            var seedValue = result.GetValue(seed) ?? int.Parse(today.ToString("yyyyMMdd", Inv), Inv);
            var data = await globals.LoadDrawsAsync(result, 0, ct);

            var markdown = Build(data, now, seedValue, result.GetValue(sets), result.GetValue(runs), result.GetValue(train));

            if (result.GetValue(output) is { } file)
            {
                if (file.Directory is { Exists: false } dir) dir.Create();
                await File.WriteAllTextAsync(file.FullName, markdown, new UTF8Encoding(false), ct);
                AnsiConsole.MarkupLine($"Đã ghi báo cáo [blue]{Markup.Escape(file.FullName)}[/].");
            }
            else
            {
                Console.Out.Write(markdown);
            }
            return data.Any(x => x.Draws.Count == 0) ? 2 : 0;
        });
        return command;
    }

    internal static string Build(
        IReadOnlyList<(Game Game, IReadOnlyList<Draw> Draws)> data,
        DateTimeOffset now, int seed, int sets, int runs, int train)
    {
        var local = DrawSchedule.ToVietnamTime(now);
        var sb = new StringBuilder();
        sb.AppendLine($"# Gợi ý Vietlott — {local.ToString("dd/MM/yyyy", Inv)}");
        sb.AppendLine();
        sb.AppendLine($"> **{Odds.Disclaimer}**");
        sb.AppendLine("> Mọi bộ số — kể cả bộ ngẫu nhiên — có cùng xác suất trúng. Bộ gợi ý chỉ phản ánh đặc điểm dữ liệu quá khứ.");
        sb.AppendLine();
        sb.AppendLine($"Tạo lúc {local.ToString("HH:mm dd/MM/yyyy", Inv)} (giờ VN) · seed `{seed}` · {sets} bộ mỗi chiến lược.");
        sb.AppendLine();

        foreach (var (game, draws) in data)
        {
            sb.AppendLine($"## {game.Name}");
            sb.AppendLine();
            if (draws.Count == 0)
            {
                sb.AppendLine("_Chưa có dữ liệu._");
                sb.AppendLine();
                continue;
            }

            var latest = draws[^1];
            var next = game.Schedule.NextFetchTime(now);
            sb.AppendLine($"- Kỳ mới nhất: **#{latest.DrawId:D5}** {latest.Date.ToString("dd/MM/yyyy", Inv)} — `{Balls(latest)}`");
            sb.AppendLine($"- Kết quả kỳ kế tiếp có sau: {DrawSchedule.ToVietnamTime(next).ToString("HH:mm dd/MM/yyyy", Inv)}");
            sb.AppendLine($"- Lịch sử dùng để gợi ý: {draws.Count} kỳ (#{draws[0].DrawId:D5} → #{latest.DrawId:D5})");
            sb.AppendLine();

            WriteSuggestions(sb, game, draws, seed, sets);
            if (runs > 0) WriteBacktest(sb, game, draws, seed, runs, train);
            WriteOdds(sb, game);
            WriteRecent(sb, game, draws);
        }
        return sb.ToString();
    }

    private static void WriteSuggestions(StringBuilder sb, Game game, IReadOnlyList<Draw> history, int seed, int sets)
    {
        var rng = new Random(SuggestCommand.GameSeed(seed, game));
        sb.Append("| Chiến lược | Bộ số |");
        if (game.TicketHasSpecial) sb.Append(" ĐB |");
        sb.AppendLine($" Chẵn/Lẻ | Nhỏ/Lớn (≤{game.LowHighBoundary}) | Tổng |");
        sb.Append("|---|---|");
        if (game.TicketHasSpecial) sb.Append("---|");
        sb.AppendLine("---|---|---:|");

        // random cuối cùng làm đối chứng
        foreach (var name in new[] { "hot", "cold", "balanced", "random" })
        {
            var strategy = StrategyCatalog.Create(name);
            for (var i = 0; i < sets; i++)
            {
                var t = strategy.Pick(game, history, rng);
                var even = t.Numbers.Count(n => n % 2 == 0);
                var low = t.Numbers.Count(n => n <= game.LowHighBoundary);
                var label = name == "random" ? "random (đối chứng)" : name;
                sb.Append($"| {label} | `{Ui.Balls(t.Numbers)}` |");
                if (game.TicketHasSpecial) sb.Append($" `{Ui.Ball(t.Special!.Value)}` |");
                sb.AppendLine($" {even}/{t.Numbers.Count - even} | {low}/{t.Numbers.Count - low} | {t.Numbers.Sum()} |");
            }
        }
        sb.AppendLine();
    }

    private static void WriteBacktest(StringBuilder sb, Game game, IReadOnlyList<Draw> draws, int seed, int runs, int train)
    {
        if (draws.Count <= train)
        {
            sb.AppendLine($"_Backtest: cần hơn {train} kỳ (có {draws.Count})._");
            sb.AppendLine();
            return;
        }
        var results = StrategyCatalog.All().Select(s => Backtester.Run(game, draws, s, train, runs, seed)).ToList();
        var baseline = results.Single(r => r.Strategy == "random");
        var comparisons = results.Where(r => r.Strategy != "random")
            .Select(r => Backtester.Compare(r, baseline)).ToDictionary(c => c.Strategy);
        var correctedAlpha = comparisons.Count == 0 ? 0.05 : 0.05 / comparisons.Count;

        sb.AppendLine("<details><summary>Backtest walk-forward " +
                      $"({baseline.TestDraws} kỳ, {runs} lần/kỳ)</summary>");
        sb.AppendLine();
        sb.AppendLine("| Chiến lược | Số trùng TB/kỳ | so với Random | p ghép cặp | Trúng giải |");
        sb.AppendLine("|---|---:|---:|---:|---:|");
        foreach (var r in results)
            sb.AppendLine($"| {r.Strategy} | {r.AverageMatches.ToString("F3", Inv)} | " +
                          $"{(r.Strategy == "random" ? "-" : (r.AverageMatches - baseline.AverageMatches).ToString("+0.000;-0.000;0.000", Inv))} | " +
                          $"{(r.Strategy == "random" ? "-" : comparisons[r.Strategy].PValue.ToString("F3", Inv))} | " +
                          $"{r.AnyPrizeRate.ToString("P2", Inv)} |");
        sb.AppendLine($"| _lý thuyết_ | {game.Odds.ExpectedMatches.ToString("F3", Inv)} | | | {game.Odds.AnyPrizeProbability.ToString("P2", Inv)} |");
        sb.AppendLine();
        var better = comparisons.Values.Where(c => c.MeanDifference > 0 && c.PValue < correctedAlpha).ToList();
        sb.AppendLine($"p dùng chênh lệch ghép cặp theo từng kỳ; ngưỡng Bonferroni 5%: p < {correctedAlpha.ToString("F4", Inv)}. " +
                      (better.Count == 0
                          ? "**Không phát hiện chiến lược nào tốt hơn Random ở ngưỡng đã hiệu chỉnh**; mẫu nhỏ nên kết quả chỉ mang tính thăm dò."
                          : $"Có bằng chứng thăm dò rằng **{string.Join(", ", better.Select(c => c.Strategy))}** tốt hơn Random trên mẫu này; cần kiểm chứng bằng dữ liệu mới."));
        sb.AppendLine();
        sb.AppendLine("</details>");
        sb.AppendLine();
    }

    private static void WriteOdds(StringBuilder sb, Game game)
    {
        sb.AppendLine("<details><summary>Xác suất thật mỗi vé</summary>");
        sb.AppendLine();
        sb.AppendLine("| Giải | Điều kiện | Xác suất / vé |");
        sb.AppendLine("|---|---|---:|");
        foreach (var o in game.Odds.Table)
            sb.AppendLine($"| {o.Prize} | {o.Condition} | 1 / {o.OneIn.ToString("N0", Ui.Vi)} |");
        sb.AppendLine($"| _Trúng giải bất kỳ_ | | 1 / {(1 / game.Odds.AnyPrizeProbability).ToString("N1", Ui.Vi)} |");
        sb.AppendLine();
        sb.AppendLine("</details>");
        sb.AppendLine();
    }

    private static void WriteRecent(StringBuilder sb, Game game, IReadOnlyList<Draw> draws)
    {
        sb.AppendLine("<details><summary>5 kỳ gần nhất</summary>");
        sb.AppendLine();
        sb.AppendLine("| Kỳ | Ngày | Kết quả | Jackpot 1 |");
        sb.AppendLine("|---|---|---|---:|");
        foreach (var d in draws.TakeLast(5).Reverse())
            sb.AppendLine($"| #{d.DrawId:D5} | {d.Date.ToString("dd/MM/yyyy", Inv)} | `{Balls(d)}` | {Ui.Money(d.Jackpot1)} |");
        sb.AppendLine();
        sb.AppendLine("</details>");
        sb.AppendLine();
    }

    private static string Balls(Draw d) =>
        Ui.Balls(d.Numbers) + (d.Special is { } s ? $" | {Ui.Ball(s)}" : "");
}
