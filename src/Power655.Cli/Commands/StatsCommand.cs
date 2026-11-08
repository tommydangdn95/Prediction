using System.CommandLine;
using System.Globalization;
using Power655.Core;
using Spectre.Console;

namespace Power655.Cli.Commands;

internal static class StatsCommand
{
    public static Command Create(GlobalOptions globals)
    {
        var last = new Option<int>("--last") { Description = "Chỉ tính N kỳ gần nhất (0 = tất cả)", DefaultValueFactory = _ => 40 };
        last.Min(0);
        var top = new Option<int>("--top") { Description = "Số mục trong các bảng top", DefaultValueFactory = _ => 10 };
        top.Min(1);

        var command = new Command("stats", "Bảng tần suất, số nóng/lạnh/gan, số đặc biệt, cặp số, chi² — từng game.") { last, top };
        command.SetAction(async (result, ct) =>
        {
            var exit = 0;
            foreach (var (game, draws) in await globals.LoadDrawsAsync(result, result.GetValue(last), ct))
            {
                Ui.GameHeader(game);
                if (draws.Count == 0) { Ui.NoData(game); exit = 1; continue; }
                Render(game, draws, result.GetValue(top));
                AnsiConsole.WriteLine();
            }
            return exit;
        });
        return command;
    }

    private static void Render(Game game, IReadOnlyList<Draw> draws, int top)
    {
        var inv = CultureInfo.InvariantCulture;
        AnsiConsole.Write(new Rule(
            $"[bold]Thống kê {Markup.Escape(game.Name)} {draws.Count} kỳ[/] (#{draws[0].DrawId:D5} {draws[0].Date:dd/MM/yyyy} → #{draws[^1].DrawId:D5} {draws[^1].Date:dd/MM/yyyy})"));

        var freq = DrawStatistics.Frequency(game, draws);
        var gap = DrawStatistics.Gap(game, draws);
        var score = DrawStatistics.WeightedScore(game, draws);
        var hotCold = DrawStatistics.HotCold(game, draws, top);
        var hot = hotCold.Hot.Select(x => x.Number).ToHashSet();
        var cold = hotCold.Cold.Select(x => x.Number).ToHashSet();

        // chia 3 cột
        var tables = game.AllNumbers.Chunk((game.MaxNumber + 2) / 3).Select(chunk =>
        {
            var t = new Table().Border(TableBorder.Simple)
                .AddColumn("Số").AddColumn(new TableColumn("Lần").RightAligned())
                .AddColumn(new TableColumn("Gan").RightAligned()).AddColumn(new TableColumn("Mô hình").RightAligned());
            foreach (var n in chunk)
            {
                var color = hot.Contains(n) ? "red" : cold.Contains(n) ? "blue" : "default";
                t.AddRow($"[{color}]{Ui.Ball(n)}[/]", $"[{color}]{freq[n]}[/]", gap[n].ToString(inv), score[n].ToString("P2", inv));
            }
            return t;
        });
        AnsiConsole.Write(new Columns(tables));
        AnsiConsole.MarkupLine($"[red]Đỏ[/] = top {top} nóng, [blue]xanh[/] = top {top} lạnh. Gan = số kỳ chưa ra (0 = vừa ra). " +
                               "Mô hình = tần suất có trọng số thời gian (decay 0.95, Laplace α=1).");
        AnsiConsole.WriteLine();

        var mostOverdue = gap.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).Take(top);
        var pairs = DrawStatistics.PairFrequency(game, draws, top);

        var lists = new List<Table>
        {
            List("Nóng", "red", hotCold.Hot.Select(x => (Ui.Ball(x.Number), $"{x.Count} lần"))),
            List("Lạnh", "blue", hotCold.Cold.Select(x => (Ui.Ball(x.Number), $"{x.Count} lần"))),
            List("Gan nhất", "blue", mostOverdue.Select(kv => (Ui.Ball(kv.Key), $"{kv.Value} kỳ"))),
        };
        if (game.HasSpecial)
        {
            var specialGap = DrawStatistics.SpecialGap(game, draws);
            var specials = DrawStatistics.SpecialFrequency(game, draws).Where(kv => kv.Value > 0)
                .OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).Take(top);
            lists.Add(List("Số đặc biệt", "yellow",
                specials.Select(kv => (Ui.Ball(kv.Key), $"{kv.Value} lần, gan {specialGap[kv.Key]}"))));
        }
        lists.Add(List("Cặp hay đi cùng", "green", pairs.Select(p => ($"{Ui.Ball(p.First)}-{Ui.Ball(p.Second)}", $"{p.Count} lần"))));
        AnsiConsole.Write(new Columns(lists));

        var oddEven = DrawStatistics.OddEven(game, draws);
        var highLow = DrawStatistics.HighLow(game, draws);
        var dist = new Table().Border(TableBorder.Simple).Title("Phân bố bộ số")
            .AddColumn("k").AddColumn(new TableColumn("k số chẵn").RightAligned())
            .AddColumn(new TableColumn($"k số nhỏ (1–{game.LowHighBoundary})").RightAligned());
        for (var k = 0; k <= game.PickCount; k++)
            dist.AddRow(k.ToString(inv), oddEven[k].ToString(inv), highLow[k].ToString(inv));

        var sum = DrawStatistics.SumRange(game, draws);
        var chi = DrawStatistics.ChiSquareUniform(game, draws);
        var summary = new Table().Border(TableBorder.Simple).Title($"Tổng {game.PickCount} số & kiểm định").HideHeaders()
            .AddColumn("").AddColumn("")
            .AddRow("Tổng min / max", $"{sum.Min} / {sum.Max}")
            .AddRow("Trung bình", sum.Mean.ToString("F1", inv))
            .AddRow("P25 / trung vị / P75", $"{sum.P25:F1} / {sum.Median:F1} / {sum.P75:F1}")
            .AddRow($"Chi² (df={chi.DegreesOfFreedom})", chi.ChiSquare.ToString("F2", inv))
            .AddRow("p-value", chi.PValue.ToString("F3", inv))
            .AddRow("Kỳ vọng mỗi số", chi.Expected.ToString("F2", inv));
        AnsiConsole.Write(new Columns(dist, summary));

        AnsiConsole.MarkupLine(chi.PValue >= 0.05
            ? $"p = {chi.PValue:F2} ≥ 0.05: [green]không có bằng chứng[/] tần suất lệch khỏi ngẫu nhiên đều — chênh lệch nóng/lạnh là dao động bình thường."
            : $"p = {chi.PValue:F3} < 0.05: tần suất lệch khỏi đều ở mức có ý nghĩa với mẫu này (cần thêm dữ liệu để kết luận).");
        Ui.WriteDisclaimer();
    }

    private static Table List(string title, string color, IEnumerable<(string Key, string Value)> rows)
    {
        var t = new Table().Border(TableBorder.Rounded).Title($"[{color}]{title}[/]").HideHeaders()
            .AddColumn("").AddColumn(new TableColumn("").RightAligned());
        foreach (var (key, value) in rows) t.AddRow($"[{color}]{key}[/]", value);
        return t;
    }
}
