using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Power655.Data;
using Power655.Scraper;

namespace Power655.Cli;

/// <summary>Dựng DI container cho một lần chạy lệnh.</summary>
internal static class AppServices
{
    public const string DefaultDbPath = "data/power655.db";
    public const string DbEnvVar = "POWER655_DB";

    public static string ResolveDbPath(string? option) =>
        Path.GetFullPath(option
            ?? Environment.GetEnvironmentVariable(DbEnvVar)
            ?? DefaultDbPath);

    public static ServiceProvider Build(string dbPath, bool verbose)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

        var services = new ServiceCollection();
        services.AddLogging(b => b
            .AddSimpleConsole(o => { o.SingleLine = true; o.TimestampFormat = "HH:mm:ss "; })
            .SetMinimumLevel(verbose ? LogLevel.Information : LogLevel.Warning)
            .AddFilter("System.Net.Http", LogLevel.Warning)
            .AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning)
            .AddFilter("Polly", LogLevel.Warning));
        services.AddScoped(_ => new Power655DbContext(Power655DbContext.CreateOptions(dbPath)));
        services.AddScoped<DrawRepository>();
        services.AddPower655Scrapers();
        return services.BuildServiceProvider();
    }

    /// <summary>Mở repository đã migrate xong.</summary>
    public static async Task<(AsyncServiceScope Scope, DrawRepository Repo)> OpenRepositoryAsync(
        ServiceProvider sp, CancellationToken ct)
    {
        var scope = sp.CreateAsyncScope();
        var repo = scope.ServiceProvider.GetRequiredService<DrawRepository>();
        await repo.MigrateAsync(ct);
        return (scope, repo);
    }
}
