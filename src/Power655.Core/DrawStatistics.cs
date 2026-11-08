using MathNet.Numerics.Distributions;

namespace Power655.Core;

public sealed record NumberCount(int Number, int Count);

public sealed record PairCount(int First, int Second, int Count);

public sealed record HotColdResult(IReadOnlyList<NumberCount> Hot, IReadOnlyList<NumberCount> Cold);

public sealed record ChiSquareResult(double ChiSquare, int DegreesOfFreedom, double PValue, double Expected);

public sealed record SumRangeResult(int Min, int Max, double Mean, double P25, double Median, double P75);

/// <summary>
/// Các hàm thống kê thuần (không IO), tham số hóa theo <see cref="Game"/>. Thứ tự đầu vào không quan trọng:
/// mọi hàm phụ thuộc thời gian đều sắp kỳ mới nhất trước theo <see cref="Draw.DrawId"/>.
/// Các kỳ của game khác trong đầu vào gây <see cref="ArgumentException"/>.
/// </summary>
public static class DrawStatistics
{
    public static IReadOnlyList<Draw> NewestFirst(IEnumerable<Draw> draws) =>
        draws.OrderByDescending(d => d.DrawId).ToList();

    /// <summary>Số lần xuất hiện của mỗi số chính (đủ MaxNumber khóa, kể cả số chưa ra).</summary>
    public static IReadOnlyDictionary<int, int> Frequency(Game game, IEnumerable<Draw> draws)
    {
        var freq = game.AllNumbers.ToDictionary(n => n, _ => 0);
        foreach (var d in Of(game, draws))
            foreach (var n in d.Numbers)
                freq[n]++;
        return freq;
    }

    /// <summary>Số lần xuất hiện của mỗi số đặc biệt (đủ SpecialMax khóa; rỗng nếu game không có ĐB).</summary>
    public static IReadOnlyDictionary<int, int> SpecialFrequency(Game game, IEnumerable<Draw> draws)
    {
        var freq = game.AllSpecialNumbers.ToDictionary(n => n, _ => 0);
        foreach (var d in Of(game, draws))
            if (d.Special is { } s) freq[s]++;
        return freq;
    }

    /// <summary>
    /// Số kỳ kể từ lần cuối mỗi số chính xuất hiện (0 = vừa ra ở kỳ mới nhất).
    /// Số chưa từng ra trong dữ liệu có gap = số kỳ.
    /// </summary>
    public static IReadOnlyDictionary<int, int> Gap(Game game, IEnumerable<Draw> draws) =>
        GapOf(game.AllNumbers, Of(game, draws), d => d.Numbers);

    /// <summary>Gap của số đặc biệt (rỗng nếu game không có ĐB).</summary>
    public static IReadOnlyDictionary<int, int> SpecialGap(Game game, IEnumerable<Draw> draws) =>
        GapOf(game.AllSpecialNumbers, Of(game, draws), d => d.Special is { } s ? [s] : []);

    /// <summary>Top số nóng (ra nhiều nhất) và lạnh (ra ít nhất). Hòa thì số nhỏ hơn đứng trước.</summary>
    public static HotColdResult HotCold(Game game, IEnumerable<Draw> draws, int top = 10)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(top);
        var freq = Frequency(game, draws);
        var hot = freq.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key)
            .Take(top).Select(kv => new NumberCount(kv.Key, kv.Value)).ToList();
        var cold = freq.OrderBy(kv => kv.Value).ThenBy(kv => kv.Key)
            .Take(top).Select(kv => new NumberCount(kv.Key, kv.Value)).ToList();
        return new HotColdResult(hot, cold);
    }

    /// <summary>Các cặp số chính hay ra cùng nhau nhất.</summary>
    public static IReadOnlyList<PairCount> PairFrequency(Game game, IEnumerable<Draw> draws, int top = 10)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(top);
        var counts = new Dictionary<(int, int), int>();
        foreach (var d in Of(game, draws))
        {
            var nums = d.Numbers; // đã sắp tăng dần
            for (var i = 0; i < nums.Count; i++)
                for (var j = i + 1; j < nums.Count; j++)
                {
                    var key = (nums[i], nums[j]);
                    counts[key] = counts.GetValueOrDefault(key) + 1;
                }
        }
        return counts
            .OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key.Item1).ThenBy(kv => kv.Key.Item2)
            .Take(top)
            .Select(kv => new PairCount(kv.Key.Item1, kv.Key.Item2, kv.Value))
            .ToList();
    }

    /// <summary>
    /// Tần suất có trọng số thời gian (kỳ thứ k tính từ mới nhất có trọng số decay^k)
    /// + làm mượt Laplace (alpha). Trả về "tỉ lệ mô hình" mỗi số, tổng = 1.
    /// Với dữ liệu rỗng mọi số bằng 1/MaxNumber.
    /// </summary>
    public static IReadOnlyDictionary<int, double> WeightedScore(
        Game game, IEnumerable<Draw> draws, double decay = 0.95, double alpha = 1.0) =>
        WeightedScoreOf(game.AllNumbers, Of(game, draws), d => d.Numbers, decay, alpha);

    /// <summary>Như <see cref="WeightedScore"/> nhưng cho số đặc biệt (rỗng nếu game không có ĐB).</summary>
    public static IReadOnlyDictionary<int, double> SpecialWeightedScore(
        Game game, IEnumerable<Draw> draws, double decay = 0.95, double alpha = 1.0) =>
        WeightedScoreOf(game.AllSpecialNumbers, Of(game, draws), d => d.Special is { } s ? [s] : [], decay, alpha);

    /// <summary>Kiểm định chi² so với phân phối đều (df = MaxNumber − 1). p lớn ⇒ không bác bỏ "ngẫu nhiên đều".</summary>
    public static ChiSquareResult ChiSquareUniform(Game game, IEnumerable<Draw> draws)
    {
        var list = Of(game, draws);
        if (list.Count == 0) throw new ArgumentException("Cần ít nhất 1 kỳ.", nameof(draws));

        var expected = (double)list.Count * game.PickCount / game.MaxNumber;
        var chi = Frequency(game, list).Values.Sum(o => (o - expected) * (o - expected) / expected);
        var df = game.MaxNumber - 1;
        var p = 1 - ChiSquared.CDF(df, chi);
        return new ChiSquareResult(chi, df, p, expected);
    }

    /// <summary>Phân bố số chẵn trong bộ: khóa = số lượng số chẵn (0..PickCount), giá trị = số kỳ.</summary>
    public static IReadOnlyDictionary<int, int> OddEven(Game game, IEnumerable<Draw> draws) =>
        Distribution(game, draws, d => d.Numbers.Count(n => n % 2 == 0));

    /// <summary>Phân bố số nhỏ (1..LowHighBoundary) trong bộ: khóa = số lượng số nhỏ, giá trị = số kỳ.</summary>
    public static IReadOnlyDictionary<int, int> HighLow(Game game, IEnumerable<Draw> draws) =>
        Distribution(game, draws, d => d.Numbers.Count(n => n <= game.LowHighBoundary));

    public static SumRangeResult SumRange(Game game, IEnumerable<Draw> draws)
    {
        var sums = Of(game, draws).Select(d => (double)d.Numbers.Sum()).Order().ToList();
        if (sums.Count == 0) throw new ArgumentException("Cần ít nhất 1 kỳ.", nameof(draws));
        return new SumRangeResult(
            (int)sums[0], (int)sums[^1], sums.Average(),
            Percentile(sums, 25), Percentile(sums, 50), Percentile(sums, 75));
    }

    /// <summary>Percentile nội suy tuyến tính (giống numpy mặc định). <paramref name="sorted"/> phải sắp tăng.</summary>
    public static double Percentile(IReadOnlyList<double> sorted, double percent)
    {
        if (sorted.Count == 0) throw new ArgumentException("Danh sách rỗng.", nameof(sorted));
        if (percent is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(percent));
        var rank = percent / 100 * (sorted.Count - 1);
        var lo = (int)Math.Floor(rank);
        var hi = (int)Math.Ceiling(rank);
        return sorted[lo] + (sorted[hi] - sorted[lo]) * (rank - lo);
    }

    private static IReadOnlyList<Draw> Of(Game game, IEnumerable<Draw> draws)
    {
        var list = draws as IReadOnlyList<Draw> ?? draws.ToList();
        if (list.FirstOrDefault(d => d.Game != game) is { } other)
            throw new ArgumentException($"Dữ liệu lẫn game: cần {game.Name}, thấy {other.Game.Name} kỳ {other.DrawId}.", nameof(draws));
        return list;
    }

    private static IReadOnlyDictionary<int, int> GapOf(
        IReadOnlyList<int> keys, IReadOnlyList<Draw> draws, Func<Draw, IEnumerable<int>> values)
    {
        var ordered = NewestFirst(draws);
        var gap = keys.ToDictionary(n => n, _ => ordered.Count);
        for (var i = ordered.Count - 1; i >= 0; i--)
            foreach (var n in values(ordered[i]))
                gap[n] = i;
        return gap;
    }

    private static IReadOnlyDictionary<int, double> WeightedScoreOf(
        IReadOnlyList<int> keys, IReadOnlyList<Draw> draws, Func<Draw, IReadOnlyList<int>> values,
        double decay, double alpha)
    {
        if (decay is <= 0 or > 1) throw new ArgumentOutOfRangeException(nameof(decay), "decay ∈ (0,1].");
        if (alpha < 0) throw new ArgumentOutOfRangeException(nameof(alpha), "alpha ≥ 0.");
        if (keys.Count == 0) return new Dictionary<int, double>();

        var weighted = keys.ToDictionary(n => n, _ => 0.0);
        double weight = 1, totalHits = 0;
        foreach (var d in NewestFirst(draws))
        {
            var v = values(d);
            foreach (var n in v)
                weighted[n] += weight;
            totalHits += weight * v.Count;
            weight *= decay;
        }

        var denominator = totalHits + alpha * keys.Count;
        if (denominator == 0)
            return keys.ToDictionary(n => n, _ => 1.0 / keys.Count);
        return weighted.ToDictionary(kv => kv.Key, kv => (kv.Value + alpha) / denominator);
    }

    private static IReadOnlyDictionary<int, int> Distribution(Game game, IEnumerable<Draw> draws, Func<Draw, int> key)
    {
        var dist = Enumerable.Range(0, game.PickCount + 1).ToDictionary(k => k, _ => 0);
        foreach (var d in Of(game, draws))
            dist[key(d)]++;
        return dist;
    }
}
