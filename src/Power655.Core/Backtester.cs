using Power655.Core.Strategies;
using MathNet.Numerics.Distributions;

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
    IReadOnlyDictionary<int, double> MatchDistribution,
    IReadOnlyList<double> AverageMatchesByDraw)
{
    public double StandardError => Backtester.StandardError(AverageMatchesByDraw);
}

public sealed record PairedBacktestComparison(
    string Strategy, double MeanDifference, double StandardError, double TStatistic, double PValue, int Draws);

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
        var averageMatchesByDraw = new List<double>(Math.Max(0, ordered.Count - train));
        long totalMatches = 0, atLeast3 = 0, specialHits = 0, prizes = 0, picks = 0;

        for (var t = train; t < ordered.Count; t++)
        {
            var history = ordered.GetRange(0, t);
            var actual = ordered[t];
            long drawMatches = 0;
            for (var r = 0; r < runs; r++)
            {
                var ticket = strategy.Pick(game, history, rng);
                var (matches, special) = game.Odds.Match(ticket, actual);
                histogram[matches]++;
                totalMatches += matches;
                drawMatches += matches;
                if (matches >= 3) atLeast3++;
                if (special) specialHits++;
                if (game.Odds.Classify(ticket, actual) is not null) prizes++;
                picks++;
            }
            averageMatchesByDraw.Add((double)drawMatches / runs);
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
            distribution,
            averageMatchesByDraw);
    }

    /// <summary>
    /// So sánh ghép cặp theo từng kỳ với baseline. P-value hai phía dùng kiểm định t trên
    /// các chênh lệch trung bình mỗi kỳ; phía hiển thị phải hiệu chỉnh khi thử nhiều strategy.
    /// </summary>
    public static PairedBacktestComparison Compare(BacktestResult strategy, BacktestResult baseline)
    {
        if (strategy.Game != baseline.Game)
            throw new ArgumentException("Hai kết quả backtest phải thuộc cùng game.", nameof(baseline));
        if (strategy.AverageMatchesByDraw.Count != baseline.AverageMatchesByDraw.Count)
            throw new ArgumentException("Hai kết quả backtest phải có cùng số kỳ kiểm tra.", nameof(baseline));

        var differences = strategy.AverageMatchesByDraw.Zip(
            baseline.AverageMatchesByDraw, (value, reference) => value - reference).ToArray();
        var mean = differences.Length == 0 ? double.NaN : differences.Average();
        var se = StandardError(differences);
        double t, p;
        if (differences.Length < 2 || double.IsNaN(se))
        {
            t = p = double.NaN;
        }
        else if (se == 0)
        {
            t = mean == 0 ? 0 : Math.CopySign(double.PositiveInfinity, mean);
            p = mean == 0 ? 1 : 0;
        }
        else
        {
            t = mean / se;
            p = 2 * (1 - StudentT.CDF(0, 1, differences.Length - 1, Math.Abs(t)));
        }
        return new PairedBacktestComparison(strategy.Strategy, mean, se, t, p, differences.Length);
    }

    internal static double StandardError(IReadOnlyList<double> values)
    {
        if (values.Count < 2) return double.NaN;
        var mean = values.Average();
        var sumSquares = values.Sum(value => (value - mean) * (value - mean));
        return Math.Sqrt(sumSquares / (values.Count - 1) / values.Count);
    }
}
