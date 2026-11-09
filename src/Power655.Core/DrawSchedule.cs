namespace Power655.Core;

/// <summary>
/// Lịch quay của một game (giờ Việt Nam). Mỗi ngày quay có một hoặc nhiều mốc "kết quả đã có":
/// Power 6/55 T3/T5/T7 và Mega 6/45 T4/T6/CN quay 18:00 → có sau 18:45;
/// Lotto 5/35 quay 13:00 và 21:00 mỗi ngày → có sau 13:45 / 21:45.
/// </summary>
public sealed class DrawSchedule
{
    public DrawSchedule(IReadOnlyList<DayOfWeek> drawDays, IReadOnlyList<TimeOnly> resultsAvailableAt)
    {
        if (drawDays.Count == 0) throw new ArgumentException("Cần ít nhất 1 ngày quay.", nameof(drawDays));
        if (resultsAvailableAt.Count == 0) throw new ArgumentException("Cần ít nhất 1 mốc giờ.", nameof(resultsAvailableAt));
        DrawDays = drawDays;
        ResultsAvailableAt = resultsAvailableAt.Order().ToArray();
    }

    public IReadOnlyList<DayOfWeek> DrawDays { get; }

    /// <summary>Các mốc trong ngày (tăng dần) mà kết quả kỳ tương ứng coi là đã có.</summary>
    public IReadOnlyList<TimeOnly> ResultsAvailableAt { get; }

    public static TimeZoneInfo VietnamTimeZone { get; } = ResolveVietnamTimeZone();

    public bool IsDrawDay(DateOnly date) => DrawDays.Contains(date.DayOfWeek);

    public static DateTime ToVietnamTime(DateTimeOffset instant) =>
        TimeZoneInfo.ConvertTime(instant, VietnamTimeZone).DateTime;

    /// <summary>Ngày quay gần nhất mà kết quả đã có tại thời điểm <paramref name="now"/>.</summary>
    public DateOnly LatestAvailableDrawDate(DateTimeOffset now)
    {
        var local = ToVietnamTime(now);
        var date = DateOnly.FromDateTime(local);
        if (IsDrawDay(date) && TimeOnly.FromDateTime(local) >= ResultsAvailableAt[0])
            return date;
        do date = date.AddDays(-1); while (!IsDrawDay(date));
        return date;
    }

    /// <summary>Hôm nay còn kỳ nào chưa có kết quả không; trả mốc giờ kế tiếp hoặc null.</summary>
    public TimeOnly? PendingToday(DateTimeOffset now)
    {
        var local = ToVietnamTime(now);
        if (!IsDrawDay(DateOnly.FromDateTime(local))) return null;
        var time = TimeOnly.FromDateTime(local);
        return ResultsAvailableAt.Cast<TimeOnly?>().FirstOrDefault(t => t > time);
    }

    /// <summary>Thời điểm nên fetch tiếp theo (mốc "kết quả đã có" kế tiếp, giờ Việt Nam).</summary>
    public DateTimeOffset NextFetchTime(DateTimeOffset now) => NextFetchTime(now, resultsToday: 0);

    /// <summary>
    /// Như <see cref="NextFetchTime(DateTimeOffset)"/> nhưng bỏ qua các kỳ hôm nay đã có kết quả
    /// (<paramref name="resultsToday"/> kỳ đầu ngày) — kết quả thường có trước mốc dự kiến.
    /// </summary>
    public DateTimeOffset NextFetchTime(DateTimeOffset now, int resultsToday)
    {
        var local = ToVietnamTime(now);
        var date = DateOnly.FromDateTime(local);
        if (IsDrawDay(date))
        {
            var time = TimeOnly.FromDateTime(local);
            for (var i = Math.Max(0, resultsToday); i < ResultsAvailableAt.Count; i++)
                if (ResultsAvailableAt[i] > time) return At(date, ResultsAvailableAt[i]);
        }
        do date = date.AddDays(1); while (!IsDrawDay(date));
        return At(date, ResultsAvailableAt[0]);
    }

    private static DateTimeOffset At(DateOnly date, TimeOnly slot)
    {
        var localTarget = date.ToDateTime(slot);
        return new DateTimeOffset(localTarget, VietnamTimeZone.GetUtcOffset(localTarget));
    }

    private static TimeZoneInfo ResolveVietnamTimeZone()
    {
        foreach (var id in new[] { "Asia/Ho_Chi_Minh", "SE Asia Standard Time" })
        {
            try { return TimeZoneInfo.FindSystemTimeZoneById(id); }
            catch (TimeZoneNotFoundException) { }
            catch (InvalidTimeZoneException) { }
        }
        return TimeZoneInfo.CreateCustomTimeZone("UTC+7", TimeSpan.FromHours(7), "UTC+7", "UTC+7");
    }
}
