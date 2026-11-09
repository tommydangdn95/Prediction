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
        var archive = new Option<DirectoryInfo?>("--predictions") { Description = "Thư mục lưu sổ dự đoán theo kỳ (mặc định: predictions; chỉ ghi khi có --out)" };
        var from = new Option<int?>("--from-draw") { Description = "Chỉ đánh giá từ ID kỳ này; giữ lịch sử trước đó để chọn số" };
        from.Validators.Add(r => { if (r.GetValueOrDefault<int?>() is <= 0) r.AddError("--from-draw phải > 0."); });
        var cover = new Option<int?>("--cover") { Description = "Số vé bộ phủ số mỗi game (mặc định = tổng số bộ gợi ý = 4 × --sets; 0 = bỏ)" };
        cover.Validators.Add(r =>
        {
            if (r.GetValueOrDefault<int?>() is < 0) r.AddError("--cover phải ≥ 0.");
        });

        var command = new Command("report", "Xuất báo cáo gợi ý dạng Markdown cho các game được chọn.") { output, sets, seed, runs, train, archive, cover, from };
        command.SetAction(async (result, ct) =>
        {
            var now = DateTimeOffset.Now;
            var today = DateOnly.FromDateTime(DrawSchedule.ToVietnamTime(now));
            var seedValue = result.GetValue(seed) ?? int.Parse(today.ToString("yyyyMMdd", Inv), Inv);
            var data = await globals.LoadDrawsAsync(result, 0, ct);

            var archiveDir = result.GetValue(archive)?.FullName ?? "predictions";
            var saved = PredictionLedger.Load(archiveDir);
            var reviews = PredictionLedger.Review(saved, data.SelectMany(x => x.Draws));
            var forecasts = new Dictionary<string, LedgerPrediction>();
            var setCount = result.GetValue(sets);
            var coverCount = result.GetValue(cover) ?? checked(4 * setCount);
            var persist = result.GetValue(output) is not null;
            var version = CodeVersion();
            foreach (var (game, draws) in data)
            {
                var assessment = PredictionLedger.AssessTarget(game, draws, DateTimeOffset.Now);
                if (!assessment.IsReady) continue;
                var existing = saved.FirstOrDefault(p => p.GameCode == game.Code && p.TargetDrawId == assessment.TargetDrawId);
                if (existing is not null)
                {
                    forecasts[game.Code] = existing;
                    continue;
                }
                var tickets = CreateSuggestions(game, draws, seedValue, setCount, coverCount);
                // Stamp after generating the tickets; Create checks the cutoff again.
                if (!PredictionLedger.AssessTarget(game, draws, DateTimeOffset.Now).IsReady) continue;
                try
                {
                    var proposal = PredictionLedger.Create(game, draws, DateTimeOffset.Now, seedValue,
                        $"sets={setCount};cover={coverCount};unique-v2", version, tickets);
                    forecasts[game.Code] = persist ? PredictionLedger.SaveOrLoad(archiveDir, proposal) : proposal;
                }
                catch (InvalidOperationException) when (!PredictionLedger.AssessTarget(game, draws, DateTimeOffset.Now).IsReady)
                {
                    // The draw started while saving. Keep valid forecasts for the other games.
                }
            }

            var markdown = Build(data, now, seedValue, result.GetValue(sets), result.GetValue(runs), result.GetValue(train), reviews,
                result.GetValue(cover), forecasts, persist, result.GetValue(from));

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
            return data.Any(x => !forecasts.ContainsKey(x.Game.Code)) ? 2 : 0;
        });
        return command;
    }

    /// <summary>Biến môi trường chứa commit SHA (workflow đặt) để biết vé được tạo từ code nào.</summary>
    internal const string CodeVersionEnvVar = "POWER655_CODE_VERSION";

    /// <summary>
    /// Commit SHA khi chạy trên GitHub Actions; luôn kèm định danh build của hai assembly để phân biệt
    /// cả khi chạy ở máy với code chưa commit.
    /// </summary>
    internal static string CodeVersion()
    {
        var build = $"cli:{typeof(ReportCommand).Assembly.ManifestModule.ModuleVersionId:D};" +
                    $"core:{typeof(Backtester).Assembly.ManifestModule.ModuleVersionId:D}";
        return Environment.GetEnvironmentVariable(CodeVersionEnvVar) is { Length: > 0 } sha
            ? $"git:{sha.Trim()};{build}"
            : build;
    }

    internal static string Build(
        IReadOnlyList<(Game Game, IReadOnlyList<Draw> Draws)> data,
        DateTimeOffset now, int seed, int sets, int runs, int train,
        IReadOnlyList<PredictionReview>? reviews = null, int? cover = null,
        IReadOnlyDictionary<string, LedgerPrediction>? forecasts = null, bool persisted = false, int? testFromDrawId = null)
    {
        var local = DrawSchedule.ToVietnamTime(now);
        var sb = new StringBuilder();
        sb.AppendLine($"# Gợi ý Vietlott — {local.ToString("dd/MM/yyyy", Inv)}");
        sb.AppendLine();
        sb.AppendLine($"> **{Odds.Disclaimer}**");
        sb.AppendLine("> Mọi bộ số — kể cả bộ ngẫu nhiên — có cùng xác suất trúng. Bộ gợi ý chỉ phản ánh đặc điểm dữ liệu quá khứ.");
        sb.AppendLine();
        sb.AppendLine($"Tạo báo cáo lúc {local.ToString("HH:mm dd/MM/yyyy", Inv)} (giờ VN). Mỗi cách chọn là một phương án mua riêng.");
        if (!persisted) sb.AppendLine("Bản xem trước; chạy với `--out` để lưu dự đoán theo kỳ trước giờ quay.");
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
            sb.AppendLine($"- Kỳ mới nhất: **#{latest.DrawId:D5}** {latest.Date.ToString("dd/MM/yyyy", Inv)} — `{Balls(latest)}`");
            sb.AppendLine($"- Lịch sử dùng để gợi ý: {draws.Count} kỳ (#{draws[0].DrawId:D5} → #{latest.DrawId:D5})");
            WriteFrequency(sb, game, draws);
            sb.AppendLine();

            WriteReview(sb, game, reviews ?? []);

            var assessment = PredictionLedger.AssessTarget(game, draws, now);
            var forecast = forecasts?.GetValueOrDefault(game.Code);
            if ((forecast is null && forecasts is not null) || !assessment.IsReady)
            {
                sb.AppendLine($"**Chưa tạo dự đoán mới:** {assessment.Reason ?? "Kỳ mục tiêu đã bắt đầu trong lúc tạo báo cáo; cần cập nhật kết quả."}");
                sb.AppendLine();
                WriteOdds(sb, game);
                WriteRecent(sb, game, draws);
                continue;
            }
            var entries = forecast?.ToPredictionSet().Tickets ?? CreateSuggestions(game, draws, seed, sets, cover ?? checked(4 * sets));
            sb.AppendLine($"- Kỳ mục tiêu: **#{assessment.TargetDrawId:D5}**, bắt đầu {DrawSchedule.ToVietnamTime(assessment.TargetDrawAt!.Value):HH:mm dd/MM/yyyy} (giờ VN).");
            if (forecast is not null)
            {
                sb.AppendLine($"- Bộ số tạo lúc {DrawSchedule.ToVietnamTime(forecast.GeneratedAt):HH:mm:ss dd/MM/yyyy} · seed `{forecast.Seed}` · `{forecast.Configuration}`." +
                    (persisted ? " Chạy lại cùng kỳ giữ nguyên bộ đã lưu." : " Bản xem trước dùng các vé này."));
                if (PredictionLedger.HashInputs(draws) != forecast.InputHash)
                    sb.AppendLine("- Lịch sử hiện tại đã thay đổi so với đầu vào lúc lưu dự đoán; giữ nguyên vé đã chốt và không tính bản này vào thành tích nếu không còn đối chiếu được đầu vào.");
            }
            sb.AppendLine();
            var statistical = new StringBuilder();
            WriteSuggestions(statistical, game, entries.Where(t => t.Strategy != CoverPicker.Name).ToList());
            var coverTickets = entries.Where(t => t.Strategy == CoverPicker.Name).Select(t => t.Ticket).ToList();
            var coverCount = coverTickets.Count;
            var groups = runs > 0 && coverCount > 0 && draws.Count > train
                ? RunGroupBacktests(game, draws, coverCount, runs, train, seed, testFromDrawId)
                : [];
            WriteCover(sb, game, coverTickets);
            WriteGroupSummary(sb, groups);

            sb.AppendLine("### Bộ theo thống kê (tham khảo)");
            sb.AppendLine();
            sb.AppendLine("Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). " +
                          "Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.");
            sb.AppendLine();
            sb.Append(statistical);
            WriteTicketChances(sb, game);
            if (runs > 0) WriteBacktest(sb, game, draws, seed, runs, train, groups, testFromDrawId, data.Count);
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
        sb.AppendLine($"- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên {draws.Count} kỳ: p = {chi.PValue.ToString("F4", Inv)} → " +
                      (chi.PValue < 0.05
                          ? "có dấu hiệu lệch tần suất ở ngưỡng 5% chưa hiệu chỉnh nhiều phép thử."
                          : "chưa bác bỏ giả thuyết tần suất đều."));
        sb.AppendLine("  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.");
    }

    private static IReadOnlyList<SuggestedTicket> CreateSuggestions(Game game, IReadOnlyList<Draw> history, int seed, int sets, int cover)
    {
        var tickets = new List<SuggestedTicket>();
        var rng = new Random(SuggestCommand.GameSeed(seed, game));
        foreach (var name in new[] { "hot", "cold", "balanced", "random" })
            tickets.AddRange(TicketGroups.PickUnique(game, StrategyCatalog.Create(name), history, sets, rng, tickets.Select(t => t.Ticket))
                .Select(t => new SuggestedTicket(name, t)));
        var coverRng = new Random(unchecked(SuggestCommand.GameSeed(seed, game) * 7919 + 1));
        tickets.AddRange(CoverPicker.Pick(game, cover, coverRng).Select(t => new SuggestedTicket(CoverPicker.Name, t)));
        return tickets;
    }

    private static void WriteSuggestions(StringBuilder sb, Game game, IReadOnlyList<SuggestedTicket> suggestions)
    {
        sb.Append("| Chiến lược | Bộ số |");
        if (game.TicketHasSpecial) sb.Append(" ĐB |");
        sb.AppendLine($" Chẵn/Lẻ | Nhỏ/Lớn (≤{game.LowHighBoundary}) | Tổng |");
        sb.Append("|---|---|");
        if (game.TicketHasSpecial) sb.Append("---|");
        sb.AppendLine("---|---|---:|");

        foreach (var suggestion in suggestions)
        {
            var t = suggestion.Ticket;
            var even = t.Numbers.Count(n => n % 2 == 0);
            var low = t.Numbers.Count(n => n <= game.LowHighBoundary);
            var label = suggestion.Strategy == "random" ? "random (đối chứng)" : suggestion.Strategy;
            sb.Append($"| {label} | `{Ui.Balls(t.Numbers)}` |");
            if (game.TicketHasSpecial) sb.Append($" `{Ui.Ball(t.Special!.Value)}` |");
            sb.AppendLine($" {even}/{t.Numbers.Count - even} | {low}/{t.Numbers.Count - low} | {t.Numbers.Sum()} |");
        }
        sb.AppendLine();
    }

    /// <summary>
    /// Tính xác suất chính xác cho nhóm vé cụ thể, không khẳng định thuật toán tìm được nhóm tối ưu.
    /// </summary>
    private static void WriteCover(StringBuilder sb, Game game, IReadOnlyList<Ticket> tickets)
    {
        var count = tickets.Count;
        if (count == 0) return;
        var any = PortfolioOdds.AnyPrizeByPrefix(game, tickets);
        var three = game.TicketHasSpecial ? PortfolioOdds.AtLeastByPrefix(game, tickets, 3) : null;

        var p = game.Odds.AnyPrizeProbability;
        sb.AppendLine($"### Phương án phủ số ({count} vé)");
        sb.AppendLine();
        sb.AppendLine("Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này " +
                      "theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.");
        sb.AppendLine($"Chi phí {count} vé: **{Ui.Money(checked((long)(count * TicketEconomics.TicketPrice)))}**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.");
        sb.AppendLine();
        // ví dụ theo thói quen mua 5 vé mỗi kỳ
        var example = Math.Min(5, count);
        var exampleAny = any[example - 1];
        var exampleRange = example == 1 ? "cover 1" : $"cover 1–{example}";
        sb.AppendLine("**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua " +
                      $"**từ vé 1 đến vé ở dòng đó**. Ví dụ mua {example} vé → mua {exampleRange} → xem dòng cover {example}: " +
                      $"**{Percent(exampleAny)}**" +
                      (1 / exampleAny >= 2 ? $", tức khoảng 1 trong {Math.Round(1 / exampleAny).ToString("N0", Ui.Vi)} kỳ có vé trúng" : "") +
                      $" ({example} vé ngẫu nhiên khác nhau: {Percent(PortfolioOdds.UniformDistinctAnyPrize(game, example))}). " +
                      $"Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng {Percent(p)}.");
        sb.AppendLine();
        sb.Append("| Vé | Bộ số |");
        if (game.TicketHasSpecial) sb.Append(" ĐB |");
        sb.Append(" Mua từ vé 1 đến vé này: có ≥1 vé trúng |");
        if (three is not null) sb.Append(" …có ≥1 vé trùng ≥3 số |");
        sb.AppendLine(" Cùng số vé ngẫu nhiên khác nhau |");
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
            sb.AppendLine($" {Percent(PortfolioOdds.UniformDistinctAnyPrize(game, k))} |");
        }
        sb.AppendLine();

        sb.AppendLine("- " + GameCoverNote(game, example));
        var top = game.Odds.Table[0];
        sb.AppendLine($"- {top.Prize} ({top.Condition}): {count} vé khác nhau có khả năng đúng bằng {count} lần một vé — " +
                      $"khoảng 1 trên {(1 / (count * top.Probability)).ToString("N0", Ui.Vi)}, chọn số kiểu gì cũng vậy.");
        sb.AppendLine("- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.");
        sb.AppendLine();
    }

    /// <summary>
    /// Tóm tắt backtest nhóm vé bằng lời thường (chi tiết và độ tin cậy nằm trong mục Backtest):
    /// mua N vé mỗi kỳ trên các kỳ đã quay thì bao nhiêu kỳ có vé trúng, và tiền thu về so với tiền vé.
    /// </summary>
    private static void WriteGroupSummary(StringBuilder sb, IReadOnlyList<GroupBacktestResult> groups)
    {
        if (groups.FirstOrDefault(g => g.Strategy == CoverPicker.Name) is not { TestDraws: > 0 } cover
            || groups.FirstOrDefault(g => g.Strategy == "random") is not { } random) return;

        static string Money(double value) => Ui.Money((long)Math.Round(value));
        string Line(GroupBacktestResult g) =>
            $"{g.Strategy} → {Percent(g.AnyWinRate)}" +
            (g.AnyWinRate > 0 && 1 / g.AnyWinRate >= 2 ? $" (khoảng 1 trong {Math.Round(1 / g.AnyWinRate).ToString("N0", Ui.Vi)} kỳ)" : "");

        var n = cover.GroupSize;
        sb.AppendLine($"**Thử lại trên lịch sử:** nếu {cover.TestDraws.ToString("N0", Ui.Vi)} kỳ đã quay " +
                      $"(#{cover.DrawIds[0]:D5} → #{cover.DrawIds[^1]:D5}) mỗi kỳ bạn mua {n} vé, số kỳ có ít nhất 1 vé trúng là:");
        sb.AppendLine();
        sb.AppendLine($"- **{Line(cover)}**");
        sb.AppendLine($"- {Line(random)}");
        foreach (var g in groups.Where(g => g.Strategy is not CoverPicker.Name and not "random"))
            sb.AppendLine($"- {Line(g)}");
        sb.AppendLine();
        sb.AppendLine($"Tiền: mỗi kỳ chi {Money(cover.StakePerGroup)}, thu về trung bình {Money(cover.AverageBasePayout)} với phủ số " +
                      $"và {Money(random.AverageBasePayout)} với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). " +
                      "Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. " +
                      "Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.");
        sb.AppendLine();
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
                $"khả năng của số ĐB; {game.SpecialMax} vé có đủ {game.SpecialMax} số ĐB thì kỳ nào cũng có vé trúng, nhưng vẫn có thể lỗ tiền.",
            SpecialKind.BonusFromMainPool =>
                $"Với {game.Name}: số ĐB là quả thứ 7 quay cùng lồng, chỉ dùng cho giải 5 số + ĐB, nên không có giải nào chỉ cần " +
                $"trùng ĐB; {share} xác suất trúng giải của một vé thuộc {common.Prize} ({common.Condition}). So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.",
            _ =>
                $"Với {game.Name}: vé không có số ĐB và {share} số lần trúng là {common.Prize} ({common.Condition}). " +
                "So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.",
        };
    }

    /// <summary>Mua nhóm <paramref name="groupSize"/> vé mỗi kỳ trên lịch sử thật: phủ số trước, random cuối.</summary>
    private static IReadOnlyList<GroupBacktestResult> RunGroupBacktests(
        Game game, IReadOnlyList<Draw> draws, int groupSize, int runs, int train, int seed, int? testFromDrawId)
    {
        var groups = Math.Max(1, runs / groupSize);
        return [Backtester.RunCover(game, draws, groupSize, train, groups, seed, testFromDrawId),
            .. StrategyCatalog.All().Select(s => Backtester.RunGroups(game, draws, s, groupSize, train, groups, seed, testFromDrawId))];
    }

    private static void WriteBacktest(
        StringBuilder sb, Game game, IReadOnlyList<Draw> draws, int seed, int runs, int train,
        IReadOnlyList<GroupBacktestResult> groups, int? testFromDrawId, int gameCount)
    {
        var results = StrategyCatalog.All().Select(s => Backtester.Run(game, draws, s, train, runs, seed, testFromDrawId)).ToList();
        sb.AppendLine("<details><summary>Backtest walk-forward</summary>");
        sb.AppendLine();
        BacktestTables.WriteMarkdown(sb, results, groups, gameCount);
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
        var mine = reviews.Where(r => r.Set.Game == game && r.Set.GeneratedAt < r.Set.TargetDrawAt)
            .GroupBy(r => r.Draw.DrawId).Select(g => g.OrderBy(r => r.Set.GeneratedAt).First())
            .OrderBy(r => r.Draw.DrawId).ToList();
        if (mine.Count == 0)
        {
            sb.AppendLine("_Kiểm tra gợi ý cũ: chưa có dự đoán đã lưu nào có kết quả. Mục này hiện ra sau khi kỳ mục tiêu " +
                          "của một dự đoán đã lưu (thư mục `predictions/ledger/`) được quay._");
            sb.AppendLine();
            return;
        }

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
        sb.AppendLine("Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.");
        sb.AppendLine();
        sb.AppendLine("</details>");
        sb.AppendLine();
    }

    /// <summary>Phần trăm với đủ chữ số có nghĩa cho cả xác suất rất nhỏ (vd 0,0000257 %).</summary>
    private static string Percent(double p) => Ui.Percent(p);

    private static string Balls(Draw d) =>
        Ui.Balls(d.Numbers) + (d.Special is { } s ? $" | {Ui.Ball(s)}" : "");
}
