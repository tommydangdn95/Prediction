namespace Power655.Core.Strategies;

public interface IPickStrategy
{
    string Name { get; }

    /// <summary>
    /// Chọn một vé cho <paramref name="game"/> chỉ dựa vào <paramref name="history"/> (các kỳ của cùng game):
    /// <see cref="Game.PickCount"/> số chính tăng dần, kèm số đặc biệt nếu <see cref="Game.TicketHasSpecial"/>.
    /// </summary>
    Ticket Pick(Game game, IReadOnlyList<Draw> history, Random rng);

    /// <summary>
    /// Tính thống kê của <paramref name="history"/> một lần rồi trả hàm chọn vé — dùng khi cần nhiều vé cho cùng
    /// lịch sử (backtest). Kết quả phải giống hệt gọi <see cref="Pick"/> với cùng chuỗi <see cref="Random"/>.
    /// </summary>
    Func<Random, Ticket> Prepare(Game game, IReadOnlyList<Draw> history) => rng => Pick(game, history, rng);
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

    /// <summary>Hàm chọn vé ngẫu nhiên trong pool số chính (và pool ĐB nếu vé có ĐB) đã tính sẵn.</summary>
    public static Func<Random, Ticket> FromPools(Game game, IReadOnlyList<int> pool, IReadOnlyList<int>? specialPool) =>
        rng => new Ticket(Sample(pool, game.PickCount, rng), specialPool is null ? null : SampleSpecial(game, specialPool, rng));

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
