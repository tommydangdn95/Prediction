namespace Power655.Core.Strategies;

/// <summary>Vé ngẫu nhiên đều — luôn hiển thị để đối chứng.</summary>
public sealed class RandomPick : IPickStrategy
{
    public string Name => "random";

    public Ticket Pick(Game game, IReadOnlyList<Draw> history, Random rng) =>
        new(PickHelper.Sample(game.AllNumbers, game.PickCount, rng),
            PickHelper.SampleSpecial(game, game.AllSpecialNumbers, rng));
}

/// <summary>
/// Random trong top-<c>poolSize</c> theo <see cref="DrawStatistics.WeightedScore"/>
/// (mặc định 3 × số lượng số: 18 với 6/55 và 6/45, 15 với 5/35).
/// Lotto 5/35: số ĐB random trong top-4 <see cref="DrawStatistics.SpecialWeightedScore"/>.
/// </summary>
public sealed class HotPick(int? poolSize = null, double decay = 0.95, double alpha = 1.0) : IPickStrategy
{
    public string Name => "hot";

    public Ticket Pick(Game game, IReadOnlyList<Draw> history, Random rng) =>
        new(PickHelper.Sample(Pool(game, history), game.PickCount, rng),
            game.TicketHasSpecial ? PickHelper.SampleSpecial(game, SpecialPool(game, history), rng) : null);

    public IReadOnlyList<int> Pool(Game game, IReadOnlyList<Draw> history) =>
        PickHelper.TopBy(DrawStatistics.WeightedScore(game, history, decay, alpha),
            poolSize ?? PickHelper.DefaultPoolSize(game));

    public IReadOnlyList<int> SpecialPool(Game game, IReadOnlyList<Draw> history) =>
        PickHelper.TopBy(DrawStatistics.SpecialWeightedScore(game, history, decay, alpha), PickHelper.SpecialPoolSize(game));
}

/// <summary>Random trong top-<c>poolSize</c> số gan (lâu chưa ra nhất). Lotto 5/35: ĐB trong top-4 ĐB gan.</summary>
public sealed class ColdPick(int? poolSize = null) : IPickStrategy
{
    public string Name => "cold";

    public Ticket Pick(Game game, IReadOnlyList<Draw> history, Random rng) =>
        new(PickHelper.Sample(Pool(game, history), game.PickCount, rng),
            game.TicketHasSpecial ? PickHelper.SampleSpecial(game, SpecialPool(game, history), rng) : null);

    public IReadOnlyList<int> Pool(Game game, IReadOnlyList<Draw> history) =>
        MostOverdue(DrawStatistics.Gap(game, history), DrawStatistics.Frequency(game, history),
            poolSize ?? PickHelper.DefaultPoolSize(game));

    public IReadOnlyList<int> SpecialPool(Game game, IReadOnlyList<Draw> history) =>
        MostOverdue(DrawStatistics.SpecialGap(game, history), DrawStatistics.SpecialFrequency(game, history),
            PickHelper.SpecialPoolSize(game));

    private static IReadOnlyList<int> MostOverdue(
        IReadOnlyDictionary<int, int> gap, IReadOnlyDictionary<int, int> freq, int take) =>
        gap.OrderByDescending(kv => kv.Value).ThenBy(kv => freq[kv.Key]).ThenBy(kv => kv.Key)
            .Take(take).Select(kv => kv.Key).ToList();
}

/// <summary>
/// Bộ cân bằng: số chẵn và số nhỏ (1..LowHighBoundary) mỗi loại bằng ⌊k/2⌋ hoặc ⌈k/2⌉
/// (6 số: đúng 3/3; 5 số: 2 hoặc 3), tổng nằm trong [P25, P75] tổng lịch sử.
/// Lấy mẫu loại bỏ (rejection sampling) từ phân phối đều nên mọi bộ thỏa điều kiện đồng khả năng.
/// Số ĐB (Lotto 5/35) ngẫu nhiên đều.
/// </summary>
public sealed class BalancedPick : IPickStrategy
{
    private const int MaxAttempts = 200_000;

    public string Name => "balanced";

    public Ticket Pick(Game game, IReadOnlyList<Draw> history, Random rng)
    {
        var (minSum, maxSum) = SumBounds(game, history);
        int[] candidate = [];
        for (var attempt = 0; attempt < MaxAttempts; attempt++)
        {
            candidate = PickHelper.Sample(game.AllNumbers, game.PickCount, rng);
            if (IsBalanced(game, candidate, minSum, maxSum))
                break;
        }
        // không thể hết lượt với dữ liệu thực; nếu có thì trả bộ cuối thay vì treo
        return new Ticket(candidate, PickHelper.SampleSpecial(game, game.AllSpecialNumbers, rng));
    }

    public static (double Min, double Max) SumBounds(Game game, IReadOnlyList<Draw> history)
    {
        if (history.Count == 0) return (double.MinValue, double.MaxValue);
        var range = DrawStatistics.SumRange(game, history);
        return (range.P25, range.P75);
    }

    public static bool IsBalanced(Game game, IReadOnlyList<int> numbers, double minSum, double maxSum)
    {
        var even = numbers.Count(n => n % 2 == 0);
        var low = numbers.Count(n => n <= game.LowHighBoundary);
        var sum = numbers.Sum();
        return IsHalf(even, game.PickCount) && IsHalf(low, game.PickCount) && sum >= minSum && sum <= maxSum;
    }

    private static bool IsHalf(int count, int total) => count == total / 2 || count == (total + 1) / 2;
}

public static class StrategyCatalog
{
    public static IReadOnlyList<string> Names { get; } = ["random", "hot", "cold", "balanced"];

    public static IPickStrategy Create(string name) => name.ToLowerInvariant() switch
    {
        "random" => new RandomPick(),
        "hot" => new HotPick(),
        "cold" => new ColdPick(),
        "balanced" => new BalancedPick(),
        _ => throw new ArgumentException($"Strategy không hợp lệ: '{name}'. Chọn: {string.Join(", ", Names)}.", nameof(name)),
    };

    public static IReadOnlyList<IPickStrategy> All() => Names.Select(Create).ToList();
}
