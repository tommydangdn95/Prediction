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
        var archive = new Option<DirectoryInfo?>("--predictions") { Description = "Thư mục báo cáo cũ để đối chiếu với kết quả thật (mặc định: predictions)" };
        var cover = new Option<int?>("--cover") { Description = "Số vé bộ phủ số mỗi game (mặc định = tổng số bộ gợi ý = 4 × --sets; 0 = bỏ)" };
        cover.Validators.Add(r =>
        {
            if (r.GetValueOrDefault<int?>() is < 0) r.AddError("--cover phải ≥ 0.");
        });

        var command = new Command("report", "Xuất báo cáo gợi ý dạng Markdown cho các game được chọn.") { output, sets, seed, runs, train, archive, cover };
        command.SetAction(async (result, ct) =>
        {
            var now = DateTimeOffset.Now;
            var today = DateOnly.FromDateTime(DrawSchedule.ToVietnamTime(now));
            var seedValue = result.GetValue(seed) ?? int.Parse(today.ToString("yyyyMMdd", Inv), Inv);
            var data = await globals.LoadDrawsAsync(result, 0, ct);

            var archiveDir = result.GetValue(archive)?.FullName ?? "predictions";
            var reviews = PredictionArchive.Review(PredictionArchive.Load(archiveDir, today), data.SelectMany(x => x.Draws));

            var markdown = Build(data, now, seedValue, result.GetValue(sets), result.GetValue(runs), result.GetValue(train), reviews,
                result.GetValue(cover));

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
        DateTimeOffset now, int seed, int sets, int runs, int train,
        IReadOnlyList<PredictionReview>? reviews = null, int? cover = null)
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
            // kỳ của hôm nay có thể đã có kết quả trước mốc dự kiến (vd 21:04 đã có kỳ 21:00)
            var next = game.Schedule.NextFetchTime(now, draws.Count(d => d.Date == DateOnly.FromDateTime(local)));
            sb.AppendLine($"- Kỳ mới nhất: **#{latest.DrawId:D5}** {latest.Date.ToString("dd/MM/yyyy", Inv)} — `{Balls(latest)}`");
            sb.AppendLine($"- Kết quả kỳ kế tiếp có sau: {DrawSchedule.ToVietnamTime(next).ToString("HH:mm dd/MM/yyyy", Inv)}");
            sb.AppendLine($"- Lịch sử dùng để gợi ý: {draws.Count} kỳ (#{draws[0].DrawId:D5} → #{latest.DrawId:D5})");
            WriteFrequency(sb, game, draws);
            sb.AppendLine();

            WriteReview(sb, game, reviews ?? []);

            // bộ theo thống kê sinh trước (cùng RNG như mọi phiên bản trước) nhưng in sau bộ khuyến nghị
            var statistical = new StringBuilder();
            var suggested = WriteSuggestions(statistical, game, draws, seed, sets);
            var coverCount = cover ?? suggested.Count;
            var groups = runs > 0 && coverCount > 0 && draws.Count > train
                ? RunGroupBacktests(game, draws, coverCount, runs, train, seed)
                : [];
            var coverExact = WriteCover(sb, game, seed, coverCount, suggested, groups);

            sb.AppendLine("### Bộ theo thống kê (tham khảo)");
            sb.AppendLine();
            sb.AppendLine("Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). " +
                          "Backtest bên dưới cho thấy các cách này không trúng nhiều hơn chọn ngẫu nhiên.");
            sb.AppendLine();
            sb.Append(statistical);
            WriteTicketChances(sb, game);
            if (runs > 0) WriteBacktest(sb, game, draws, seed, runs, train, groups, coverExact);
            WriteOdds(sb, game);
            WriteRecent(sb, game, draws);
        }
        return sb.ToString();
    }

    /// <summary>Tỉ lệ xuất hiện của từng số trên toàn bộ lịch sử và kiểm định χ² xem có lệch khỏi phân phối đều không.</summary>
    private static void WriteFrequency(StringBuilder sb, Game game, IReadOnlyList<Draw> draws)
    {
        var freq = DrawStatistics.Frequency(game, draws);
        var most = freq.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).First();
        var least = freq.OrderBy(kv => kv.Value).ThenBy(kv => kv.Key).First();
        double Rate(int count) => (double)count / draws.Count;
        sb.AppendLine($"- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng {Percent((double)game.PickCount / game.MaxNumber)} số kỳ; " +
                      $"thực tế từ {Percent(Rate(least.Value))} (số {Ui.Ball(least.Key)}) đến {Percent(Rate(most.Value))} (số {Ui.Ball(most.Key)}).");
        var chi = DrawStatistics.ChiSquareUniform(game, draws);
        sb.AppendLine($"- Kiểm định ngẫu nhiên χ² trên {draws.Count} kỳ: p = {chi.PValue.ToString("F3", Inv)} → " +
                      (chi.PValue < 0.05
                          ? "**lệch so với phân phối đều** (p < 0,05 vẫn xảy ra 1/20 lần dù máy quay hoàn toàn ngẫu nhiên — cần theo dõi thêm)."
                          : "chênh lệch trên nằm trong dao động ngẫu nhiên — tần suất quá khứ không giúp dự đoán kỳ sau."));
    }

    /// <summary>Bảng bộ số theo từng chiến lược; trả về các vé theo thứ tự in.</summary>
    private static List<Ticket> WriteSuggestions(StringBuilder sb, Game game, IReadOnlyList<Draw> history, int seed, int sets)
    {
        var tickets = new List<Ticket>();
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
                tickets.Add(t);
                var even = t.Numbers.Count(n => n % 2 == 0);
                var low = t.Numbers.Count(n => n <= game.LowHighBoundary);
                var label = name == "random" ? "random (đối chứng)" : name;
                sb.Append($"| {label} | `{Ui.Balls(t.Numbers)}` |");
                if (game.TicketHasSpecial) sb.Append($" `{Ui.Ball(t.Special!.Value)}` |");
                sb.AppendLine($" {even}/{t.Numbers.Count - even} | {low}/{t.Numbers.Count - low} | {t.Numbers.Sum()} |");
            }
        }
        sb.AppendLine();
        return tickets;
    }

    /// <summary>
    /// Bộ phủ số: các vé ít trùng nhau nhất → P(ít nhất một vé trúng) cao nhất cho cùng số vé.
    /// Tính chính xác bằng <see cref="PortfolioOdds"/>, so với cùng số vé ngẫu nhiên và với các bộ gợi ý ở trên.
    /// </summary>
    private static double? WriteCover(
        StringBuilder sb, Game game, int seed, int count, IReadOnlyList<Ticket> suggested, IReadOnlyList<GroupBacktestResult> groups)
    {
        if (count <= 0) return null;
        // RNG riêng để các bộ gợi ý ở trên giữ nguyên như trước khi có bộ phủ số
        var rng = new Random(unchecked(SuggestCommand.GameSeed(seed, game) * 7919 + 1));
        var tickets = CoverPicker.Pick(game, count, rng);
        var any = PortfolioOdds.AnyPrizeByPrefix(game, tickets);
        var three = game.TicketHasSpecial ? PortfolioOdds.AtLeastByPrefix(game, tickets, 3) : null;

        var p = game.Odds.AnyPrizeProbability;
        sb.AppendLine($"### Bộ khuyến nghị — phủ số ({count} vé)");
        sb.AppendLine();
        sb.AppendLine("Các vé không trùng số nhau" + (game.TicketHasSpecial ? " và có số ĐB khác nhau" : "") +
                      " nên ít khi cùng trượt — cách chọn duy nhất có lợi thế đo được.");
        sb.AppendLine();
        // ví dụ theo thói quen mua 5 vé mỗi kỳ
        var example = Math.Min(5, count);
        var exampleAny = any[example - 1];
        var exampleRange = example == 1 ? "cover 1" : $"cover 1–{example}";
        sb.AppendLine("**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua " +
                      $"**từ vé 1 đến vé ở dòng đó**. Ví dụ mua {example} vé → mua {exampleRange} → xem dòng cover {example}: " +
                      $"**{Percent(exampleAny)}**" +
                      (1 / exampleAny >= 2 ? $", tức khoảng 1 trong {Math.Round(1 / exampleAny).ToString("N0", Ui.Vi)} kỳ có vé trúng" : "") +
                      $" ({example} vé chọn ngẫu nhiên: {Percent(PortfolioOdds.IndependentAnyPrize(game, example))}). " +
                      $"Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng {Percent(p)}.");
        sb.AppendLine();
        sb.Append("| Vé | Bộ số |");
        if (game.TicketHasSpecial) sb.Append(" ĐB |");
        sb.Append(" Mua từ vé 1 đến vé này: có ≥1 vé trúng |");
        if (three is not null) sb.Append(" …có ≥1 vé trùng ≥3 số |");
        sb.AppendLine(" Cùng số vé chọn ngẫu nhiên |");
        sb.Append("|---|---|");
        if (game.TicketHasSpecial) sb.Append("---|");
        sb.Append("---:|");
        if (three is not null) sb.Append("---:|");
        sb.AppendLine("---:|");
        for (var k = 1; k <= tickets.Count; k++)
        {
            var t = tickets[k - 1];
            sb.Append($"| cover {k} | `{Ui.Balls(t.Numbers)}` |");
            if (game.TicketHasSpecial) sb.Append($" `{Ui.Ball(t.Special!.Value)}` |");
            sb.Append($" **{Percent(any[k - 1])}** |");
            if (three is not null) sb.Append($" {Percent(three[k - 1])} |");
            sb.AppendLine($" {Percent(PortfolioOdds.IndependentAnyPrize(game, k))} |");
        }
        sb.AppendLine();

        var n = suggested.Count;
        if (n > 0)
        {
            var suggestedAny = PortfolioOdds.AnyPrizeByPrefix(game, suggested)[^1];
            sb.AppendLine($"- Cùng {n} vé: {n} bộ theo thống kê ở bảng dưới → {Percent(suggestedAny)}; " +
                          $"{n} vé chọn ngẫu nhiên → {Percent(PortfolioOdds.IndependentAnyPrize(game, n))}" +
                          (n <= tickets.Count ? $"; {n} vé phủ số → **{Percent(any[n - 1])}**." : "."));
        }
        if (groups.FirstOrDefault(g => g.Strategy == CoverPicker.Name) is { } real)
        {
            var others = string.Join(", ", groups.Where(g => g.Strategy != CoverPicker.Name)
                .Select(g => $"{g.Strategy} {Percent(g.AnyWinRate)}"));
            sb.AppendLine($"- Kiểm chứng trên {real.TestDraws} kỳ đã quay: mỗi kỳ mua {real.GroupSize} vé phủ số → " +
                          $"**{Percent(real.AnyWinRate)}** số kỳ có vé trúng; {real.GroupSize} vé theo {others}.");
        }
        sb.AppendLine("- " + GameCoverNote(game, example));
        var top = game.Odds.Table[0];
        sb.AppendLine($"- {top.Prize} ({top.Condition}): {count} vé khác nhau có khả năng đúng bằng {count} lần một vé — " +
                      $"khoảng 1 trên {(1 / (count * top.Probability)).ToString("N0", Ui.Vi)}, chọn số kiểu gì cũng vậy.");
        sb.AppendLine("- Tiền thưởng trung bình không đổi theo cách chọn số; phủ số chỉ làm giảm số kỳ cả nhóm vé cùng trắng tay.");
        sb.AppendLine();
        return any[^1];
    }

    /// <summary>Vì sao phủ số giúp nhiều hay ít ở từng game.</summary>
    private static string GameCoverNote(Game game, int example)
    {
        // hạng giải dễ trúng nhất chiếm phần lớn "trúng giải bất kỳ"
        var common = game.Odds.Table.MaxBy(o => o.Probability)!;
        var share = Math.Round(common.Probability / game.Odds.AnyPrizeProbability * 100).ToString("N0", Ui.Vi) + " %";
        return game.Special switch
        {
            SpecialKind.SeparatePool =>
                $"Với {game.Name}: giải {common.Prize} ({common.Condition}) chiếm {share} số lần trúng và chỉ cần trùng số ĐB " +
                $"(01–{game.SpecialMax:D2}), nên số ĐB khác nhau là lợi thế chính: {example} vé phủ {example}/{game.SpecialMax} " +
                $"khả năng của số ĐB; {game.SpecialMax} vé có đủ {game.SpecialMax} số ĐB thì kỳ nào cũng có vé trúng.",
            SpecialKind.BonusFromMainPool =>
                $"Với {game.Name}: số ĐB là quả thứ 7 quay cùng lồng, chỉ dùng cho giải 5 số + ĐB, nên không có giải nào chỉ cần " +
                $"trùng ĐB; {share} số lần trúng là {common.Prize} ({common.Condition}). Phủ số vì vậy chỉ hơn chọn ngẫu nhiên " +
                "một chút; khác biệt rõ hơn khi so với hot/cold (cả nhóm vé lấy từ cùng một nhóm số nên hay cùng trượt).",
            _ =>
                $"Với {game.Name}: vé không có số ĐB và {share} số lần trúng là {common.Prize} ({common.Condition}). " +
                "Phủ số vì vậy chỉ hơn chọn ngẫu nhiên một chút; khác biệt rõ hơn khi so với hot/cold " +
                "(cả nhóm vé lấy từ cùng một nhóm số nên hay cùng trượt).",
        };
    }

    /// <summary>Mua nhóm <paramref name="groupSize"/> vé mỗi kỳ trên lịch sử thật: phủ số trước, random cuối.</summary>
    private static IReadOnlyList<GroupBacktestResult> RunGroupBacktests(
        Game game, IReadOnlyList<Draw> draws, int groupSize, int runs, int train, int seed)
    {
        // cùng số vé mỗi kỳ như backtest từng vé (runs), chia thành các nhóm groupSize vé
        var groups = Math.Max(1, runs / groupSize);
        var strategies = StrategyCatalog.All().OrderBy(s => s.Name == "random" ? 1 : 0).ToList();
        return [Backtester.RunCover(game, draws, groupSize, train, groups, seed),
                .. strategies.Select(s => Backtester.RunGroups(game, draws, s, groupSize, train, groups, seed))];
    }

    private static void WriteBacktest(
        StringBuilder sb, Game game, IReadOnlyList<Draw> draws, int seed, int runs, int train,
        IReadOnlyList<GroupBacktestResult> groups, double? coverExact)
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
                          ? "**Không phát hiện chiến lược nào tốt hơn Random ở ngưỡng đã hiệu chỉnh**; " +
                            (baseline.TestDraws < 500
                                ? "mẫu nhỏ nên kết quả chỉ mang tính thăm dò."
                                : $"trên {baseline.TestDraws} kỳ, chênh lệch so với Random đều rất nhỏ và không có ý nghĩa thống kê.")
                          : $"Có bằng chứng thăm dò rằng **{string.Join(", ", better.Select(c => c.Strategy))}** tốt hơn Random trên mẫu này; cần kiểm chứng bằng dữ liệu mới."));
        sb.AppendLine();
        if (groups.Count > 0)
        {
            var size = groups[0].GroupSize;
            sb.AppendLine($"**Mua {size} vé mỗi kỳ** trên {groups[0].TestDraws} kỳ thật ({groups[0].Groups} lần/kỳ):");
            sb.AppendLine();
            sb.AppendLine($"| Cách chọn {size} vé | Kỳ có ≥1 vé trúng | Lý thuyết | Vé trúng TB/kỳ |");
            sb.AppendLine("|---|---:|---:|---:|");
            foreach (var g in groups)
            {
                var theory = g.Strategy switch
                {
                    CoverPicker.Name when coverExact is { } c => Percent(c),
                    "random" => Percent(PortfolioOdds.IndependentAnyPrize(game, size)),
                    _ => "",
                };
                sb.AppendLine($"| {g.Strategy} | {Percent(g.AnyWinRate)} | {theory} | {g.AverageWinningTickets.ToString("F3", Inv)} |");
            }
            sb.AppendLine();
            sb.AppendLine($"Vé trúng TB/kỳ như nhau với mọi cách chọn (lý thuyết {size} × {Percent(game.Odds.AnyPrizeProbability)} = " +
                          $"{(size * game.Odds.AnyPrizeProbability).ToString("F3", Inv)}); chỉ tỉ lệ kỳ có vé trúng là thay đổi.");
            sb.AppendLine();
        }
        sb.AppendLine("</details>");
        sb.AppendLine();
    }

    private static void WriteOdds(StringBuilder sb, Game game)
    {
        sb.AppendLine("<details><summary>Xác suất thật mỗi vé</summary>");
        sb.AppendLine();
        sb.AppendLine("| Giải | Điều kiện | Xác suất / vé | % |");
        sb.AppendLine("|---|---|---:|---:|");
        foreach (var o in game.Odds.Table)
            sb.AppendLine($"| {o.Prize} | {o.Condition} | 1 / {o.OneIn.ToString("N0", Ui.Vi)} | {Percent(o.Probability)} |");
        sb.AppendLine($"| _Trúng giải bất kỳ_ | | 1 / {(1 / game.Odds.AnyPrizeProbability).ToString("N1", Ui.Vi)} | {Percent(game.Odds.AnyPrizeProbability)} |");
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

    /// <summary>
    /// Khả năng (%) cho MỖI bộ số gợi ý ở kỳ tới. Mọi bộ hợp lệ có cùng phân phối, nên bảng áp dụng cho từng dòng ở trên.
    /// </summary>
    private static void WriteTicketChances(StringBuilder sb, Game game)
    {
        var odds = game.Odds;
        sb.AppendLine($"**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): " +
                      $"trúng giải bất kỳ **{Percent(odds.AnyPrizeProbability)}** · " +
                      $"trúng ≥3 số **{Percent(odds.ProbabilityAtLeast(3))}** · " +
                      $"{odds.Table[0].Prize} **{Percent(odds.Table[0].Probability)}** (1 / {odds.Table[0].OneIn.ToString("N0", Ui.Vi)}).");
        sb.AppendLine();
        sb.Append("| Trùng số chính |");
        for (var k = 0; k <= game.PickCount; k++) sb.Append($" {k} |");
        sb.AppendLine();
        sb.Append("|---|");
        for (var k = 0; k <= game.PickCount; k++) sb.Append("---:|");
        sb.AppendLine();
        sb.Append("| Xác suất |");
        for (var k = 0; k <= game.PickCount; k++) sb.Append($" {Percent(odds.ProbabilityExactMatches(k))} |");
        sb.AppendLine();
        if (game.TicketHasSpecial)
        {
            sb.AppendLine();
            sb.AppendLine($"Số ĐB trùng: {Percent(1.0 / game.SpecialMax)} (độc lập với số chính).");
        }
        sb.AppendLine();
    }

    /// <summary>Đối chiếu các bộ đã gợi ý ở báo cáo trước với kết quả thật.</summary>
    private static void WriteReview(StringBuilder sb, Game game, IReadOnlyList<PredictionReview> reviews)
    {
        var mine = reviews.Where(r => r.Set.Game == game).ToList();
        if (mine.Count == 0) return;

        var last = mine[^1];
        var d = last.Draw;
        sb.AppendLine($"### Kiểm tra gợi ý ngày {last.Set.Date.ToString("dd/MM/yyyy", Inv)} → kỳ #{d.DrawId:D5} ({d.Date.ToString("dd/MM/yyyy", Inv)}): `{Balls(d)}`");
        sb.AppendLine();
        sb.Append("| Chiến lược | Bộ số |");
        if (game.TicketHasSpecial) sb.Append(" ĐB |");
        sb.AppendLine(" Trùng | Kết quả |");
        sb.Append("|---|---|");
        if (game.TicketHasSpecial) sb.Append("---|");
        sb.AppendLine("---|---|");
        foreach (var t in last.Tickets)
        {
            var hits = t.Hits.ToHashSet();
            var nums = string.Join(" ", t.Suggestion.Ticket.Numbers.Select(n => hits.Contains(n) ? $"**{Ui.Ball(n)}**" : Ui.Ball(n)));
            sb.Append($"| {t.Suggestion.Strategy} | {nums} |");
            if (game.TicketHasSpecial)
            {
                var sp = Ui.Ball(t.Suggestion.Ticket.Special!.Value);
                sb.Append(t.Special ? $" **{sp}** ✓ |" : $" {sp} |");
            }
            var matchText = $"{t.Main}/{game.PickCount}" + (t.Special ? " + ĐB" : "");
            sb.AppendLine($" {matchText} | {(t.Prize is { } p ? $"🎉 {p.Name}" : "-")} |");
        }
        var n = last.Tickets.Count;
        sb.AppendLine();
        sb.AppendLine($"Kỳ này: {last.Tickets.Sum(t => t.Main)} số trùng trên {n} bộ " +
                      $"(kỳ vọng ngẫu nhiên {(n * game.Odds.ExpectedMatches).ToString("F1", Inv)}), " +
                      $"{last.Tickets.Count(t => t.Prize is not null)} bộ trúng giải " +
                      $"(kỳ vọng {(n * game.Odds.AnyPrizeProbability).ToString("F1", Inv)}).");
        sb.AppendLine();

        var scores = StrategyScore.Summarize(game, mine);
        sb.AppendLine($"<details><summary>Thành tích thực tế cộng dồn ({mine.Count} kỳ đã đối chiếu)</summary>");
        sb.AppendLine();
        sb.Append("| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng |");
        if (game.TicketHasSpecial) sb.Append(" Trùng ĐB |");
        sb.AppendLine();
        sb.Append("|---|---:|---:|---:|---:|---:|");
        if (game.TicketHasSpecial) sb.Append("---:|");
        sb.AppendLine();
        foreach (var s in scores)
        {
            sb.Append($"| {s.Strategy} | {s.Tickets} | {s.AverageMatches.ToString("F3", Inv)} | {game.Odds.ExpectedMatches.ToString("F3", Inv)} | " +
                      $"{s.Prizes} | {s.ExpectedPrizes.ToString("F1", Inv)} |");
            if (game.TicketHasSpecial) sb.Append($" {s.SpecialHits}/{s.Tickets} |");
            sb.AppendLine();
        }
        sb.AppendLine();
        sb.AppendLine("Chênh lệch giữa các chiến lược trên vài chục vé là dao động ngẫu nhiên bình thường; cần hàng trăm kỳ mới đánh giá được.");
        sb.AppendLine();
        sb.AppendLine("</details>");
        sb.AppendLine();
    }

    /// <summary>Phần trăm với đủ chữ số có nghĩa cho cả xác suất rất nhỏ (vd 0,0000257 %).</summary>
    private static string Percent(double p) => Ui.Percent(p);

    private static string Balls(Draw d) =>
        Ui.Balls(d.Numbers) + (d.Special is { } s ? $" | {Ui.Ball(s)}" : "");
}
