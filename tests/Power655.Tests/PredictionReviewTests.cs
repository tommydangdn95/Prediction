using Power655.Core;
using static Power655.Tests.TestData;

namespace Power655.Tests;

public class PredictionReviewTests
{
    private static readonly PredictionSet LottoSet = new(Games.Lotto535, new DateOnly(2026, 11, 8), 996,
    [
        new("hot", new Ticket([2, 4, 20, 26, 35], 5)),
        new("hot", new Ticket([1, 13, 20, 27, 29], 6)),
        new("random", new Ticket([3, 8, 20, 30, 32], 9)),
    ]);

    [Fact]
    public void Scores_each_ticket_against_the_target_draw()
    {
        var draw = G(Games.Lotto535, 997, 6, 4, 16, 26, 27, 29);

        var review = PredictionReview.Evaluate(LottoSet, draw);

        review.Draw.DrawId.Should().Be(997);
        review.Tickets.Select(t => t.Main).Should().Equal(2, 2, 0);
        review.Tickets[0].Hits.Should().Equal(4, 26);
        review.Tickets[0].Prize.Should().BeNull();
        review.Tickets[1].Special.Should().BeTrue();
        review.Tickets[1].Prize!.Name.Should().Be("Khuyến khích");

        var scores = StrategyScore.Summarize(Games.Lotto535, [review]);
        scores.Select(s => s.Strategy).Should().Equal("hot", "random");
        scores[0].TotalMatches.Should().Be(4);
        scores[0].Prizes.Should().Be(1);
    }

    [Fact]
    public void Rejects_a_draw_that_is_not_the_target()
    {
        var act = () => PredictionReview.Evaluate(LottoSet, G(Games.Lotto535, 998, 6, 4, 16, 26, 27, 29));
        act.Should().Throw<ArgumentException>();
    }
}
