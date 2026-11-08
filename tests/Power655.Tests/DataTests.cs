using Power655.Core;
using Power655.Data;
using static Power655.Tests.TestData;

namespace Power655.Tests;

public class CsvTests
{
    [Fact]
    public void Round_trip_preserves_draws()
    {
        var draws = new[]
        {
            new Draw(P, 1420, new DateOnly(2026, 11, 5), [6, 20, 34, 41, 49, 52], 36, 42_432_448_800, 5_618_647_400, "minhngoc"),
            new Draw(P, 1419, new DateOnly(2026, 11, 3), [10, 22, 31, 39, 43, 54], 14),
        };
        var writer = new StringWriter();
        CsvDrawFile.Write(writer, draws);

        var result = CsvDrawFile.Read(new StringReader(writer.ToString()), null);
        result.Errors.Should().BeEmpty();
        result.Draws.Should().Equal(draws);
    }

    [Fact]
    public void Invalid_rows_are_reported_not_imported()
    {
        const string csv = """
            draw_id,date,n1,n2,n3,n4,n5,n6,special
            1,2026-01-01,1,2,3,4,5,6,7
            2,2026-01-03,1,1,3,4,5,6,7
            3,not-a-date,1,2,3,4,5,6,7
            """;
        var result = CsvDrawFile.Read(new StringReader(csv), P);
        result.Draws.Should().ContainSingle(d => d.DrawId == 1);
        result.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void Missing_columns_are_reported()
    {
        CsvDrawFile.Read(new StringReader("draw_id,date\n1,2026-01-01"), P).Errors.Should().ContainSingle();
    }
}

public sealed class RepositoryTests : IDisposable
{
    private readonly string _dbPath = Path.Combine(Path.GetTempPath(), $"power655-test-{Guid.NewGuid():N}.db");

    private async Task<(Power655DbContext Db, DrawRepository Repo)> OpenAsync()
    {
        var db = new Power655DbContext(Power655DbContext.CreateOptions(_dbPath));
        var repo = new DrawRepository(db);
        await repo.MigrateAsync();
        return (db, repo);
    }

    [Fact]
    public async Task Importing_seed_twice_keeps_40_rows()
    {
        var (db, repo) = await OpenAsync();
        await using (db)
        {
            (await repo.UpsertAsync(Seed)).Inserted.Should().Be(40);
            var second = await repo.UpsertAsync(Seed);
            second.Inserted.Should().Be(0);
            second.Unchanged.Should().Be(40);
            (await repo.CountAsync()).Should().Be(40);
        }
    }

    [Fact]
    public async Task GetAsync_returns_ascending_and_supports_last()
    {
        var (db, repo) = await OpenAsync();
        await using (db)
        {
            await repo.UpsertAsync(Seed);
            var all = await repo.GetAsync(P);
            all.Should().BeInAscendingOrder(d => d.DrawId).And.HaveCount(40);
            all.Should().BeEquivalentTo(Seed);
            (await repo.GetAsync(P, 5)).Select(d => d.DrawId).Should().Equal(1416, 1417, 1418, 1419, 1420);
        }
    }

    [Fact]
    public async Task Fetched_source_overwrites_seed_on_conflict_but_not_vice_versa()
    {
        var (db, repo) = await OpenAsync();
        await using (db)
        {
            var seed = D(100, 7, 1, 2, 3, 4, 5, 6);
            var real = new Draw(P, 100, seed.Date, [1, 2, 3, 4, 5, 9], 7, source: DrawSources.Vietlott);
            await repo.UpsertAsync([seed]);

            var r1 = await repo.UpsertAsync([real]);
            r1.Updated.Should().Be(1);
            r1.Conflicts.Should().ContainSingle(c => c.Overwritten);

            var r2 = await repo.UpsertAsync([seed]);
            r2.Unchanged.Should().Be(1);
            r2.Conflicts.Should().ContainSingle(c => !c.Overwritten);

            (await repo.GetAsync(P)).Single().Should().Be(real);
        }
    }

    [Fact]
    public async Task Same_result_fills_missing_jackpot()
    {
        var (db, repo) = await OpenAsync();
        await using (db)
        {
            var seed = D(100, 7, 1, 2, 3, 4, 5, 6);
            await repo.UpsertAsync([seed]);
            var withJackpot = new Draw(P, 100, seed.Date, seed.Numbers, 7, 123, 45, DrawSources.MinhNgoc);

            (await repo.UpsertAsync([withJackpot])).Updated.Should().Be(1);
            (await repo.GetAsync(P)).Single().Jackpot1.Should().Be(123);
            (await repo.UpsertAsync([withJackpot])).Unchanged.Should().Be(1);
        }
    }

    public void Dispose()
    {
        Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
        if (File.Exists(_dbPath)) File.Delete(_dbPath);
    }
}
