using System.Collections.ObjectModel;

namespace Power655.Core;

/// <summary>
/// Kết quả một kỳ quay của <see cref="Game"/>. Luôn hợp lệ sau khi khởi tạo:
/// <see cref="Game.PickCount"/> số chính phân biệt trong [1, MaxNumber] (sắp tăng dần) và số đặc biệt theo luật game:
/// Power 6/55 — 1 số trong [1,55] không trùng số chính; Lotto 5/35 — 1 số trong [1,12]; Mega 6/45 — không có.
/// </summary>
public sealed record Draw
{
    public Draw(
        Game game,
        int drawId,
        DateOnly date,
        IReadOnlyList<int> numbers,
        int? special,
        long? jackpot1 = null,
        long? jackpot2 = null,
        string source = DrawSources.Seed)
    {
        ArgumentNullException.ThrowIfNull(game);
        ArgumentNullException.ThrowIfNull(numbers);

        var label = $"{game.Name} kỳ {drawId}";
        if (drawId <= 0)
            throw new InvalidDrawException($"{game.Name}: DrawId phải > 0 (nhận {drawId}).");
        if (numbers.Count != game.PickCount)
            throw new InvalidDrawException($"{label}: cần đúng {game.PickCount} số chính (nhận {numbers.Count}).");
        if (numbers.Any(n => !game.IsValidNumber(n)))
            throw new InvalidDrawException($"{label}: số chính phải trong [{Game.MinNumber},{game.MaxNumber}] ({string.Join(",", numbers)}).");
        if (numbers.Distinct().Count() != game.PickCount)
            throw new InvalidDrawException($"{label}: số chính bị trùng ({string.Join(",", numbers)}).");
        switch (game.Special)
        {
            case SpecialKind.None when special is not null:
                throw new InvalidDrawException($"{label}: {game.Name} không có số đặc biệt (nhận {special}).");
            case not SpecialKind.None when special is null:
                throw new InvalidDrawException($"{label}: thiếu số đặc biệt.");
            case not SpecialKind.None when !game.IsValidSpecial(special!.Value):
                throw new InvalidDrawException($"{label}: số đặc biệt phải trong [{Game.MinNumber},{game.SpecialMax}] (nhận {special}).");
            case SpecialKind.BonusFromMainPool when numbers.Contains(special!.Value):
                throw new InvalidDrawException($"{label}: số đặc biệt {special} trùng với số chính.");
        }
        if (string.IsNullOrWhiteSpace(source))
            throw new InvalidDrawException($"{label}: thiếu Source.");

        Game = game;
        DrawId = drawId;
        Date = date;
        Numbers = new ReadOnlyCollection<int>(numbers.Order().ToArray());
        Special = special;
        Jackpot1 = jackpot1;
        Jackpot2 = jackpot2;
        Source = source;
    }

    public Game Game { get; }
    public int DrawId { get; }
    public DateOnly Date { get; }
    public IReadOnlyList<int> Numbers { get; }
    public int? Special { get; }
    public long? Jackpot1 { get; }
    public long? Jackpot2 { get; }
    public string Source { get; }

    /// <summary>Bản sao với nguồn/ngày/jackpot khác, giữ nguyên bộ số.</summary>
    public Draw With(DateOnly? date = null, long? jackpot1 = null, long? jackpot2 = null, string? source = null) =>
        new(Game, DrawId, date ?? Date, Numbers, Special, jackpot1 ?? Jackpot1, jackpot2 ?? Jackpot2, source ?? Source);

    /// <summary>Hai kỳ có cùng game và bộ số (số chính + đặc biệt) hay không, bỏ qua jackpot/nguồn.</summary>
    public bool HasSameResultAs(Draw other) =>
        Game == other.Game && Special == other.Special && Numbers.SequenceEqual(other.Numbers);

    public bool Equals(Draw? other) =>
        other is not null
        && DrawId == other.DrawId
        && Date == other.Date
        && HasSameResultAs(other)
        && Jackpot1 == other.Jackpot1
        && Jackpot2 == other.Jackpot2
        && Source == other.Source;

    public override int GetHashCode() => HashCode.Combine(Game.Code, DrawId, Date, Special, Numbers[0], Numbers[^1]);

    public override string ToString() =>
        $"{Game.Code} #{DrawId:D5} {Date:dd/MM/yyyy}: {string.Join(" ", Numbers.Select(n => n.ToString("D2")))}"
        + (Special is { } s ? $" | {s:D2}" : "");
}

/// <summary>Một vé gợi ý: các số chính (tăng dần) và số đặc biệt nếu game cho người chơi chọn (Lotto 5/35).</summary>
public sealed record Ticket(IReadOnlyList<int> Numbers, int? Special = null)
{
    public override string ToString() =>
        string.Join(" ", Numbers.Select(n => n.ToString("D2"))) + (Special is { } s ? $" | {s:D2}" : "");
}

public static class DrawSources
{
    public const string Vietlott = "vietlott";
    public const string MinhNgoc = "minhngoc";
    public const string Seed = "seed";

    /// <summary>Độ ưu tiên khi hai nguồn mâu thuẫn: vietlott &gt; minhngoc &gt; seed/csv.</summary>
    public static int Priority(string source) => source switch
    {
        Vietlott => 3,
        MinhNgoc => 2,
        _ => 1,
    };
}
