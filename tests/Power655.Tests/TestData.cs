using Power655.Core;
using Power655.Data;

namespace Power655.Tests;

internal static class TestData
{
    public static string Fixture(string name) => Path.Combine(AppContext.BaseDirectory, "Fixtures", name);

    private static readonly Lazy<IReadOnlyList<Draw>> SeedDraws = new(() =>
    {
        using var reader = new StreamReader(Fixture("draws_seed.csv"));
        var result = CsvDrawFile.Read(reader, Games.Power655);
        result.Errors.Should().BeEmpty();
        return result.Draws;
    });

    /// <summary>40 kỳ seed (Phụ lục A), thứ tự như trong file (mới nhất trước).</summary>
    public static IReadOnlyList<Draw> Seed => SeedDraws.Value;

    public static Game P => Games.Power655;

    /// <summary>Kỳ Power 6/55 giả lập.</summary>
    public static Draw D(int id, int special, params int[] numbers) =>
        new(P, id, new DateOnly(2026, 1, 1).AddDays(id), numbers, special);

    /// <summary>Lịch sử ngẫu nhiên đều hợp lệ cho <paramref name="game"/>, id 1..count.</summary>
    public static IReadOnlyList<Draw> Synthetic(Game game, int count, int seed)
    {
        var rng = new Random(seed);
        return Enumerable.Range(1, count).Select(id =>
        {
            var numbers = game.AllNumbers.OrderBy(_ => rng.Next()).Take(game.PickCount).ToArray();
            int? special = game.Special switch
            {
                SpecialKind.BonusFromMainPool => game.AllNumbers.Except(numbers).OrderBy(_ => rng.Next()).First(),
                SpecialKind.SeparatePool => rng.Next(1, game.SpecialMax + 1),
                _ => null,
            };
            return G(game, id, special, numbers);
        }).ToList();
    }

    /// <summary>Kỳ giả lập cho game bất kỳ (special = null với Mega 6/45).</summary>
    public static Draw G(Game game, int id, int? special, params int[] numbers) =>
        new(game, id, new DateOnly(2026, 1, 1).AddDays(id), numbers, special);
}
