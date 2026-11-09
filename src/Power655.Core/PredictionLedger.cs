using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Power655.Core;

public sealed record PredictionTargetAssessment(
    bool IsReady, int? TargetDrawId, DateTimeOffset? TargetDrawAt, string? Reason);

/// <summary>
/// Bản dự đoán đã đóng băng. Timestamp do máy tạo, không phải chứng thực độc lập rằng file đã được công bố.
/// Có thể dùng commit trước giờ quay để kiểm tra việc công bố; không đọc Markdown cũ thành bằng chứng này.
/// </summary>
public sealed record LedgerPrediction(
    int SchemaVersion,
    string GameCode,
    int TargetDrawId,
    DateTimeOffset TargetDrawAt,
    DateTimeOffset GeneratedAt,
    int LatestInputDrawId,
    DateTimeOffset LatestInputDrawAt,
    string InputHash,
    int InputDrawCount,
    int Seed,
    string Configuration,
    string CodeVersion,
    IReadOnlyList<SuggestedTicket> Tickets)
{
    public PredictionSet ToPredictionSet() => new(
        Games.Get(GameCode), DateOnly.FromDateTime(DrawSchedule.ToVietnamTime(GeneratedAt)), LatestInputDrawId, Tickets)
    {
        GeneratedAt = GeneratedAt,
        TargetDrawAt = TargetDrawAt,
    };
}

/// <summary>Ledger JSON bất biến theo (game, kỳ mục tiêu), tách khỏi báo cáo trình bày có thể tạo lại.</summary>
public static class PredictionLedger
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static PredictionTargetAssessment AssessTarget(Game game, IReadOnlyList<Draw> history, DateTimeOffset now)
    {
        ArgumentNullException.ThrowIfNull(game);
        ArgumentNullException.ThrowIfNull(history);
        if (history.Count == 0) return Unready("Chưa có lịch sử.");
        if (history.Any(d => d.Game != game)) return Unready("Lịch sử lẫn game.");
        var ordered = history.OrderBy(d => d.DrawId).ToArray();
        for (var i = 1; i < ordered.Length; i++)
            if (ordered[i].DrawId == ordered[i - 1].DrawId || ordered[i].Date < ordered[i - 1].Date)
                return Unready("Lịch sử có kỳ trùng hoặc ngày quay không theo thứ tự.");
        var latest = ordered[^1];
        if (ResolveDrawTime(game, ordered, latest) is not { } latestAt)
            return Unready("Không xác định được ca quay của kỳ mới nhất; cần dữ liệu các kỳ liền trước.");
        if (latestAt > now) return Unready("Lịch sử chứa kết quả của kỳ chưa bắt đầu quay.");
        var targetAt = game.Schedule.NextDrawTime(latestAt);
        var targetId = checked(latest.DrawId + 1);
        if (targetAt <= now)
            return new(false, targetId, targetAt, "Kỳ ngay sau dữ liệu mới nhất đã bắt đầu; cần cập nhật kết quả trước khi tạo dự đoán mới.");
        return new(true, targetId, targetAt, null);
    }

    public static LedgerPrediction Create(
        Game game, IReadOnlyList<Draw> history, DateTimeOffset now, int seed,
        string configuration, string codeVersion, IReadOnlyList<SuggestedTicket> tickets)
    {
        var target = AssessTarget(game, history, now);
        if (!target.IsReady) throw new InvalidOperationException(target.Reason);
        var latest = history.MaxBy(d => d.DrawId)!;
        var entry = new LedgerPrediction(1, game.Code, target.TargetDrawId!.Value, target.TargetDrawAt!.Value,
            now, latest.DrawId, ResolveDrawTime(game, history, latest)!.Value, HashInputs(history), history.Count,
            seed, configuration, codeVersion,
            tickets.Select(t => new SuggestedTicket(t.Strategy, new Ticket(t.Ticket.Numbers.ToArray(), t.Ticket.Special))).ToArray());
        if (!IsValid(entry)) throw new ArgumentException("Cấu hình hoặc vé dự đoán không hợp lệ.", nameof(tickets));
        return entry;
    }

    /// <summary>Lần đầu ghi nguyên tử; chạy lại luôn trả bản đầu tiên và không ghi đè nó.</summary>
    public static LedgerPrediction SaveOrLoad(string directory, LedgerPrediction candidate, TimeProvider? clock = null)
    {
        if (!IsValid(candidate)) throw new ArgumentException("Dự đoán không hợp lệ.", nameof(candidate));
        clock ??= TimeProvider.System;
        var ledgerDir = Path.Combine(directory, "ledger");
        Directory.CreateDirectory(ledgerDir);
        var path = Path.Combine(ledgerDir, FileName(candidate));
        if (File.Exists(path)) return ReadExisting(path, candidate);
        void CheckCutoff()
        {
            var now = clock.GetUtcNow();
            if (now < candidate.GeneratedAt || now >= candidate.TargetDrawAt)
                throw new InvalidOperationException("Chỉ được lưu dự đoán mới trước giờ bắt đầu kỳ mục tiêu, với giờ tạo không nằm trong tương lai.");
        }
        CheckCutoff();
        var temp = Path.Combine(ledgerDir, $".{Guid.NewGuid():N}.tmp");
        try
        {
            File.WriteAllText(temp, JsonSerializer.Serialize(candidate, JsonOptions), new UTF8Encoding(false));
            CheckCutoff();
            try { File.Move(temp, path, overwrite: false); }
            catch (IOException) when (File.Exists(path)) { return ReadExisting(path, candidate); }
            // Deserialize also the first write so no caller-owned ticket list can mutate the retained instance.
            return ReadExisting(path, candidate);
        }
        finally
        {
            if (File.Exists(temp)) File.Delete(temp);
        }
    }

    /// <summary>Chỉ đọc ledger hợp lệ. File Markdown không có giờ tạo/cấu hình không được nâng thành dự đoán đã ghi nhận.</summary>
    public static IReadOnlyList<LedgerPrediction> Load(string directory)
    {
        var ledgerDir = Path.Combine(directory, "ledger");
        if (!Directory.Exists(ledgerDir)) return [];
        var result = new List<LedgerPrediction>();
        foreach (var path in Directory.EnumerateFiles(ledgerDir, "*.json"))
        {
            try
            {
                var entry = JsonSerializer.Deserialize<LedgerPrediction>(File.ReadAllText(path), JsonOptions);
                if (entry is not null && IsValid(entry) && Path.GetFileName(path) == FileName(entry)) result.Add(entry);
            }
            catch (JsonException) { /* File hỏng không trở thành bằng chứng. SaveOrLoad sẽ báo lỗi nếu đụng đúng khóa này. */ }
        }
        return result.OrderBy(e => e.TargetDrawAt).ThenBy(e => e.GameCode, StringComparer.Ordinal).ToList();
    }

    /// <summary>Mỗi game/kỳ chỉ một bản, ghi trước giờ quay; kiểm tra lại ngày/ca quay bằng kết quả thật.</summary>
    public static IReadOnlyList<PredictionReview> Review(IEnumerable<LedgerPrediction> entries, IEnumerable<Draw> draws)
    {
        var allDraws = draws.ToList();
        var byKey = allDraws.GroupBy(d => (d.Game.Code, d.DrawId))
            .Where(g => g.Count() == 1).ToDictionary(g => g.Key, g => g.Single());
        var valid = new List<(LedgerPrediction Entry, Draw Draw)>();
        foreach (var entry in entries)
        {
            if (!IsValid(entry) || !byKey.TryGetValue((entry.GameCode, entry.TargetDrawId), out var actual)
                || !byKey.TryGetValue((entry.GameCode, entry.LatestInputDrawId), out var input)) continue;
            var history = allDraws.Where(d => d.Game == actual.Game).ToList();
            if (ResolveDrawTime(actual.Game, history, actual) != entry.TargetDrawAt
                || ResolveDrawTime(actual.Game, history, input) != entry.LatestInputDrawAt) continue;
            var snapshot = history.Where(d => d.DrawId <= entry.LatestInputDrawId)
                .OrderBy(d => d.DrawId).TakeLast(entry.InputDrawCount).ToList();
            if (snapshot.Count != entry.InputDrawCount || HashInputs(snapshot) != entry.InputHash) continue;
            valid.Add((entry, actual));
        }
        return valid.GroupBy(x => (x.Entry.GameCode, x.Entry.TargetDrawId))
            .Select(g => g.OrderBy(x => x.Entry.GeneratedAt).First())
            .OrderBy(x => x.Entry.TargetDrawAt).ThenBy(x => x.Entry.GameCode, StringComparer.Ordinal)
            .Select(x => PredictionReview.Evaluate(x.Entry.ToPredictionSet(), x.Draw)).ToList();
    }

    /// <summary>
    /// Dấu vết lịch sử đầu vào: chỉ gồm kết quả quay (game, kỳ, ngày, số chính, số ĐB) — những gì chiến lược dùng.
    /// Không gồm Jackpot/nguồn: fetch sau có thể bổ sung Jackpot hoặc đổi nguồn dự phòng (minhngoc) sang vietlott
    /// cho cùng kết quả, việc đó không được làm mất dự đoán khỏi phần đối chiếu.
    /// </summary>
    public static string HashInputs(IEnumerable<Draw> history)
    {
        var canonical = string.Join('\n', history.OrderBy(d => d.Game.Code, StringComparer.Ordinal).ThenBy(d => d.DrawId)
            .Select(d => string.Join('|', d.Game.Code, d.DrawId.ToString(CultureInfo.InvariantCulture),
                d.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), string.Join(',', d.Numbers),
                d.Special?.ToString(CultureInfo.InvariantCulture) ?? "")));
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(canonical)));
    }

    /// <summary>
    /// CSV cũ chỉ có ngày: với game hai ca, cần hai kỳ liên tiếp trong ngày, hoặc kỳ đầu ngày nối sau
    /// hai kỳ của ngày trước. Không đoán ca bằng chẵn/lẻ của ID; thiếu bằng chứng thì từ chối.
    /// </summary>
    private static DateTimeOffset? ResolveDrawTime(Game game, IReadOnlyList<Draw> history, Draw draw)
    {
        if (!game.Schedule.IsDrawDay(draw.Date)) return null;
        var day = history.Where(d => d.Date == draw.Date).OrderBy(d => d.DrawId).ToArray();
        var slots = game.Schedule.DrawStartsAt.Count;
        if (day.Length == slots && day.Select(d => d.DrawId).Distinct().Count() == slots
            && day[^1].DrawId - day[0].DrawId == slots - 1)
        {
            var slot = Array.FindIndex(day, d => d.DrawId == draw.DrawId);
            return slot < 0 ? null : game.Schedule.DrawTime(draw.Date, slot);
        }
        if (slots == 2 && day.Length == 1)
        {
            var previousDate = draw.Date.AddDays(-1);
            while (!game.Schedule.IsDrawDay(previousDate)) previousDate = previousDate.AddDays(-1);
            var previous = history.Where(d => d.Date == previousDate).OrderBy(d => d.DrawId).ToArray();
            if (previous.Length == 2 && previous[0].DrawId == draw.DrawId - 2 && previous[1].DrawId == draw.DrawId - 1)
                return game.Schedule.DrawTime(draw.Date, 0);
        }
        return null;
    }

    private static bool IsValid(LedgerPrediction entry)
    {
        if (entry.SchemaVersion != 1 || Games.TryGet(entry.GameCode) is not { } game || game.Code != entry.GameCode
            || entry.LatestInputDrawId <= 0 || (long)entry.TargetDrawId != (long)entry.LatestInputDrawId + 1
            || entry.GeneratedAt >= entry.TargetDrawAt || entry.GeneratedAt < entry.LatestInputDrawAt
            || !game.Schedule.IsDrawTime(entry.LatestInputDrawAt) || !game.Schedule.IsDrawTime(entry.TargetDrawAt)
            || game.Schedule.NextDrawTime(entry.LatestInputDrawAt) != entry.TargetDrawAt
            || entry.InputDrawCount <= 0 || entry.InputHash is null || entry.InputHash.Length != 64 || !entry.InputHash.All(char.IsAsciiHexDigit)
            || string.IsNullOrWhiteSpace(entry.Configuration) || string.IsNullOrWhiteSpace(entry.CodeVersion)
            || entry.Tickets is null || entry.Tickets.Count == 0) return false;
        if (!entry.Tickets.All(s => s is not null && !string.IsNullOrWhiteSpace(s.Strategy) && s.Ticket is { } t
            && t.Numbers is not null && t.Numbers.Count == game.PickCount && t.Numbers.Distinct().Count() == game.PickCount
            && t.Numbers.All(game.IsValidNumber) && game.TicketHasSpecial == t.Special.HasValue
            && (t.Special is not { } special || game.IsValidSpecial(special)))) return false;
        return entry.Tickets.GroupBy(s => s.Strategy).All(g => g.Select(s =>
            string.Join(',', s.Ticket.Numbers.Order()) + "|" + s.Ticket.Special).Distinct().Count() == g.Count());
    }

    private static LedgerPrediction ReadExisting(string path, LedgerPrediction candidate)
    {
        try
        {
            var existing = JsonSerializer.Deserialize<LedgerPrediction>(File.ReadAllText(path), JsonOptions);
            if (existing is not null && IsValid(existing) && existing.GameCode == candidate.GameCode
                && existing.TargetDrawId == candidate.TargetDrawId) return existing;
        }
        catch (JsonException ex) { throw new InvalidDataException($"Ledger không hợp lệ: {path}. Không ghi đè.", ex); }
        throw new InvalidDataException($"Ledger không hợp lệ: {path}. Không ghi đè.");
    }

    private static string FileName(LedgerPrediction entry) => $"{entry.GameCode}-{entry.TargetDrawId:D6}.json";
    private static PredictionTargetAssessment Unready(string reason) => new(false, null, null, reason);
}
