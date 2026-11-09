using Power655.Core.Strategies;
using MathNet.Numerics.Distributions;

namespace Power655.Core;

public sealed record BacktestResult(
    Game Game, string Strategy, int TestDraws, int Runs, double AverageMatches,
    double RateAtLeast3, double? SpecialHitRate, double AnyPrizeRate,
    IReadOnlyDictionary<int, double> MatchDistribution, IReadOnlyList<double> AverageMatchesByDraw)
{
    public IReadOnlyList<int> DrawIds { get; init; } = [];
    public IReadOnlyList<double> AnyPrizeRateByDraw { get; init; } = [];
    public double StandardError => Backtester.StandardError(AverageMatchesByDraw);
}

/// <summary>So sánh thăm dò; CI t 95% và p hai phía tính trên kỳ thật, không trên lần mô phỏng.</summary>
public sealed record PairedBacktestComparison(
    string Strategy, double MeanDifference, double StandardError, double TStatistic, double PValue, int Draws)
{
    public double ConfidenceLow { get; init; } = double.NaN;
    public double ConfidenceHigh { get; init; } = double.NaN;
}

/// <summary>Mỗi phần tử ByDraw là trung bình các lần mô phỏng của MỘT kỳ quay thật.</summary>
public sealed record GroupBacktestResult(
    Game Game, string Strategy, int TestDraws, int GroupSize, int Groups, double AnyWinRate, double AverageWinningTickets)
{
    public IReadOnlyList<int> DrawIds { get; init; } = [];
    public IReadOnlyList<double> AnyWinRateByDraw { get; init; } = [];
    public IReadOnlyList<double> BasePayoutByDraw { get; init; } = [];
    public IReadOnlyList<double> BaseBreakEvenRateByDraw { get; init; } = [];
    public IReadOnlyList<double> BaseProfitRateByDraw { get; init; } = [];
    public double StakePerGroup => GroupSize * (double)TicketEconomics.TicketPrice;
    public double AverageBasePayout => Mean(BasePayoutByDraw);
    public double AverageBaseNet => AverageBasePayout - StakePerGroup;
    public double BaseRoi => AverageBaseNet / StakePerGroup;
    public double BaseBreakEvenRate => Mean(BaseBreakEvenRateByDraw);
    public double BaseProfitRate => Mean(BaseProfitRateByDraw);

    // Hoeffding 95% dùng số kỳ thật; không cho khoảng [0,0] khi chưa thấy giải.
    public double AnyWinConfidenceLow => Backtester.RateConfidence95(AnyWinRateByDraw).Low;
    public double AnyWinConfidenceHigh => Backtester.RateConfidence95(AnyWinRateByDraw).High;
    private static double Mean(IReadOnlyList<double> values) => values.Count == 0 ? double.NaN : values.Average();
}

/// <summary>Walk-forward: chỉ dùng các kỳ trước kỳ kiểm tra; giữ lịch sử trước testFromDrawId để huấn luyện.</summary>
public static class Backtester
{
    public static BacktestResult Run(
        Game game, IReadOnlyList<Draw> draws, IPickStrategy strategy,
        int train = 20, int runs = 1, int seed = 42, int? testFromDrawId = null)
    {
        ArgumentNullException.ThrowIfNull(strategy);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(runs);
        var ordered = ValidateAndOrder(game, draws, train, testFromDrawId);
        var rng = new Random(seed);
        var histogram = new long[game.PickCount + 1];
        var ids = new List<int>();
        var matchesByDraw = new List<double>();
        var prizesByDraw = new List<double>();
        long totalMatches = 0, atLeast3 = 0, specialHits = 0, prizes = 0, picks = 0;

        for (var t = train; t < ordered.Count; t++)
        {
            var actual = ordered[t];
            if (testFromDrawId is { } first && actual.DrawId < first) continue;
            var pick = strategy.Prepare(game, ordered.GetRange(0, t));
            long drawMatches = 0, drawPrizes = 0;
            for (var r = 0; r < runs; r++)
            {
                var ticket = pick(rng);
                var (matches, special) = game.Odds.Match(ticket, actual);
                histogram[matches]++;
                totalMatches += matches;
                drawMatches += matches;
                if (matches >= 3) atLeast3++;
                if (special) specialHits++;
                if (game.Odds.Classify(ticket, actual) is not null) { prizes++; drawPrizes++; }
                picks++;
            }
            ids.Add(actual.DrawId);
            matchesByDraw.Add((double)drawMatches / runs);
            prizesByDraw.Add((double)drawPrizes / runs);
        }

        double Rate(long count) => picks == 0 ? double.NaN : (double)count / picks;
        var distribution = Enumerable.Range(0, histogram.Length)
            .ToDictionary(k => k, k => picks == 0 ? 0 : (double)histogram[k] / picks);
        return new BacktestResult(game, strategy.Name, ids.Count, runs, Rate(totalMatches), Rate(atLeast3),
            game.TicketHasSpecial ? Rate(specialHits) : null, Rate(prizes), distribution, matchesByDraw)
        { DrawIds = ids, AnyPrizeRateByDraw = prizesByDraw };
    }

    /// <summary>Cùng số vé KHÁC NHAU mỗi nhóm; chuẩn bị chiến lược một lần mỗi kỳ.</summary>
    public static GroupBacktestResult RunGroups(
        Game game, IReadOnlyList<Draw> draws, IPickStrategy strategy, int groupSize,
        int train = 20, int groups = 50, int seed = 42, int? testFromDrawId = null)
    {
        ArgumentNullException.ThrowIfNull(strategy);
        return RunGroups(game, draws, strategy.Name, history =>
        {
            var pick = strategy.Prepare(game, history);
            return rng => TicketGroups.PickUnique(game, pick, groupSize, rng);
        }, groupSize, train, groups, seed, testFromDrawId);
    }

    public static GroupBacktestResult RunCover(
        Game game, IReadOnlyList<Draw> draws, int groupSize,
        int train = 20, int groups = 50, int seed = 42, int? testFromDrawId = null) =>
        RunGroups(game, draws, CoverPicker.Name, _ => rng => CoverPicker.Pick(game, groupSize, rng),
            groupSize, train, groups, seed, testFromDrawId);

    private static GroupBacktestResult RunGroups(
        Game game, IReadOnlyList<Draw> draws, string name,
        Func<IReadOnlyList<Draw>, Func<Random, IReadOnlyList<Ticket>>> prepare,
        int groupSize, int train, int groups, int seed, int? testFromDrawId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(groupSize);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(groups);
        var ordered = ValidateAndOrder(game, draws, train, testFromDrawId);
        var rng = new Random(seed);
        var ids = new List<int>();
        var winsByDraw = new List<double>();
        var payoutsByDraw = new List<double>();
        var breakEvenByDraw = new List<double>();
        var profitsByDraw = new List<double>();
        long anyWin = 0, winningTickets = 0, total = 0;
        var stake = groupSize * TicketEconomics.TicketPrice;
        for (var t = train; t < ordered.Count; t++)
        {
            var actual = ordered[t];
            if (testFromDrawId is { } first && actual.DrawId < first) continue;
            var pickGroup = prepare(ordered.GetRange(0, t));
            long drawWins = 0, drawBreakEven = 0, drawProfit = 0;
            decimal drawPayout = 0;
            for (var g = 0; g < groups; g++)
            {
                var tickets = pickGroup(rng);
                var wins = 0;
                decimal payout = 0;
                foreach (var ticket in tickets)
                {
                    var prize = game.Odds.Classify(ticket, actual);
                    if (prize is not null) wins++;
                    payout += TicketEconomics.BasePrizeAmount(game, prize);
                }
                if (wins > 0) { anyWin++; drawWins++; }
                if (payout >= stake) drawBreakEven++;
                if (payout > stake) drawProfit++;
                drawPayout += payout;
                winningTickets += wins;
                total++;
            }
            ids.Add(actual.DrawId);
            winsByDraw.Add((double)drawWins / groups);
            payoutsByDraw.Add((double)(drawPayout / groups));
            breakEvenByDraw.Add((double)drawBreakEven / groups);
            profitsByDraw.Add((double)drawProfit / groups);
        }
        return new GroupBacktestResult(game, name, ids.Count, groupSize, groups,
            total == 0 ? double.NaN : (double)anyWin / total,
            total == 0 ? double.NaN : (double)winningTickets / total)
        {
            DrawIds = ids, AnyWinRateByDraw = winsByDraw, BasePayoutByDraw = payoutsByDraw,
            BaseBreakEvenRateByDraw = breakEvenByDraw, BaseProfitRateByDraw = profitsByDraw,
        };
    }

    public static PairedBacktestComparison Compare(BacktestResult strategy, BacktestResult baseline)
    {
        ValidatePair(strategy.Game, baseline.Game, strategy.DrawIds, baseline.DrawIds,
            strategy.AverageMatchesByDraw.Count, baseline.AverageMatchesByDraw.Count);
        return CompareValues(strategy.Strategy, strategy.AverageMatchesByDraw, baseline.AverageMatchesByDraw);
    }

    public static PairedBacktestComparison ComparePrizeRate(BacktestResult strategy, BacktestResult baseline)
    {
        ValidatePair(strategy.Game, baseline.Game, strategy.DrawIds, baseline.DrawIds,
            strategy.AnyPrizeRateByDraw.Count, baseline.AnyPrizeRateByDraw.Count);
        return CompareValues(strategy.Strategy, strategy.AnyPrizeRateByDraw, baseline.AnyPrizeRateByDraw);
    }

    public static PairedBacktestComparison CompareGroups(GroupBacktestResult strategy, GroupBacktestResult baseline)
    {
        ValidateGroupPair(strategy, baseline);
        return CompareValues(strategy.Strategy, strategy.AnyWinRateByDraw, baseline.AnyWinRateByDraw);
    }

    public static PairedBacktestComparison CompareGroupBaseNet(GroupBacktestResult strategy, GroupBacktestResult baseline)
    {
        ValidateGroupPair(strategy, baseline);
        if (strategy.BasePayoutByDraw.Count != strategy.DrawIds.Count
            || baseline.BasePayoutByDraw.Count != baseline.DrawIds.Count)
            throw new ArgumentException("Thiếu tiền giải theo kỳ.", nameof(baseline));
        // Cùng số vé/giá vé, chênh lệch tiền ròng bằng chênh lệch tiền giải cơ bản.
        return CompareValues(strategy.Strategy, strategy.BasePayoutByDraw, baseline.BasePayoutByDraw);
    }

    private static void ValidateGroupPair(GroupBacktestResult strategy, GroupBacktestResult baseline)
    {
        if (strategy.GroupSize != baseline.GroupSize)
            throw new ArgumentException("Hai nhóm phải có cùng số vé/ngân sách.", nameof(baseline));
        ValidatePair(strategy.Game, baseline.Game, strategy.DrawIds, baseline.DrawIds,
            strategy.AnyWinRateByDraw.Count, baseline.AnyWinRateByDraw.Count);
    }

    private static void ValidatePair(Game game, Game baselineGame, IReadOnlyList<int> ids,
        IReadOnlyList<int> baselineIds, int count, int baselineCount)
    {
        if (game != baselineGame) throw new ArgumentException("Hai kết quả phải thuộc cùng game.");
        if (count != baselineCount || ids.Count != count || baselineIds.Count != baselineCount
            || !ids.SequenceEqual(baselineIds) || ids.Distinct().Count() != ids.Count)
            throw new ArgumentException("Hai kết quả phải có cùng mã kỳ kiểm tra duy nhất, theo cùng thứ tự.");
    }

    private static PairedBacktestComparison CompareValues(string strategy, IReadOnlyList<double> values, IReadOnlyList<double> baseline)
    {
        var differences = values.Zip(baseline, (value, reference) => value - reference).ToArray();
        var mean = differences.Length == 0 ? double.NaN : differences.Average();
        var se = StandardError(differences);
        double t, p;
        if (differences.Length < 2 || double.IsNaN(se)) t = p = double.NaN;
        else if (se == 0)
        {
            t = mean == 0 ? 0 : Math.CopySign(double.PositiveInfinity, mean);
            // Không suy ra chắc chắn có lợi thế từ mẫu không có biến thiên.
            p = mean == 0 ? 1 : double.NaN;
        }
        else
        {
            t = mean / se;
            p = 2 * StudentT.CDF(0, 1, differences.Length - 1, -Math.Abs(t));
        }
        var margin = differences.Length >= 2 && se > 0
            ? StudentT.InvCDF(0, 1, differences.Length - 1, 0.975) * se : double.NaN;
        return new PairedBacktestComparison(strategy, mean, se, t, p, differences.Length)
        { ConfidenceLow = mean - margin, ConfidenceHigh = mean + margin };
    }

    /// <summary>Khoảng Hoeffding 95% cho trung bình tỷ lệ [0,1], đơn vị mẫu là kỳ quay thật.</summary>
    public static (double Low, double High) RateConfidence95(IReadOnlyList<double> values)
    {
        if (values.Count == 0) return (double.NaN, double.NaN);
        var margin = Math.Sqrt(Math.Log(40) / (2 * values.Count));
        var mean = values.Average();
        return (Math.Max(0, mean - margin), Math.Min(1, mean + margin));
    }

    private static List<Draw> ValidateAndOrder(Game game, IReadOnlyList<Draw> draws, int train, int? testFromDrawId)
    {
        ArgumentNullException.ThrowIfNull(game);
        ArgumentNullException.ThrowIfNull(draws);
        ArgumentOutOfRangeException.ThrowIfNegative(train);
        if (testFromDrawId is <= 0) throw new ArgumentOutOfRangeException(nameof(testFromDrawId));
        if (draws.FirstOrDefault(d => d.Game != game) is { } other)
            throw new ArgumentException($"Dữ liệu lẫn game: cần {game.Name}, thấy {other.Game.Name}.", nameof(draws));
        if (draws.Select(d => d.DrawId).Distinct().Count() != draws.Count)
            throw new ArgumentException("Dữ liệu trùng mã kỳ; không thể backtest độc lập.", nameof(draws));
        return draws.OrderBy(d => d.DrawId).ToList();
    }

    internal static double StandardError(IReadOnlyList<double> values)
    {
        if (values.Count < 2) return double.NaN;
        var mean = values.Average();
        var sumSquares = values.Sum(value => (value - mean) * (value - mean));
        return Math.Sqrt(sumSquares / (values.Count - 1) / values.Count);
    }
}
