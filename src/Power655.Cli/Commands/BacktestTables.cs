using System.Globalization;
using System.Text;
using Power655.Core;
using Spectre.Console;

namespace Power655.Cli.Commands;

/// <summary>Cùng một nội dung đánh giá cho CLI và báo cáo Markdown.</summary>
internal static class BacktestTables
{
    private sealed record Section(string Title, string[] Columns, List<string[]> Rows);
    private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

    public static void WriteMarkdown(StringBuilder sb, IReadOnlyList<BacktestResult> singles,
        IReadOnlyList<GroupBacktestResult> groups, int gameCount)
    {
        foreach (var section in Sections(singles, groups))
        {
            sb.AppendLine($"**{section.Title}**");
            sb.AppendLine();
            sb.AppendLine("| " + string.Join(" | ", section.Columns) + " |");
            sb.AppendLine("|" + string.Join("|", section.Columns.Select(_ => "---")) + "|");
            foreach (var row in section.Rows) sb.AppendLine("| " + string.Join(" | ", row) + " |");
            sb.AppendLine();
        }
        foreach (var note in Notes(singles, groups, gameCount)) { sb.AppendLine(note); sb.AppendLine(); }
    }

    public static void WriteConsole(IReadOnlyList<BacktestResult> singles,
        IReadOnlyList<GroupBacktestResult> groups, int gameCount)
    {
        foreach (var section in Sections(singles, groups))
        {
            var table = new Table().Border(TableBorder.Rounded).Title(Markup.Escape(section.Title));
            foreach (var column in section.Columns) table.AddColumn(Markup.Escape(column));
            foreach (var row in section.Rows) table.AddRow(row.Select(Markup.Escape).ToArray());
            AnsiConsole.Write(table);
        }
        foreach (var note in Notes(singles, groups, gameCount)) AnsiConsole.WriteLine(note);
    }

    private static IEnumerable<Section> Sections(IReadOnlyList<BacktestResult> singles, IReadOnlyList<GroupBacktestResult> groups)
    {
        if (singles.Count == 0 || singles[0].TestDraws == 0) yield break;
        var baseline = singles.Single(r => r.Strategy == "random");
        var rows = new List<string[]>();
        foreach (var r in singles)
        {
            var c = r.Strategy == "random" ? null : Backtester.ComparePrizeRate(r, baseline);
            rows.Add([r.Strategy, r.AverageMatches.ToString("F3", Inv), Ui.Percent(r.AnyPrizeRate),
                c is null ? "-" : Points(c.MeanDifference), c is null ? "-" : Interval(c), c is null ? "-" : P(c.PValue)]);
        }
        yield return new Section($"Từng vé: {baseline.TestDraws} kỳ thật, {baseline.Runs} lần lấy mẫu/kỳ",
            ["Chiến lược", "Số chính trùng TB", "Trúng giải", "Δ với random (điểm %)", "CI 95% của Δ (điểm %)", "p trúng giải"], rows);

        if (groups.Count == 0 || groups[0].TestDraws == 0) yield break;
        var reference = groups.Single(r => r.Strategy == "random");
        rows = [];
        foreach (var r in groups)
        {
            var c = r.Strategy == "random" ? null : Backtester.CompareGroups(r, reference);
            rows.Add([r.Strategy, Ui.Percent(r.AnyWinRate),
                $"{Ui.Percent(r.AnyWinConfidenceLow)} đến {Ui.Percent(r.AnyWinConfidenceHigh)}",
                c is null ? "-" : Points(c.MeanDifference), c is null ? "-" : Interval(c), c is null ? "-" : P(c.PValue)]);
        }
        yield return new Section($"Cùng {reference.GroupSize} vé khác nhau/kỳ, {reference.Groups} nhóm mô phỏng/kỳ",
            ["Cách chọn", "Có ≥1 giải", "Khoảng 95% theo kỳ", "Δ với random (điểm %)", "CI 95% của Δ (điểm %)", "p có ≥1 giải"], rows);
        rows = groups.Select(r => new[] { r.Strategy, Money(r.AverageBasePayout), Money(r.AverageBaseNet),
            r.BaseRoi.ToString("P2", Ui.Vi), Ui.Percent(r.BaseBreakEvenRate), Ui.Percent(r.BaseProfitRate) }).ToList();
        yield return new Section($"Mô hình tiền giải cơ bản: chi {Money(reference.StakePerGroup)}/kỳ",
            ["Cách chọn", "Thưởng TB/kỳ", "Lãi/lỗ TB/kỳ", "ROI cơ bản", "Thu ≥ chi", "Thu > chi"], rows);
    }

    private static IEnumerable<string> Notes(IReadOnlyList<BacktestResult> singles, IReadOnlyList<GroupBacktestResult> groups, int gameCount)
    {
        if (singles.Count == 0 || singles[0].TestDraws == 0)
        {
            yield return "Không có kỳ kiểm tra trong khoảng đã chọn.";
            yield break;
        }
        var random = singles.Single(r => r.Strategy == "random");
        var comparisons = singles.Where(r => r.Strategy != "random")
            .Select(r => Backtester.ComparePrizeRate(r, random)).ToList();
        if (groups.Count > 0)
        {
            var reference = groups.Single(r => r.Strategy == "random");
            comparisons.AddRange(groups.Where(r => r.Strategy != "random").Select(r => Backtester.CompareGroups(r, reference)));
        }
        // At most 3 single-ticket and 4 portfolio comparisons per game. Retained
        // ledgers may use different group counts, so do not shrink the family for one game.
        var family = Math.Max(7, comparisons.Count) * Math.Max(1, gameCount);
        var alpha = 0.05 / family;
        yield return $"Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < {alpha.ToString("F5", Inv)} cho tối đa {family} so sánh trên các game được chọn. " +
            "CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.";
        yield return comparisons.Any(c => c.MeanDifference > 0 && c.PValue < alpha)
            ? "Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới."
            : "Chưa phát hiện cách chọn tăng tỷ lệ trúng giải so với random ở ngưỡng đã hiệu chỉnh.";
        yield return "Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.";
        if (groups.Count > 0)
        {
            var r = groups[0];
            yield return TicketEconomics.ModelDescription + ". Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. " +
                "Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.";
            yield return $"Tổng chi của một chuỗi mua {r.GroupSize} vé qua {r.TestDraws} kỳ: {Money(r.StakePerGroup * r.TestDraws)}. " +
                "Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.";
        }
    }

    private static string Points(double value) => double.IsFinite(value) ? (100 * value).ToString("+0.000;-0.000;0.000", Inv) : "-";
    private static string Interval(PairedBacktestComparison c) =>
        double.IsFinite(c.ConfidenceLow) && double.IsFinite(c.ConfidenceHigh) ? $"{Points(c.ConfidenceLow)} đến {Points(c.ConfidenceHigh)}" : "-";
    private static string P(double p) => double.IsFinite(p) ? p.ToString("G4", Inv) : "-";
    private static string Money(double value) => double.IsFinite(value) ? value.ToString("N0", Ui.Vi) + "đ" : "-";
}
