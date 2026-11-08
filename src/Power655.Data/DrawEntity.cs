using Power655.Core;

namespace Power655.Data;

/// <summary>
/// Dòng bảng <c>Draws</c>: (Game, DrawId) là khóa chính, Date, N1..N6, Special, Jackpot1, Jackpot2, Source, FetchedAt.
/// Lotto 5/35 không dùng N6; Mega 6/45 không có Special.
/// </summary>
public sealed class DrawEntity
{
    public string Game { get; set; } = Games.Power655.Code;
    public int DrawId { get; set; }
    public DateOnly Date { get; set; }
    public int N1 { get; set; }
    public int N2 { get; set; }
    public int N3 { get; set; }
    public int N4 { get; set; }
    public int N5 { get; set; }
    public int? N6 { get; set; }
    public int? Special { get; set; }
    public long? Jackpot1 { get; set; }
    public long? Jackpot2 { get; set; }
    public string Source { get; set; } = DrawSources.Seed;
    public DateTime FetchedAt { get; set; }

    public Draw ToDraw()
    {
        var game = Games.Get(Game);
        int[] numbers = N6 is { } n6 ? [N1, N2, N3, N4, N5, n6] : [N1, N2, N3, N4, N5];
        return new Draw(game, DrawId, Date, numbers, Special, Jackpot1, Jackpot2, Source);
    }

    public void CopyFrom(Draw draw, DateTime fetchedAtUtc)
    {
        var n = draw.Numbers;
        Game = draw.Game.Code;
        DrawId = draw.DrawId;
        Date = draw.Date;
        (N1, N2, N3, N4, N5) = (n[0], n[1], n[2], n[3], n[4]);
        N6 = n.Count > 5 ? n[5] : null;
        Special = draw.Special;
        Jackpot1 = draw.Jackpot1;
        Jackpot2 = draw.Jackpot2;
        Source = draw.Source;
        FetchedAt = fetchedAtUtc;
    }

    public static DrawEntity From(Draw draw, DateTime fetchedAtUtc)
    {
        var e = new DrawEntity();
        e.CopyFrom(draw, fetchedAtUtc);
        return e;
    }
}
