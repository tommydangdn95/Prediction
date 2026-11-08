namespace Power655.Core.Strategies;

public interface IPickStrategy
{
    string Name { get; }

    /// <summary>
    /// Chọn một vé cho <paramref name="game"/> chỉ dựa vào <paramref name="history"/> (các kỳ của cùng game):
    /// <see cref="Game.PickCount"/> số chính tăng dần, kèm số đặc biệt nếu <see cref="Game.TicketHasSpecial"/>.
    /// </summary>
    Ticket Pick(Game game, IReadOnlyList<Draw> history, Random rng);
}

internal static class PickHelper
{
    /// <summary>Lấy ngẫu nhiên đều <paramref name="count"/> phần tử khác nhau từ pool (Fisher–Yates một phần).</summary>
    public static int[] Sample(IReadOnlyList<int> pool, int count, Random rng)
    {
        if (pool.Count < count)
            throw new ArgumentException($"Pool có {pool.Count} số, cần ≥ {count}.", nameof(pool));
        var buffer = pool.ToArray();
        for (var i = 0; i < count; i++)
        {
            var j = rng.Next(i, buffer.Length);
            (buffer[i], buffer[j]) = (buffer[j], buffer[i]);
        }
        var result = buffer[..count];
        Array.Sort(result);
        return result;
    }

    /// <summary>Số đặc biệt ngẫu nhiên trong pool, hoặc null nếu vé của game không có ĐB.</summary>
    public static int? SampleSpecial(Game game, IReadOnlyList<int> pool, Random rng) =>
        game.TicketHasSpecial ? pool[rng.Next(pool.Count)] : null;

    /// <summary>Kích thước pool mặc định: 3 × số lượng số (18 với 6 số — như spec).</summary>
    public static int DefaultPoolSize(Game game) => game.PickCount * 3;

    /// <summary>Pool số đặc biệt cho hot/cold: 1/3 số ĐB (4 trong 12 với Lotto 5/35).</summary>
    public static int SpecialPoolSize(Game game) => Math.Max(1, game.SpecialMax / 3);

    public static IReadOnlyList<int> TopBy<T>(IReadOnlyDictionary<int, T> scores, int take)
        where T : IComparable<T> =>
        scores.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).Take(take).Select(kv => kv.Key).ToList();
}
