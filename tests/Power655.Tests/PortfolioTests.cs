using Power655.Core;
using Power655.Core.Strategies;
using static Power655.Tests.TestData;

namespace Power655.Tests;

public class PortfolioOddsTests
{
    [Theory]
    [InlineData("535")]
    [InlineData("645")]
    [InlineData("655")]
    public void Single_ticket_matches_the_prize_table(string code)
    {
        var game = Games.Get(code);
        var ticket = CoverPicker.Pick(game, 1, new Random(1))[0];

        PortfolioOdds.AnyPrizeByPrefix(game, [ticket]).Should().ContainSingle()
            .Which.Should().BeApproximately(game.Odds.AnyPrizeProbability, 1e-12);
        PortfolioOdds.AtLeastByPrefix(game, [ticket], 3).Should().ContainSingle()
            .Which.Should().BeApproximately(game.Odds.ProbabilityAtLeast(3), 1e-12);
    }

    [Fact]
    public void Duplicate_ticket_adds_nothing()
    {
        var game = Games.Mega645;
        var t = new Ticket([1, 2, 3, 4, 5, 6]);

        var p = PortfolioOdds.AnyPrizeByPrefix(game, [t, t]);

        p[1].Should().BeApproximately(p[0], 1e-15);
    }

    [Fact]
    public void Two_disjoint_mega_tickets_follow_inclusion_exclusion()
    {
        var game = Games.Mega645;
        Ticket a = new([1, 2, 3, 4, 5, 6]), b = new([7, 8, 9, 10, 11, 12]);
        // cả hai vé cùng trúng chỉ khi kết quả có đúng 3 số của mỗi vé: C(6,3)² bộ
        var both = 20.0 * 20 / game.Odds.TotalCombinations;

        var p = PortfolioOdds.AnyPrizeByPrefix(game, [a, b]);

        p[1].Should().BeApproximately(2 * game.Odds.AnyPrizeProbability - both, 1e-12);
    }

    [Fact]
    public void Lotto_partition_with_distinct_specials_matches_closed_form()
    {
        var game = Games.Lotto535;
        // 7 vé rời nhau phủ đủ 35 số, ĐB 1..7. Hai vé không thể cùng trùng ≥3 số (3 + 3 > 5), nên
        // P(không vé nào trúng) = (1 − 7·P(≥3)) × P(ĐB quay ra không thuộc 7 số ĐB của vé) = (1 − 7·P(≥3)) × 5/12
        var tickets = Enumerable.Range(0, 7)
            .Select(i => new Ticket(Enumerable.Range(i * 5 + 1, 5).ToArray(), i + 1)).ToList();
        var atLeast3 = game.Odds.ProbabilityAtLeast(3);

        PortfolioOdds.AnyPrizeByPrefix(game, tickets)[6]
            .Should().BeApproximately(1 - (1 - 7 * atLeast3) * 5 / 12, 1e-12);
        PortfolioOdds.AtLeastByPrefix(game, tickets, 3)[6].Should().BeApproximately(7 * atLeast3, 1e-12);
    }

    [Fact]
    public void Twelve_distinct_specials_guarantee_a_lotto_prize()
    {
        var game = Games.Lotto535;
        var tickets = CoverPicker.Pick(game, 12, new Random(4));

        PortfolioOdds.AnyPrizeByPrefix(game, tickets)[^1].Should().BeApproximately(1, 1e-12);
    }

    [Theory]
    [InlineData("535")]
    [InlineData("645")]
    [InlineData("655")]
    public void Cover_beats_the_same_number_of_random_tickets(string code)
    {
        var game = Games.Get(code);
        var p = PortfolioOdds.AnyPrizeByPrefix(game, CoverPicker.Pick(game, 6, new Random(5)));

        p.Should().BeInAscendingOrder();
        for (var k = 2; k <= 6; k++)
            p[k - 1].Should().BeGreaterThan(PortfolioOdds.IndependentAnyPrize(game, k));
    }

    [Fact]
    public void Rejects_invalid_tickets()
    {
        var act = () => PortfolioOdds.AnyPrizeByPrefix(Games.Lotto535, [new Ticket([1, 2, 3, 4, 5])]);
        act.Should().Throw<ArgumentException>().WithMessage("*ĐB*");
    }
}

public class CoverPickerTests
{
    [Theory]
    [InlineData("535", 7)]
    [InlineData("645", 7)]
    [InlineData("655", 9)]
    public void First_round_is_disjoint_and_later_tickets_overlap_at_most_one_number(string code, int round)
    {
        var game = Games.Get(code);
        var tickets = CoverPicker.Pick(game, round + 3, new Random(3));

        tickets.Should().HaveCount(round + 3);
        foreach (var t in tickets)
        {
            t.Numbers.Should().HaveCount(game.PickCount).And.OnlyHaveUniqueItems().And.BeInAscendingOrder();
            t.Numbers.Should().OnlyContain(n => game.IsValidNumber(n));
        }
        tickets.Take(round).SelectMany(t => t.Numbers).Should().OnlyHaveUniqueItems();
        for (var i = round; i < tickets.Count; i++)
            for (var j = 0; j < i; j++)
                tickets[i].Numbers.Intersect(tickets[j].Numbers).Count().Should().BeLessThanOrEqualTo(1);

        if (game.TicketHasSpecial)
            tickets.Select(t => t.Special!.Value).Should().OnlyHaveUniqueItems().And.OnlyContain(s => game.IsValidSpecial(s));
        else
            tickets.Should().OnlyContain(t => t.Special == null);
    }

    [Fact]
    public void Is_reproducible_with_seed()
    {
        var a = CoverPicker.Pick(Games.Power655, 8, new Random(11));
        var b = CoverPicker.Pick(Games.Power655, 8, new Random(11));
        a.Select(t => t.ToString()).Should().Equal(b.Select(t => t.ToString()));
    }
}

public class GroupBacktestTests
{
    [Fact]
    public void Random_groups_match_independent_formula_and_cover_matches_exact_odds()
    {
        var game = Games.Lotto535;
        var draws = Synthetic(game, 60, 21);

        var random = Backtester.RunGroups(game, draws, new RandomPick(), groupSize: 8, train: 20, groups: 200, seed: 1);
        var cover = Backtester.RunCover(game, draws, groupSize: 8, train: 20, groups: 200, seed: 1);

        random.TestDraws.Should().Be(40);
        // 40 kỳ × 200 nhóm: sai số chuẩn ≈ 0,006
        random.AnyWinRate.Should().BeApproximately(PortfolioOdds.IndependentAnyPrize(game, 8), 0.03);
        var exact = PortfolioOdds.AnyPrizeByPrefix(game, CoverPicker.Pick(game, 8, new Random(2)))[^1];
        cover.AnyWinRate.Should().BeApproximately(exact, 0.03);
        cover.AnyWinRate.Should().BeGreaterThan(random.AnyWinRate + 0.05);
        // số vé trúng trung bình không đổi theo cách chọn
        cover.AverageWinningTickets.Should().BeApproximately(8 * game.Odds.AnyPrizeProbability, 0.05);
        random.AverageWinningTickets.Should().BeApproximately(8 * game.Odds.AnyPrizeProbability, 0.05);
    }
}

public class PreparedStrategyTests
{
    [Theory]
    [InlineData("hot")]
    [InlineData("cold")]
    [InlineData("balanced")]
    public void Backtest_with_prepared_strategy_equals_picking_one_by_one(string name)
    {
        var game = Games.Lotto535;
        var draws = Synthetic(game, 40, 2);
        var strategy = StrategyCatalog.Create(name);

        var fast = Backtester.Run(game, draws, strategy, 20, 30, seed: 9);
        var slow = Backtester.Run(game, draws, new PickOnly(strategy), 20, 30, seed: 9);

        fast.AverageMatchesByDraw.Should().Equal(slow.AverageMatchesByDraw);
        fast.AnyPrizeRate.Should().Be(slow.AnyPrizeRate);
    }

    /// <summary>Chỉ cài <see cref="IPickStrategy.Pick"/> → backtest dùng Prepare mặc định (gọi Pick từng vé).</summary>
    private sealed class PickOnly(IPickStrategy inner) : IPickStrategy
    {
        public string Name => inner.Name;
        public Ticket Pick(Game game, IReadOnlyList<Draw> history, Random rng) => inner.Pick(game, history, rng);
    }
}
