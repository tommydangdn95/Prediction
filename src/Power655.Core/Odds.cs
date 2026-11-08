namespace Power655.Core;

/// <param name="Probability">Xác suất chính xác một vé trúng hạng này.</param>
/// <param name="OneIn">1 / Probability, làm tròn — giá trị hiển thị.</param>
public sealed record PrizeOdds(string Prize, string Condition, double Probability, long OneIn);

public static class Odds
{
    public const string Disclaimer =
        "Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng.";

    public static long Combinations(int n, int k)
    {
        if (k < 0 || k > n) return 0;
        k = Math.Min(k, n - k);
        long result = 1;
        for (var i = 1; i <= k; i++)
            result = result * (n - k + i) / i;
        return result;
    }
}

/// <summary>
/// Xác suất thật của một game (spec mục 8), tính bằng tổ hợp từ <see cref="Game.Prizes"/>.
/// Bắt buộc hiển thị cùng mọi kết quả gợi ý.
/// </summary>
public sealed class GameOdds
{
    private readonly Game _game;

    internal GameOdds(Game game)
    {
        _game = game;
        TotalCombinations = Odds.Combinations(game.MaxNumber, game.PickCount);
        Table = game.Prizes.Select(p =>
        {
            var prob = PrizeProbability(p);
            // làm tròn 6 chữ số trước để khử sai số dấu phẩy động, rồi làm tròn về chẵn
            // (C(55,6)/6 = 4.831.612,5 → 4.831.612 như spec)
            var oneIn = Math.Round(Math.Round(1 / prob, 6), MidpointRounding.ToEven);
            return new PrizeOdds(p.Name, p.Condition, prob, (long)oneIn);
        }).ToList();
    }

    /// <summary>Số bộ số chính khác nhau, vd C(55,6) = 28.989.675.</summary>
    public long TotalCombinations { get; }

    /// <summary>Số vé khác nhau (× số đặc biệt nếu người chơi chọn ĐB): C(35,5)·12 với Lotto 5/35.</summary>
    public long TicketCombinations => _game.TicketHasSpecial ? TotalCombinations * _game.SpecialMax : TotalCombinations;

    public IReadOnlyList<PrizeOdds> Table { get; }

    /// <summary>Số trùng kỳ vọng của một vé bất kỳ: k²/N (6·6/55 ≈ 0.655).</summary>
    public double ExpectedMatches => (double)_game.PickCount * _game.PickCount / _game.MaxNumber;

    /// <summary>Phương sai số trùng (siêu bội) — dùng tính sai số chuẩn trong backtest.</summary>
    public double MatchVariance
    {
        get
        {
            double n = _game.PickCount, total = _game.MaxNumber, k = _game.PickCount;
            return n * (k / total) * ((total - k) / total) * ((total - n) / (total - 1));
        }
    }

    /// <summary>P(vé trùng đúng k số chính) — phân phối siêu bội.</summary>
    public double ProbabilityExactMatches(int k) =>
        (double)Odds.Combinations(_game.PickCount, k)
        * Odds.Combinations(_game.MaxNumber - _game.PickCount, _game.PickCount - k)
        / TotalCombinations;

    /// <summary>P(vé trùng ≥ k số chính).</summary>
    public double ProbabilityAtLeast(int k) =>
        Enumerable.Range(k, Math.Max(0, _game.PickCount - k + 1)).Sum(ProbabilityExactMatches);

    /// <summary>
    /// P(trùng số đặc biệt | đã trùng đúng k số chính).
    /// Power 6/55: ĐB đều trên N−6 số còn lại, vé có 6−k số ở đó. Lotto 5/35: 1/12, độc lập.
    /// </summary>
    public double ProbabilitySpecialMatch(int k) => _game.Special switch
    {
        SpecialKind.BonusFromMainPool => (double)(_game.PickCount - k) / (_game.MaxNumber - _game.PickCount),
        SpecialKind.SeparatePool => 1.0 / _game.SpecialMax,
        _ => 0,
    };

    public double PrizeProbability(PrizeTier tier)
    {
        double total = 0;
        for (var k = 0; k <= _game.PickCount; k++)
        {
            var pk = ProbabilityExactMatches(k);
            var ps = ProbabilitySpecialMatch(k);
            if (tier.IsWonBy(k, true)) total += pk * ps;
            if (tier.IsWonBy(k, false)) total += pk * (1 - ps);
        }
        return total;
    }

    /// <summary>P(vé trúng ít nhất một giải).</summary>
    public double AnyPrizeProbability => Table.Sum(t => t.Probability);

    /// <summary>Hạng giải cao nhất vé trúng ở kỳ <paramref name="draw"/>, hoặc null.</summary>
    public PrizeTier? Classify(Ticket ticket, Draw draw)
    {
        var (main, special) = Match(ticket, draw);
        return _game.Prizes.FirstOrDefault(p => p.IsWonBy(main, special));
    }

    /// <summary>Số chính trùng và có trùng ĐB không (theo luật của game).</summary>
    public (int Main, bool Special) Match(Ticket ticket, Draw draw)
    {
        var drawn = draw.Numbers.ToHashSet();
        var main = ticket.Numbers.Count(drawn.Contains);
        var special = _game.Special switch
        {
            SpecialKind.BonusFromMainPool => draw.Special is { } b && ticket.Numbers.Contains(b),
            SpecialKind.SeparatePool => draw.Special is { } s && ticket.Special == s,
            _ => false,
        };
        return (main, special);
    }
}
