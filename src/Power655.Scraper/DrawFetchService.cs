using Microsoft.Extensions.Logging;
using Power655.Core;

namespace Power655.Scraper;

public sealed record SourceConflict(Draw Kept, Draw Rejected);

public sealed record FetchReport(
    Game Game,
    IReadOnlyList<Draw> Draws,
    IReadOnlyList<string> SourcesUsed,
    IReadOnlyList<SourceConflict> Conflicts,
    IReadOnlyList<string> Warnings);

public sealed class FetchOptions
{
    /// <summary>Nghỉ giữa các trang để lịch sự với server (spec: ≥ 1s).</summary>
    public TimeSpan PageDelay { get; set; } = TimeSpan.FromSeconds(1);
}

/// <summary>
/// Điều phối lấy dữ liệu một game: nguồn chính (vietlott) → lỗi thì chuyển nguồn dự phòng (minhngoc).
/// Khi nguồn chính chạy được, vẫn lấy trang 1 nguồn dự phòng để cross-check và bổ sung kỳ mới
/// (trang vietlott có thể bị cache). Mâu thuẫn số → log lỗi, giữ vietlott.
/// Nguồn không hỗ trợ game (minhngoc với Lotto 5/35) thì bỏ qua.
/// </summary>
public sealed class DrawFetchService(
    IDrawSource primary, IDrawSource fallback, ILogger<DrawFetchService> logger, FetchOptions? options = null)
{
    private readonly FetchOptions _options = options ?? new FetchOptions();

    public IReadOnlyList<string> SourceNames => [primary.Name, fallback.Name];

    /// <param name="sourceName">null = tự động (chính + dự phòng); hoặc tên một nguồn để chỉ dùng nguồn đó.</param>
    public async Task<FetchReport> FetchAsync(Game game, int pages, string? sourceName, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(game);
        ArgumentOutOfRangeException.ThrowIfLessThan(pages, 1);
        var warnings = new List<string>();

        if (sourceName is not null)
        {
            var only = new[] { primary, fallback }.FirstOrDefault(s => s.Name == sourceName)
                ?? throw new ArgumentException($"Nguồn không hợp lệ '{sourceName}'. Chọn: {string.Join(", ", SourceNames)}.");
            if (!only.Supports(game))
            {
                Warn(warnings, $"{only.Name} không hỗ trợ {game.Name}.");
                return new FetchReport(game, [], [], [], warnings);
            }
            var (draws, _) = await FetchPagesAsync(game, only, pages, warnings, ct);
            return new FetchReport(game, draws, [only.Name], [], warnings);
        }

        IReadOnlyList<Draw> primaryDraws = [];
        var primaryFailed = true;
        if (primary.Supports(game))
            (primaryDraws, primaryFailed) = await FetchPagesAsync(game, primary, pages, warnings, ct);
        var used = new List<string>();
        if (primaryDraws.Count > 0) used.Add(primary.Name);

        IReadOnlyList<Draw> fallbackDraws = [];
        if (!fallback.Supports(game))
        {
            if (primaryFailed)
                Warn(warnings, $"Nguồn chính {primary.Name} lỗi và {fallback.Name} không hỗ trợ {game.Name}.");
        }
        else if (primaryFailed)
        {
            Warn(warnings, $"Nguồn chính {primary.Name} lỗi — chuyển sang nguồn dự phòng {fallback.Name}.");
            (fallbackDraws, _) = await FetchPagesAsync(game, fallback, pages, warnings, ct);
        }
        else
        {
            fallbackDraws = await FetchCrossCheckAsync(game, warnings, ct);
        }
        if (fallbackDraws.Count > 0) used.Add(fallback.Name);

        var merged = primaryDraws.ToDictionary(d => d.DrawId);
        var conflicts = new List<SourceConflict>();
        foreach (var d in fallbackDraws)
        {
            if (!merged.TryGetValue(d.DrawId, out var kept))
            {
                merged[d.DrawId] = d;
                continue;
            }
            if (!kept.HasSameResultAs(d))
            {
                conflicts.Add(new SourceConflict(kept, d));
                logger.LogError("Mâu thuẫn kỳ {DrawId}: {Primary} ({PrimarySource}) ≠ {Fallback} ({FallbackSource}); giữ {PrimarySource}.",
                    d.DrawId, kept, kept.Source, d, d.Source, kept.Source);
            }
            else if (kept.Jackpot1 is null && d.Jackpot1 is not null)
            {
                // cùng kết quả: lấy thêm jackpot từ nguồn dự phòng
                merged[d.DrawId] = kept.With(jackpot1: d.Jackpot1, jackpot2: d.Jackpot2);
            }
        }

        return new FetchReport(game, merged.Values.OrderByDescending(d => d.DrawId).ToList(), used, conflicts, warnings);
    }

    private async Task<IReadOnlyList<Draw>> FetchCrossCheckAsync(Game game, List<string> warnings, CancellationToken ct)
    {
        try
        {
            await Task.Delay(_options.PageDelay, ct);
            return await fallback.FetchPageAsync(game, 1, ct);
        }
        catch (Exception ex) when (VietlottSource.IsRecoverable(ex, ct))
        {
            Warn(warnings, $"Không cross-check được {game.Name} với {fallback.Name}: {ex.Message}");
            return [];
        }
    }

    /// <returns>Các kỳ lấy được, và cờ "nguồn lỗi ngay trang đầu".</returns>
    private async Task<(IReadOnlyList<Draw> Draws, bool FailedAtStart)> FetchPagesAsync(
        Game game, IDrawSource source, int pages, List<string> warnings, CancellationToken ct)
    {
        var result = new Dictionary<int, Draw>();
        for (var page = 1; page <= pages; page++)
        {
            if (page > 1) await Task.Delay(_options.PageDelay, ct);
            IReadOnlyList<Draw> draws;
            try
            {
                draws = await source.FetchPageAsync(game, page, ct);
            }
            catch (Exception ex) when (VietlottSource.IsRecoverable(ex, ct))
            {
                Warn(warnings, $"{source.Name} {game.Name} trang {page} lỗi: {ex.Message}");
                return (Ordered(result), result.Count == 0);
            }

            if (draws.Count == 0)
            {
                if (page == 1) Warn(warnings, $"{source.Name} {game.Name} trang 1 không có dữ liệu.");
                return (Ordered(result), result.Count == 0);
            }

            var added = draws.Count(d => result.TryAdd(d.DrawId, d));
            if (added == 0)
            {
                Warn(warnings, $"{source.Name} {game.Name} trang {page} trùng dữ liệu trang trước — dừng (kiểm tra PageIndex trong docs/vietlott-endpoint.md).");
                break;
            }
            logger.LogInformation("{Source} {Game} trang {Page}: {Count} kỳ", source.Name, game.Name, page, draws.Count);
        }
        return (Ordered(result), result.Count == 0);
    }

    private static List<Draw> Ordered(Dictionary<int, Draw> draws) =>
        draws.Values.OrderByDescending(d => d.DrawId).ToList();

    private void Warn(List<string> warnings, string message)
    {
        warnings.Add(message);
        logger.LogWarning("{Message}", message);
    }
}
