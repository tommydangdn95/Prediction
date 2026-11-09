using System.Globalization;
using System.Text.RegularExpressions;

namespace Power655.Core;

/// <summary>Một bộ số đã gợi ý trong báo cáo cũ.</summary>
public sealed record SuggestedTicket(string Strategy, Ticket Ticket);

/// <summary>Các bộ gợi ý của một game trong một file báo cáo, dành cho kỳ ngay sau <see cref="LatestDrawId"/>.</summary>
public sealed record PredictionSet(Game Game, DateOnly Date, int LatestDrawId, IReadOnlyList<SuggestedTicket> Tickets)
{
    public int TargetDrawId => LatestDrawId + 1;
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

/// <summary>
/// Đọc lại các bộ gợi ý từ báo cáo Markdown (định dạng của lệnh <c>report</c>) để kiểm tra với kết quả thật.
/// </summary>
public static partial class PredictionArchive
{
    [GeneratedRegex(@"^##\s+(?<name>.+?)\s*$")]
    private static partial Regex GameHeader();

    [GeneratedRegex(@"Kỳ mới nhất:\s*\*\*#(?<id>\d+)\*\*")]
    private static partial Regex LatestLine();

    [GeneratedRegex(@"^\|\s*(?<label>[^|`]+?)\s*\|\s*`(?<nums>[\d ]+)`\s*\|(?:\s*`(?<sp>\d+)`\s*\|)?")]
    private static partial Regex TicketRow();

    [GeneratedRegex(@"^(?<date>\d{4}-\d{2}-\d{2})\.md$")]
    private static partial Regex FileName();

    /// <summary>Ngày của file báo cáo dạng <c>yyyy-MM-dd.md</c>, hoặc null (vd LATEST.md).</summary>
    public static DateOnly? DateOf(string path)
    {
        var m = FileName().Match(Path.GetFileName(path));
        return m.Success ? DateOnly.ParseExact(m.Groups["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture) : null;
    }

    public static IReadOnlyList<PredictionSet> Parse(string markdown, DateOnly date)
    {
        var result = new List<PredictionSet>();
        Game? game = null;
        int? latest = null;
        var tickets = new List<SuggestedTicket>();

        void Flush()
        {
            if (game is not null && latest is not null && tickets.Count > 0)
                result.Add(new PredictionSet(game, date, latest.Value, tickets.ToList()));
            tickets.Clear();
            latest = null;
        }

        foreach (var raw in markdown.Split('\n'))
        {
            var line = raw.TrimEnd('\r');
            if (GameHeader().Match(line) is { Success: true } h)
            {
                Flush();
                game = Games.All.FirstOrDefault(g => g.Name == h.Groups["name"].Value);
                continue;
            }
            if (game is null) continue;
            if (LatestLine().Match(line) is { Success: true } l)
            {
                latest = int.Parse(l.Groups["id"].Value, CultureInfo.InvariantCulture);
                continue;
            }
            if (TicketRow().Match(line) is not { Success: true } row) continue;

            var numbers = row.Groups["nums"].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s, CultureInfo.InvariantCulture)).ToList();
            int? special = row.Groups["sp"].Success ? int.Parse(row.Groups["sp"].Value, CultureInfo.InvariantCulture) : null;
            if (numbers.Count != game.PickCount || numbers.Any(n => !game.IsValidNumber(n))) continue;
            if (game.TicketHasSpecial != special.HasValue) continue;
            var label = row.Groups["label"].Value.Split(' ')[0];
            tickets.Add(new SuggestedTicket(label, new Ticket(numbers.Order().ToList(), special)));
        }
        Flush();
        return result;
    }

    /// <summary>Đọc mọi file <c>yyyy-MM-dd.md</c> trong thư mục có ngày &lt; <paramref name="before"/>.</summary>
    public static IReadOnlyList<PredictionSet> Load(string directory, DateOnly before)
    {
        if (!Directory.Exists(directory)) return [];
        return Directory.EnumerateFiles(directory, "*.md")
            .Select(p => (Path: p, Date: DateOf(p)))
            .Where(x => x.Date is { } d && d < before)
            .OrderBy(x => x.Date)
            .SelectMany(x => Parse(File.ReadAllText(x.Path), x.Date!.Value))
            .ToList();
    }

    /// <summary>Đối chiếu các bộ đã có kết quả kỳ mục tiêu; bỏ qua bộ chưa có kết quả.</summary>
    public static IReadOnlyList<PredictionReview> Review(IEnumerable<PredictionSet> sets, IEnumerable<Draw> draws)
    {
        var byKey = draws.ToDictionary(d => (d.Game, d.DrawId));
        return sets.Where(s => byKey.ContainsKey((s.Game, s.TargetDrawId)))
            .Select(s => PredictionReview.Evaluate(s, byKey[(s.Game, s.TargetDrawId)]))
            .ToList();
    }
}
