using Microsoft.EntityFrameworkCore;
using Power655.Core;

namespace Power655.Data;

public sealed record DrawConflict(Draw Existing, Draw Incoming, bool Overwritten);

public sealed record UpsertResult(int Inserted, int Updated, int Unchanged, IReadOnlyList<DrawConflict> Conflicts);

public sealed class DrawRepository(Power655DbContext db, TimeProvider? clock = null)
{
    private readonly TimeProvider _clock = clock ?? TimeProvider.System;

    public Task MigrateAsync(CancellationToken ct = default) => db.Database.MigrateAsync(ct);

    /// <summary>Số kỳ của một game, hoặc của mọi game nếu <paramref name="game"/> = null.</summary>
    public Task<int> CountAsync(Game? game = null, CancellationToken ct = default) =>
        game is null ? db.Draws.CountAsync(ct) : db.Draws.CountAsync(d => d.Game == game.Code, ct);

    /// <summary>Các kỳ của game theo DrawId tăng dần. <paramref name="last"/> &gt; 0 thì chỉ lấy N kỳ mới nhất.</summary>
    public async Task<IReadOnlyList<Draw>> GetAsync(Game game, int last = 0, CancellationToken ct = default)
    {
        IQueryable<DrawEntity> query = db.Draws.AsNoTracking()
            .Where(d => d.Game == game.Code)
            .OrderByDescending(d => d.DrawId);
        if (last > 0) query = query.Take(last);
        var rows = await query.ToListAsync(ct);
        return rows.Select(r => r.ToDraw()).OrderBy(d => d.DrawId).ToList();
    }

    /// <summary>
    /// Upsert theo (Game, DrawId) — chạy nhiều lần không tạo bản trùng.
    /// Cùng bộ số: cập nhật jackpot còn thiếu / nguồn ưu tiên cao hơn.
    /// Khác bộ số: ghi đè chỉ khi nguồn mới ưu tiên cao hơn (vietlott &gt; minhngoc &gt; seed), luôn báo xung đột.
    /// </summary>
    public async Task<UpsertResult> UpsertAsync(IEnumerable<Draw> draws, CancellationToken ct = default)
    {
        var incoming = draws.GroupBy(d => (d.Game.Code, d.DrawId))
            .Select(g => g.OrderByDescending(d => DrawSources.Priority(d.Source)).First())
            .ToList();
        var now = _clock.GetUtcNow().UtcDateTime;

        var existing = new Dictionary<(string, int), DrawEntity>();
        foreach (var group in incoming.GroupBy(d => d.Game.Code))
        {
            var code = group.Key;
            var ids = group.Select(d => d.DrawId).ToList();
            foreach (var row in await db.Draws.Where(d => d.Game == code && ids.Contains(d.DrawId)).ToListAsync(ct))
                existing[(row.Game, row.DrawId)] = row;
        }

        int inserted = 0, updated = 0, unchanged = 0;
        var conflicts = new List<DrawConflict>();

        foreach (var draw in incoming)
        {
            if (!existing.TryGetValue((draw.Game.Code, draw.DrawId), out var row))
            {
                db.Draws.Add(DrawEntity.From(draw, now));
                inserted++;
                continue;
            }

            var current = row.ToDraw();
            var higherPriority = DrawSources.Priority(draw.Source) > DrawSources.Priority(current.Source);

            if (!current.HasSameResultAs(draw))
            {
                conflicts.Add(new DrawConflict(current, draw, higherPriority));
                if (higherPriority) { row.CopyFrom(draw, now); updated++; }
                else unchanged++;
                continue;
            }

            var merged = current.With(
                date: higherPriority ? draw.Date : current.Date,
                jackpot1: draw.Jackpot1,
                jackpot2: draw.Jackpot2,
                source: higherPriority ? draw.Source : current.Source);

            if (merged.Equals(current)) { unchanged++; continue; }
            row.CopyFrom(merged, now);
            updated++;
        }

        await db.SaveChangesAsync(ct);
        return new UpsertResult(inserted, updated, unchanged, conflicts);
    }
}
