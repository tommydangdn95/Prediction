using System.Numerics;

namespace Power655.Core;

/// <summary>
/// Xác suất cho cả một nhóm vé mua cùng kỳ, tính CHÍNH XÁC bằng cách duyệt mọi kết quả quay có thể
/// (mọi C(N,k) bộ số chính × mọi số ĐB).
/// <para>
/// Từng vé luôn có cùng xác suất trúng. Nhưng các vé trùng nhiều số với nhau thì hay trúng/trượt cùng lúc,
/// nên với cùng số vé, nhóm vé phủ rộng có thể có P(ít nhất một vé trúng) cao hơn. Số vé trúng kỳ vọng
/// (= số vé × p) không đổi theo cách chọn. Trúng giải không đồng nghĩa với có lãi.
/// </para>
/// </summary>
public static class PortfolioOdds
{
    /// <summary>Phần tử i = P(ít nhất một trong i+1 vé đầu trúng một giải bất kỳ của game).</summary>
    public static IReadOnlyList<double> AnyPrizeByPrefix(Game game, IReadOnlyList<Ticket> tickets) =>
        AnyWinByPrefix(game, tickets, (main, special) => game.Prizes.Any(p => p.IsWonBy(main, special)));

    /// <summary>Phần tử i = P(ít nhất một trong i+1 vé đầu trùng ≥ <paramref name="minMain"/> số chính).</summary>
    public static IReadOnlyList<double> AtLeastByPrefix(Game game, IReadOnlyList<Ticket> tickets, int minMain) =>
        AnyWinByPrefix(game, tickets, (main, _) => main >= minMain);

    /// <summary>
    /// P(ít nhất một vé trúng) khi mua <paramref name="tickets"/> vé chọn ngẫu nhiên độc lập: 1 − (1 − p)^n.
    /// Chính xác (không phải xấp xỉ) vì với mọi kết quả quay, một vé ngẫu nhiên đều trúng với xác suất p.
    /// </summary>
    public static double IndependentAnyPrize(Game game, int tickets) =>
        1 - Math.Pow(1 - game.Odds.AnyPrizeProbability, tickets);

    /// <summary>
    /// P(ít nhất một giải) của nhóm vé ngẫu nhiên đều KHÁC NHAU (lấy mẫu không hoàn lại).
    /// Cùng chính sách không mua trùng vé như TicketGroups; đây là trung bình trên các nhóm ngẫu nhiên.
    /// </summary>
    public static double UniformDistinctAnyPrize(Game game, int tickets)
    {
        ArgumentNullException.ThrowIfNull(game);
        ArgumentOutOfRangeException.ThrowIfNegative(tickets);
        var total = game.Odds.TicketCombinations;
        if (tickets > total) throw new ArgumentOutOfRangeException(nameof(tickets));
        if (tickets == 0) return 0;
        // Số vé thắng đối với một kết quả cố định là số nguyên; đối xứng giữa các kết quả quay.
        var winners = (long)Math.Round(total * game.Odds.AnyPrizeProbability);
        if (tickets > total - winners) return 1;
        double any = 0;
        for (var i = 0; i < tickets; i++)
        {
            // P(any sau i+1) = P(any sau i) + P(chưa thắng) × P(vé kế tiếp thắng).
            // Tránh triệt tiêu số khi lấy 1 − tích gần 1.
            any += (1 - any) * winners / (total - i);
        }
        return any;
    }

    /// <param name="wins">Vé có trúng không, theo (số chính trùng, có trùng ĐB không).</param>
    /// <returns>Phần tử i = P(ít nhất một trong i+1 vé đầu thỏa <paramref name="wins"/>).</returns>
    public static IReadOnlyList<double> AnyWinByPrefix(Game game, IReadOnlyList<Ticket> tickets, Func<int, bool, bool> wins)
    {
        ArgumentNullException.ThrowIfNull(game);
        ArgumentNullException.ThrowIfNull(tickets);
        ArgumentNullException.ThrowIfNull(wins);
        if (tickets.Count == 0) return [];
        foreach (var t in tickets) Validate(game, t);

        var counter = new Counter(game, tickets, wins);
        var histogram = new long[tickets.Count + 1];
        var gate = new object();
        // chia theo số nhỏ nhất của bộ quay để chạy song song
        Parallel.For(0, game.MaxNumber - game.PickCount + 1,
            () => (Hist: new long[tickets.Count + 1], Matches: new int[tickets.Count]),
            (first, _, local) =>
            {
                counter.EnumerateFrom(first, local.Hist, local.Matches);
                return local;
            },
            local =>
            {
                lock (gate)
                    for (var i = 0; i < histogram.Length; i++) histogram[i] += local.Hist[i];
            });

        // histogram[f] = số (kết quả quay × ĐB) mà vé trúng đầu tiên là vé f (f = n: không vé nào trúng)
        double total = game.Odds.TotalCombinations * (double)counter.Scenarios;
        var result = new double[tickets.Count];
        long noWin = histogram[tickets.Count];
        for (var length = tickets.Count; length >= 1; length--)
        {
            result[length - 1] = 1 - noWin / total;
            noWin += histogram[length - 1];
        }
        return result;
    }

    private static void Validate(Game game, Ticket ticket)
    {
        if (ticket.Numbers.Count != game.PickCount || ticket.Numbers.Distinct().Count() != game.PickCount
            || ticket.Numbers.Any(n => !game.IsValidNumber(n)))
            throw new ArgumentException($"Vé {ticket} không hợp lệ cho {game.Name}.", nameof(ticket));
        if (game.TicketHasSpecial != ticket.Special.HasValue
            || (ticket.Special is { } s && !game.IsValidSpecial(s)))
            throw new ArgumentException($"Vé {ticket}: số ĐB không hợp lệ cho {game.Name}.", nameof(ticket));
    }

    /// <summary>Duyệt các bộ số chính bằng bitmask (số n ↔ bit n−1) và đếm vé trúng đầu tiên.</summary>
    private sealed class Counter
    {
        private readonly Game _game;
        private readonly ulong[] _masks;
        private readonly int[] _specials;
        private readonly bool[] _winPlain;
        private readonly bool[] _winSpecial;

        public Counter(Game game, IReadOnlyList<Ticket> tickets, Func<int, bool, bool> wins)
        {
            _game = game;
            _masks = tickets.Select(t => t.Numbers.Aggregate(0UL, (m, n) => m | 1UL << (n - 1))).ToArray();
            _specials = tickets.Select(t => t.Special ?? 0).ToArray();
            _winPlain = Enumerable.Range(0, game.PickCount + 1).Select(m => wins(m, false)).ToArray();
            _winSpecial = Enumerable.Range(0, game.PickCount + 1)
                .Select(m => game.HasSpecial ? wins(m, true) : wins(m, false)).ToArray();
            Scenarios = game.Special switch
            {
                SpecialKind.SeparatePool => game.SpecialMax,
                SpecialKind.BonusFromMainPool => game.MaxNumber - game.PickCount,
                _ => 1,
            };
        }

        /// <summary>Số khả năng của số ĐB ứng với mỗi bộ số chính (đồng khả năng).</summary>
        public long Scenarios { get; }

        public void EnumerateFrom(int first, long[] hist, int[] matches)
        {
            int k = _game.PickCount, n = _game.MaxNumber;
            var idx = new int[k];
            var prefix = new ulong[k + 1];
            idx[0] = first;
            prefix[1] = 1UL << first;
            for (var j = 1; j < k; j++)
            {
                idx[j] = first + j;
                prefix[j + 1] = prefix[j] | 1UL << idx[j];
            }
            while (true)
            {
                Visit(prefix[k], hist, matches);
                var p = k - 1;
                while (p >= 1 && idx[p] == n - k + p) p--;
                if (p < 1) return;
                idx[p]++;
                prefix[p + 1] = prefix[p] | 1UL << idx[p];
                for (var j = p + 1; j < k; j++)
                {
                    idx[j] = idx[j - 1] + 1;
                    prefix[j + 1] = prefix[j] | 1UL << idx[j];
                }
            }
        }

        private void Visit(ulong draw, long[] hist, int[] matches)
        {
            var count = _masks.Length;
            // vé đầu tiên chắc chắn trúng (không phụ thuộc ĐB), và có vé nào đứng trước nó phụ thuộc ĐB không
            var always = count;
            var dependent = false;
            for (var i = 0; i < count; i++)
            {
                var m = BitOperations.PopCount(_masks[i] & draw);
                matches[i] = m;
                if (_winPlain[m] && _winSpecial[m]) { always = i; break; }
                if (_winPlain[m] != _winSpecial[m]) dependent = true;
            }
            if (!dependent)
            {
                hist[always] += Scenarios;
                return;
            }

            if (_game.Special == SpecialKind.SeparatePool)
            {
                // ĐB quay từ lồng riêng 1..SpecialMax; vé trùng ĐB nếu số ĐB của vé bằng số đó
                for (var s = 1; s <= _game.SpecialMax; s++)
                {
                    var f = always;
                    for (var i = 0; i < always; i++)
                    {
                        var m = matches[i];
                        if (_specials[i] == s ? _winSpecial[m] : _winPlain[m]) { f = i; break; }
                    }
                    hist[f]++;
                }
            }
            else
            {
                // ĐB là một trong các số chưa quay; vé trùng ĐB nếu chứa số đó
                for (var b = 0; b < _game.MaxNumber; b++)
                {
                    var bit = 1UL << b;
                    if ((draw & bit) != 0) continue;
                    var f = always;
                    for (var i = 0; i < always; i++)
                    {
                        var m = matches[i];
                        if ((_masks[i] & bit) != 0 ? _winSpecial[m] : _winPlain[m]) { f = i; break; }
                    }
                    hist[f]++;
                }
            }
        }
    }
}
