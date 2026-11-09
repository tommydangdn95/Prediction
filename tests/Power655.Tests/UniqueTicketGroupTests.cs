using Power655.Core;
using Power655.Core.Strategies;

namespace Power655.Tests;

public class UniqueTicketGroupTests
{
    [Fact]
    public void Same_main_numbers_in_another_order_are_the_same_ticket()
    {
        var queue = new Queue<Ticket>([
            new([1, 2, 3, 4, 5, 6]), new([6, 5, 4, 3, 2, 1]), new([2, 3, 4, 5, 6, 7])]);

        var group = TicketGroups.PickUnique(Games.Mega645, _ => queue.Dequeue(), 2, new Random(1));

        group.Should().HaveCount(2);
        group[0].Numbers.Should().Equal(1, 2, 3, 4, 5, 6);
        group[1].Numbers.Should().Equal(2, 3, 4, 5, 6, 7);
        queue.Should().BeEmpty();
    }

    [Fact]
    public void Different_lotto_specials_make_different_whole_tickets()
    {
        var queue = new Queue<Ticket>([new([1, 2, 3, 4, 5], 1), new([1, 2, 3, 4, 5], 2)]);
        var group = TicketGroups.PickUnique(Games.Lotto535, _ => queue.Dequeue(), 2, new Random(1));
        group.Select(t => t.Special).Should().Equal(1, 2);
    }

    [Fact]
    public void Existing_tickets_can_be_excluded_across_strategy_groups()
    {
        Ticket existing = new([1, 2, 3, 4, 5, 6]);
        var queue = new Queue<Ticket>([new([6, 5, 4, 3, 2, 1]), new([2, 3, 4, 5, 6, 7])]);
        var group = TicketGroups.PickUnique(Games.Mega645, _ => queue.Dequeue(), 1, new Random(1), [existing]);
        group.Single().Numbers.Should().Equal(2, 3, 4, 5, 6, 7);
    }

    [Fact]
    public void Exhausted_strategy_fails_instead_of_silently_substituting_random_tickets()
    {
        var calls = 0;
        var act = () => TicketGroups.PickUnique(Games.Mega645, _ =>
        {
            calls++;
            return new Ticket([1, 2, 3, 4, 5, 6]);
        }, 2, new Random(1));

        act.Should().Throw<InvalidOperationException>().WithMessage("*Không tạo đủ 2 vé khác nhau*");
        calls.Should().BeInRange(2, 100_000);
    }

    [Fact]
    public void Impossible_count_is_rejected_before_sampling()
    {
        var game = Games.Lotto535;
        var act = () => TicketGroups.PickUnique(game, _ => throw new Exception("Must not sample"),
            (int)game.Odds.TicketCombinations + 1, new Random(1));
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData("535")]
    [InlineData("645")]
    [InlineData("655")]
    public void Random_and_cover_groups_are_unique_and_reproducible(string code)
    {
        var game = Games.Get(code);
        var a = TicketGroups.PickUnique(game, new RandomPick(), [], 200, new Random(17));
        var b = TicketGroups.PickUnique(game, new RandomPick(), [], 200, new Random(17));
        a.Select(t => t.ToString()).Should().OnlyHaveUniqueItems().And.Equal(b.Select(t => t.ToString()));
        var cover = CoverPicker.Pick(game, 200, new Random(17));
        cover.Select(t => t.ToString()).Should().OnlyHaveUniqueItems();
    }
}

public class UniformDistinctOddsTests
{
    [Theory]
    [InlineData("535")]
    [InlineData("645")]
    [InlineData("655")]
    public void Distinct_baseline_matches_finite_population_probability(string code)
    {
        var game = Games.Get(code);
        var total = game.Odds.TicketCombinations;
        var winners = (long)Math.Round(total * game.Odds.AnyPrizeProbability);
        var losers = total - winners;
        PortfolioOdds.UniformDistinctAnyPrize(game, 0).Should().Be(0);
        PortfolioOdds.UniformDistinctAnyPrize(game, 1).Should().BeApproximately(game.Odds.AnyPrizeProbability, 1e-14);
        var exactTwo = 1 - (double)losers / total * (losers - 1) / (total - 1);
        PortfolioOdds.UniformDistinctAnyPrize(game, 2).Should().BeApproximately(exactTwo, 1e-14);
        PortfolioOdds.UniformDistinctAnyPrize(game, 8).Should().BeGreaterThan(PortfolioOdds.IndependentAnyPrize(game, 8));
        PortfolioOdds.UniformDistinctAnyPrize(game, (int)losers + 1).Should().Be(1);
        PortfolioOdds.UniformDistinctAnyPrize(game, (int)total).Should().Be(1);
    }

    [Fact]
    public void Distinct_baseline_rejects_counts_outside_ticket_space()
    {
        var negative = () => PortfolioOdds.UniformDistinctAnyPrize(Games.Mega645, -1);
        var excessive = () => PortfolioOdds.UniformDistinctAnyPrize(Games.Mega645, (int)Games.Mega645.Odds.TicketCombinations + 1);
        negative.Should().Throw<ArgumentOutOfRangeException>();
        excessive.Should().Throw<ArgumentOutOfRangeException>();
    }
}
