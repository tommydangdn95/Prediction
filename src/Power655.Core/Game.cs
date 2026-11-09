namespace Power655.Core;

/// <summary>Cách một game dùng số đặc biệt.</summary>
public enum SpecialKind
{
    /// <summary>Không có số đặc biệt (Mega 6/45).</summary>
    None,

    /// <summary>
    /// Máy quay thêm 1 số từ cùng lồng, khác các số chính; người chơi không chọn riêng (Power 6/55).
    /// Vé "trùng ĐB" khi số đặc biệt nằm trong các số của vé.
    /// </summary>
    BonusFromMainPool,

    /// <summary>Số đặc biệt quay từ lồng riêng [1, SpecialMax]; người chơi chọn thêm 1 số (Lotto 5/35).</summary>
    SeparatePool,
}

/// <summary>
/// Định nghĩa một loại xổ số Vietlott: số lượng số, khoảng số, số đặc biệt, lịch quay và cơ cấu giải.
/// Dùng <see cref="Games"/> để lấy các game có sẵn.
/// </summary>
public sealed class Game
{
    internal Game(
        string code, string name, int pickCount, int maxNumber,
        SpecialKind special, int specialMax, DrawSchedule schedule,
        IReadOnlyList<PrizeTier> prizes)
    {
        Code = code;
        Name = name;
        PickCount = pickCount;
        MaxNumber = maxNumber;
        Special = special;
        SpecialMax = special == SpecialKind.None ? 0 : specialMax;
        Schedule = schedule;
        Prizes = prizes;
        AllNumbers = Enumerable.Range(MinNumber, maxNumber).ToArray();
        AllSpecialNumbers = Enumerable.Range(MinNumber, SpecialMax).ToArray();
        Odds = new GameOdds(this);
    }

    public const int MinNumber = 1;

    /// <summary>Mã ngắn dùng trong CLI/DB/CSV: "535", "645", "655".</summary>
    public string Code { get; }

    public string Name { get; }

    /// <summary>Số lượng số chính mỗi kỳ / mỗi vé.</summary>
    public int PickCount { get; }

    public int MaxNumber { get; }

    public SpecialKind Special { get; }

    /// <summary>Số đặc biệt lớn nhất (= MaxNumber với Power 6/55, 12 với Lotto 5/35, 0 nếu không có).</summary>
    public int SpecialMax { get; }

    public bool HasSpecial => Special != SpecialKind.None;

    /// <summary>Người chơi có tự chọn số đặc biệt trên vé không (chỉ Lotto 5/35).</summary>
    public bool TicketHasSpecial => Special == SpecialKind.SeparatePool;

    /// <summary>Ranh giới nhỏ/lớn: nhỏ = 1..LowHighBoundary (27 với 6/55, 22 với 6/45, 17 với 5/35).</summary>
    public int LowHighBoundary => MaxNumber / 2;

    public IReadOnlyList<int> AllNumbers { get; }

    public IReadOnlyList<int> AllSpecialNumbers { get; }

    public DrawSchedule Schedule { get; }

    /// <summary>Các hạng giải, từ cao xuống thấp.</summary>
    public IReadOnlyList<PrizeTier> Prizes { get; }

    public GameOdds Odds { get; }

    public bool IsValidNumber(int n) => n >= MinNumber && n <= MaxNumber;

    public bool IsValidSpecial(int n) => n >= MinNumber && n <= SpecialMax;

    public override string ToString() => Name;
}

/// <summary>
/// Một hạng giải: số chính trùng trong [MinMain, MaxMain] và (nếu <see cref="SpecialMatch"/> khác null)
/// có/không trùng số đặc biệt.
/// </summary>
public sealed record PrizeTier(string Name, string Condition, int MinMain, int MaxMain, bool? SpecialMatch = null)
{
    public PrizeTier(string name, string condition, int mainMatches, bool? specialMatch = null)
        : this(name, condition, mainMatches, mainMatches, specialMatch) { }

    /// <summary><paramref name="specialMatch"/> chỉ có nghĩa với game có số đặc biệt.</summary>
    public bool IsWonBy(int mainMatches, bool specialMatch) =>
        mainMatches >= MinMain && mainMatches <= MaxMain && (SpecialMatch is null || SpecialMatch == specialMatch);
}

public static class Games
{
    public static Game Power655 { get; } = new(
        "655", "Power 6/55", 6, 55, SpecialKind.BonusFromMainPool, 55,
        new DrawSchedule([DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday], [new TimeOnly(18, 45)], [new TimeOnly(18, 0)]),
        [
            new("Jackpot 1", "6 số", 6),
            new("Jackpot 2", "5 số + ĐB", 5, true),
            new("Giải Nhất", "5 số", 5, false),
            new("Giải Nhì", "4 số", 4),
            new("Giải Ba", "3 số", 3),
        ]);

    public static Game Mega645 { get; } = new(
        "645", "Mega 6/45", 6, 45, SpecialKind.None, 0,
        new DrawSchedule([DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Sunday], [new TimeOnly(18, 45)], [new TimeOnly(18, 0)]),
        [
            new("Jackpot", "6 số", 6),
            new("Giải Nhất", "5 số", 5),
            new("Giải Nhì", "4 số", 4),
            new("Giải Ba", "3 số", 3),
        ]);

    /// <summary>
    /// Lotto 5/35: chọn 5 số 01–35 + 1 số đặc biệt 01–12; quay 13:00 và 21:00 mỗi ngày.
    /// Giải Khuyến khích: trùng ĐB và 0–2 số chính (theo bảng giải trên vietlott.vn).
    /// </summary>
    public static Game Lotto535 { get; } = new(
        "535", "Lotto 5/35", 5, 35, SpecialKind.SeparatePool, 12,
        new DrawSchedule(Enum.GetValues<DayOfWeek>(), [new TimeOnly(13, 45), new TimeOnly(21, 45)], [new TimeOnly(13, 0), new TimeOnly(21, 0)]),
        [
            new("Độc đắc", "5 số + ĐB", 5, true),
            new("Giải Nhất", "5 số", 5, false),
            new("Giải Nhì", "4 số + ĐB", 4, true),
            new("Giải Ba", "4 số", 4, false),
            new("Giải Tư", "3 số + ĐB", 3, true),
            new("Giải Năm", "3 số", 3, false),
            new("Khuyến khích", "0–2 số + ĐB", 0, 2, true),
        ]);

    /// <summary>Theo thứ tự hiển thị: 5/35, 6/45, 6/55.</summary>
    public static IReadOnlyList<Game> All { get; } = [Lotto535, Mega645, Power655];

    public static IReadOnlyList<string> Codes { get; } = All.Select(g => g.Code).ToArray();

    public static Game Get(string code) =>
        TryGet(code) ?? throw new ArgumentException(
            $"Game không hợp lệ: '{code}'. Chọn: {string.Join(", ", Codes)}.", nameof(code));

    /// <summary>Nhận "655", "6/55", "6x55", "power655"... ; null nếu không khớp.</summary>
    public static Game? TryGet(string? code)
    {
        if (string.IsNullOrWhiteSpace(code)) return null;
        var digits = new string(code.Where(char.IsAsciiDigit).ToArray());
        return All.FirstOrDefault(g => g.Code == digits);
    }
}
