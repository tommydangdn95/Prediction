using Power655.Core;
using Power655.Core.Strategies;
using static Power655.Tests.TestData;

namespace Power655.Tests;

public sealed class EvaluationTests
{
    [Theory]
    [InlineData("535", 1, 10000000)]
    [InlineData("535", 2, 5000000)]
    [InlineData("535", 6, 10000)]
    [InlineData("645", 2, 300000)]
    [InlineData("645", 3, 30000)]
    [InlineData("655", 2, 40000000)]
    [InlineData("655", 3, 500000)]
    [InlineData("655", 4, 50000)]
    public void Base_prize_amounts_follow_the_published_fixed_prizes(string code, int tier, int amount)
    {
        var game = Games.Get(code);
        TicketEconomics.BasePrizeAmount(game, game.Prizes[tier]).Should().Be(amount);
    }

    [Theory]
    [InlineData("535", 0)]
    [InlineData("645", 0)]
    [InlineData("655", 0)]
    [InlineData("655", 1)]
    public void Jackpot_pool_is_never_assumed_to_be_a_single_tickets_cash_return(string code, int tier)
    {
        var game = Games.Get(code);
        TicketEconomics.BasePrizeAmount(game, game.Prizes[tier]).Should().Be(0);
    }

    [Fact]
    public void Winning_a_prize_can_still_lose_money_and_repetitions_do_not_multiply_the_stake()
    {
        var game = Games.Lotto535;
        var draws = Enumerable.Range(1, 4).Select(id => G(game, id, 1, 1, 2, 3, 4, 5)).ToList();
        var result = Backtester.RunGroups(game, draws, new TwoTickets(), groupSize: 2, train: 1, groups: 7);
        result.AnyWinRate.Should().Be(1);
        result.StakePerGroup.Should().Be(20000);
        result.AverageBasePayout.Should().Be(10000);
        result.AverageBaseNet.Should().Be(-10000);
        result.BaseRoi.Should().Be(-0.5);
        result.BaseBreakEvenRate.Should().Be(0);
        result.BaseProfitRate.Should().Be(0);
        result.AnyWinRateByDraw.Should().HaveCount(3);
        result.AnyWinConfidenceLow.Should().BeLessThan(1);
    }

    [Fact]
    public void Holdout_boundary_keeps_earlier_history_and_never_sees_test_draw()
    {
        var spy = new HistorySpy();
        var draws = Synthetic(Games.Mega645, 6, 8);
        var result = Backtester.Run(Games.Mega645, draws, spy, train: 2, runs: 1, testFromDrawId: 5);
        result.DrawIds.Should().Equal(5, 6);
        spy.SeenIds.Should().Equal(4, 5);
    }

    [Fact]
    public void Prize_comparison_tests_prizes_not_main_number_matches()
    {
        var baseline = Backtester.Run(Games.Mega645, Synthetic(Games.Mega645, 7, 1), new RandomPick(), train: 2);
        baseline = baseline with { AnyPrizeRateByDraw = [0, 0, 0, 0, 0] };
        var strategy = baseline with { Strategy = "other", AnyPrizeRateByDraw = [0.1, 0.2, 0.3, 0.1, 0.2] };
        Backtester.Compare(strategy, baseline).MeanDifference.Should().Be(0);
        var comparison = Backtester.ComparePrizeRate(strategy, baseline);
        comparison.MeanDifference.Should().BeApproximately(0.18, 1e-12);
        comparison.ConfidenceLow.Should().BeLessThan(comparison.MeanDifference);
        comparison.ConfidenceHigh.Should().BeGreaterThan(comparison.MeanDifference);
        comparison.Draws.Should().Be(5);
    }

    [Fact]
    public void Comparison_rejects_different_draw_ids_or_group_budgets()
    {
        var result = Backtester.Run(Games.Mega645, Synthetic(Games.Mega645, 4, 1), new RandomPick(), train: 2);
        var differentIds = result with { DrawIds = [100, 101] };
        var compare = () => Backtester.ComparePrizeRate(result, differentIds);
        compare.Should().Throw<ArgumentException>();
        var group = Backtester.RunGroups(Games.Mega645, Synthetic(Games.Mega645, 4, 1), new RandomPick(), 2, train: 2, groups: 2);
        var compareGroups = () => Backtester.CompareGroups(group, group with { GroupSize = 3 });
        compareGroups.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void No_observed_wins_does_not_produce_a_false_zero_width_confidence_interval()
    {
        var interval = Backtester.RateConfidence95([0, 0, 0, 0]);
        interval.Low.Should().Be(0);
        interval.High.Should().BeGreaterThan(0);
    }

    private sealed class TwoTickets : IPickStrategy
    {
        private int _next;
        public string Name => "two";
        public Ticket Pick(Game game, IReadOnlyList<Draw> history, Random rng) => new([6, 7, 8, 9, 10], ++_next % 2 + 1);
    }

    private sealed class HistorySpy : IPickStrategy
    {
        public List<int> SeenIds { get; } = [];
        public string Name => "spy";
        public Ticket Pick(Game game, IReadOnlyList<Draw> history, Random rng)
        {
            SeenIds.Add(history.Max(d => d.DrawId));
            return new Ticket([1, 2, 3, 4, 5, 6]);
        }
    }
}
