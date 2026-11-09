namespace Power655.Core;

/// <summary>Một bộ số đã gợi ý (đã lưu trong sổ dự đoán).</summary>
public sealed record SuggestedTicket(string Strategy, Ticket Ticket);

/// <summary>Các bộ gợi ý của một game, dành cho kỳ ngay sau <see cref="LatestDrawId"/>.</summary>
public sealed record PredictionSet(Game Game, DateOnly Date, int LatestDrawId, IReadOnlyList<SuggestedTicket> Tickets)
{
    public int TargetDrawId => LatestDrawId + 1;
    public DateTimeOffset? GeneratedAt { get; init; }
    public DateTimeOffset? TargetDrawAt { get; init; }
}

public sealed record TicketReview(SuggestedTicket Suggestion, int Main, bool Special, PrizeTier? Prize)
{
    public IReadOnlyList<int> Hits { get; init; } = [];
}

/// <summary>Đối chiếu một <see cref="PredictionSet"/> với kết quả thật của kỳ mục tiêu.</summary>
public sealed record PredictionReview(PredictionSet Set, Draw Draw, IReadOnlyList<TicketReview> Tickets)
{
    public static PredictionReview Evaluate(PredictionSet set, Draw draw)
    {
        if (draw.Game != set.Game) throw new ArgumentException("Khác game.", nameof(draw));
        if (draw.DrawId != set.TargetDrawId) throw new ArgumentException("Khác kỳ mục tiêu.", nameof(draw));
        var drawn = draw.Numbers.ToHashSet();
        var tickets = set.Tickets.Select(s =>
        {
            var (main, special) = set.Game.Odds.Match(s.Ticket, draw);
            return new TicketReview(s, main, special, set.Game.Odds.Classify(s.Ticket, draw))
            {
                Hits = s.Ticket.Numbers.Where(drawn.Contains).ToList(),
            };
        }).ToList();
        return new PredictionReview(set, draw, tickets);
    }
}

/// <summary>Tổng hợp theo chiến lược trên nhiều kỳ đã đối chiếu, so với kỳ vọng lý thuyết.</summary>
public sealed record StrategyScore(
    string Strategy, int Tickets, int TotalMatches, double ExpectedMatches, int Prizes, double ExpectedPrizes, int SpecialHits)
{
    public double AverageMatches => Tickets == 0 ? double.NaN : (double)TotalMatches / Tickets;

    public static IReadOnlyList<StrategyScore> Summarize(Game game, IEnumerable<PredictionReview> reviews)
    {
        var all = reviews.Where(r => r.Set.Game == game).SelectMany(r => r.Tickets).ToList();
        return all.GroupBy(t => t.Suggestion.Strategy)
            .OrderBy(g => g.Key == "random" ? 1 : 0).ThenBy(g => g.Key, StringComparer.Ordinal)
            .Select(g => new StrategyScore(
                g.Key,
                g.Count(),
                g.Sum(t => t.Main),
                g.Count() * game.Odds.ExpectedMatches,
                g.Count(t => t.Prize is not null),
                g.Count() * game.Odds.AnyPrizeProbability,
                g.Count(t => t.Special)))
            .ToList();
    }
}
