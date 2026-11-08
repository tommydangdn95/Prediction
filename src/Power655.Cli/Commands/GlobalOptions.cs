using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Power655.Core;
using Power655.Data;

namespace Power655.Cli.Commands;

internal sealed record GlobalOptions(Option<string?> Db, Option<bool> Verbose, Option<string> Game)
{
    public const string AllGames = "all";

    public static Option<string> CreateGameOption()
    {
        var option = new Option<string>("--game", "-g")
        {
            Description = $"Game: {string.Join("|", Games.Codes)} hoặc {AllGames} (mặc định: {AllGames})",
            DefaultValueFactory = _ => AllGames,
            Recursive = true,
        };
        option.Validators.Add(r =>
        {
            var value = r.GetValueOrDefault<string>();
            if (value != AllGames && Games.TryGet(value) is null)
                r.AddError($"--game không hợp lệ '{value}'. Chọn: {string.Join(", ", Games.Codes)}, {AllGames}.");
        });
        return option;
    }

    /// <summary>Các game được chọn (theo thứ tự 5/35, 6/45, 6/55).</summary>
    public IReadOnlyList<Game> SelectedGames(ParseResult result) =>
        SingleGame(result) is { } g ? [g] : Games.All;

    /// <summary>Game cụ thể, hoặc null nếu chọn "all".</summary>
    public Game? SingleGame(ParseResult result) => Games.TryGet(result.GetValue(Game));

    public ServiceProvider BuildServices(ParseResult result) =>
        AppServices.Build(AppServices.ResolveDbPath(result.GetValue(Db)), result.GetValue(Verbose));

    /// <summary>Đọc lịch sử từng game từ DB (tăng dần theo DrawId); <paramref name="last"/> = 0 lấy tất cả.</summary>
    public async Task<IReadOnlyList<(Game Game, IReadOnlyList<Draw> Draws)>> LoadDrawsAsync(
        ParseResult result, int last, CancellationToken ct)
    {
        await using var sp = BuildServices(result);
        var (scope, repo) = await AppServices.OpenRepositoryAsync(sp, ct);
        await using (scope)
        {
            var list = new List<(Game, IReadOnlyList<Draw>)>();
            foreach (var game in SelectedGames(result))
                list.Add((game, await repo.GetAsync(game, last, ct)));
            return list;
        }
    }
}

internal static class OptionValidation
{
    public static void Min(this Option<int> option, int min) =>
        option.Validators.Add(r =>
        {
            if (r.GetValueOrDefault<int>() < min)
                r.AddError($"{option.Name} phải ≥ {min}.");
        });
}
