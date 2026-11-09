namespace Power655.Core.Strategies;

/// <summary>Sinh nhóm vé khác nhau hoàn toàn; số ĐB là một phần của vé Lotto 5/35.</summary>
public static class TicketGroups
{
    public static IReadOnlyList<Ticket> PickUnique(
        Game game, IPickStrategy strategy, IReadOnlyList<Draw> history, int count, Random rng,
        IEnumerable<Ticket>? exclude = null)
    {
        ArgumentNullException.ThrowIfNull(strategy);
        ValidateCount(game, count);
        return PickUnique(game, strategy.Prepare(game, history), count, rng, exclude);
    }

    public static IReadOnlyList<Ticket> PickUnique(
        Game game, Func<Random, Ticket> pick, int count, Random rng, IEnumerable<Ticket>? exclude = null)
    {
        ValidateCount(game, count);
        ArgumentNullException.ThrowIfNull(pick);
        ArgumentNullException.ThrowIfNull(rng);
        var tickets = new List<Ticket>(count);
        var seen = new HashSet<string>(StringComparer.Ordinal);
        if (exclude is not null)
            foreach (var ticket in exclude) seen.Add(Key(game, ticket));
        if ((long)seen.Count + count > game.Odds.TicketCombinations)
            throw new ArgumentOutOfRangeException(nameof(count), "Không còn đủ vé khác nhau sau khi loại các vé đã có.");
        var limit = AttemptLimit(count);
        for (long attempt = 0; tickets.Count < count && attempt < limit; attempt++)
        {
            var candidate = pick(rng);
            if (seen.Add(Key(game, candidate)))
                tickets.Add(new Ticket(candidate.Numbers.Order().ToArray(), candidate.Special));
        }
        if (tickets.Count != count)
            throw GenerationFailed(count, tickets.Count);
        return tickets;
    }

    internal static void ValidateCount(Game game, int count)
    {
        ArgumentNullException.ThrowIfNull(game);
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        if (count > game.Odds.TicketCombinations)
            throw new ArgumentOutOfRangeException(nameof(count),
                $"{game.Name} chỉ có {game.Odds.TicketCombinations} vé khác nhau.");
    }

    internal static long AttemptLimit(int count) => Math.Max(10_000L, 100L * count);

    internal static InvalidOperationException GenerationFailed(int requested, int generated) => new(
        $"Không tạo đủ {requested} vé khác nhau trong giới hạn lấy mẫu (đã tạo {generated}). " +
        "Chiến lược có thể hỗ trợ quá ít bộ số; hãy giảm số vé hoặc đổi chiến lược.");

    internal static string Key(Game game, Ticket ticket)
    {
        ArgumentNullException.ThrowIfNull(ticket);
        if (ticket.Numbers.Count != game.PickCount || ticket.Numbers.Distinct().Count() != game.PickCount
            || ticket.Numbers.Any(n => !game.IsValidNumber(n))
            || game.TicketHasSpecial != ticket.Special.HasValue
            || (ticket.Special is { } special && !game.IsValidSpecial(special)))
            throw new ArgumentException($"Vé {ticket} không hợp lệ cho {game.Name}.", nameof(ticket));
        return string.Join(",", ticket.Numbers.Order()) + "|" + ticket.Special;
    }
}
