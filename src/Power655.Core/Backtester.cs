using Power655.Core.Strategies;

namespace Power655.Core;

/// <param name="SpecialHitRate">Tỉ lệ vé trùng số đặc biệt (chỉ Lotto 5/35, null với game khác).</param>
/// <param name="AnyPrizeRate">Tỉ lệ vé trúng ít nhất một giải theo cơ cấu giải của game.</param>
public sealed record BacktestResult(
    Game Game,
    string Strategy,
    int TestDraws,
    int Runs,
    double AverageMatches,
    double RateAtLeast3,
    double? SpecialHitRate,
    double AnyPrizeRate,
    IReadOnlyDictionary<int, double> MatchDistribution)
{
    /// <summary>
    /// Sai số chuẩn của số trùng trung bình do chỉ có <see cref="TestDraws"/> kỳ kiểm tra.
    /// Chạy nhiều lần trên cùng các kỳ không làm giảm sai số này.
    /// </summary>
    public double StandardError => TestDraws == 0 ? double.NaN : Math.Sqrt(Game.Odds.MatchVariance / TestDraws);
}

/// <summary>Backtest walk-forward: với mỗi kỳ t (từ kỳ thứ train+1) chỉ dùng dữ liệu trước t để chọn số.</summary>
public static class Backtester
{
    public static BacktestResult Run(
        Game game, IReadOnlyList<Draw> draws, IPickStrategy strategy, int train = 20, int runs = 1, int seed = 42)
    {
        ArgumentNullException.ThrowIfNull(game);
        ArgumentNullException.ThrowIfNull(strategy);
        ArgumentOutOfRangeException.ThrowIfNegative(train);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(runs);
        if (draws.FirstOrDefault(d => d.Game != game) is { } other)
            throw new ArgumentException($"Dữ liệu lẫn game: cần {game.Name}, thấy {other.Game.Name}.", nameof(draws));

        var ordered = draws.OrderBy(d => d.DrawId).ToList();
        var rng = new Random(seed);
        var histogram = new long[game.PickCount + 1];
        long totalMatches = 0, atLeast3 = 0, specialHits = 0, prizes = 0, picks = 0;

        for (var t = train; t < ordered.Count; t++)
        {
            var history = ordered.GetRange(0, t);
            var actual = ordered[t];
            for (var r = 0; r < runs; r++)
            {
                var ticket = strategy.Pick(game, history, rng);
                var (matches, special) = game.Odds.Match(ticket, actual);
                histogram[matches]++;
                totalMatches += matches;
                if (matches >= 3) atLeast3++;
                if (special) specialHits++;
                if (game.Odds.Classify(ticket, actual) is not null) prizes++;
                picks++;
            }
        }

        double Rate(long count) => picks == 0 ? double.NaN : (double)count / picks;
        var testDraws = Math.Max(0, ordered.Count - train);
        var distribution = Enumerable.Range(0, histogram.Length)
            .ToDictionary(k => k, k => picks == 0 ? 0 : (double)histogram[k] / picks);
        return new BacktestResult(
            game,
            strategy.Name,
            testDraws,
            runs,
            Rate(totalMatches),
            Rate(atLeast3),
            game.TicketHasSpecial ? Rate(specialHits) : null,
            Rate(prizes),
            distribution);
    }
}
