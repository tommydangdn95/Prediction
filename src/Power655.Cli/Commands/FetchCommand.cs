using System.CommandLine;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Power655.Core;
using Power655.Scraper;
using Spectre.Console;

namespace Power655.Cli.Commands;

internal static class FetchCommand
{
    public static Command Create(GlobalOptions globals)
    {
        var pages = new Option<int>("--pages") { Description = "Số trang mỗi game (8 kỳ/trang)", DefaultValueFactory = _ => 5 };
        pages.Min(1);
        var source = new Option<string?>("--source") { Description = "Chỉ dùng một nguồn (mặc định: vietlott, lỗi thì minhngoc)" };
        source.AcceptOnlyFromAmong(DrawSources.Vietlott, DrawSources.MinhNgoc);

        var command = new Command("fetch", "Lấy kết quả mới nhất từ web và lưu vào DB (upsert theo game + kỳ).") { pages, source };
        command.SetAction(async (result, ct) =>
        {
            await using var sp = globals.BuildServices(result);
            var fetcher = sp.GetRequiredService<DrawFetchService>();
            var failed = 0;
            var games = globals.SelectedGames(result);

            foreach (var game in games)
            {
                Ui.GameHeader(game);
                WriteScheduleNote(game);

                FetchReport report = null!;
                await AnsiConsole.Status().StartAsync($"Đang tải {game.Name}...", async _ =>
                    report = await fetcher.FetchAsync(game, result.GetValue(pages), result.GetValue(source), ct));

                foreach (var w in report.Warnings) Ui.Warn(w);
                foreach (var c in report.Conflicts)
                    Ui.Error($"Mâu thuẫn kỳ {c.Kept.DrawId}: {c.Kept.Source} = {c.Kept} | {c.Rejected.Source} = {c.Rejected}. Giữ {c.Kept.Source}.");

                if (report.Draws.Count == 0)
                {
                    Ui.Error($"Không lấy được kỳ {game.Name} nào từ các nguồn.");
                    failed++;
                    continue;
                }

                Ui.WriteDraws(game, report.Draws.Take(10),
                    $"Đã lấy {report.Draws.Count} kỳ từ {string.Join(" + ", report.SourcesUsed)}"
                    + (report.Draws.Count > 10 ? " (hiện 10 kỳ mới nhất)" : ""));

                var (scope, repo) = await AppServices.OpenRepositoryAsync(sp, ct);
                await using (scope)
                {
                    var upsert = await repo.UpsertAsync(report.Draws, ct);
                    AnsiConsole.MarkupLine(
                        $"DB: [green]+{upsert.Inserted} mới[/], [blue]{upsert.Updated} cập nhật[/], {upsert.Unchanged} không đổi. " +
                        $"Tổng {await repo.CountAsync(game, ct)} kỳ {Markup.Escape(game.Name)}.");
                    foreach (var c in upsert.Conflicts)
                        Ui.Warn($"Kỳ {c.Existing.DrawId} trong DB ({c.Existing.Source}) khác dữ liệu mới ({c.Incoming.Source}): "
                            + (c.Overwritten ? "đã ghi đè bằng nguồn ưu tiên cao hơn." : "giữ bản trong DB."));
                }

                var expected = game.Schedule.LatestAvailableDrawDate(DateTimeOffset.Now);
                var latest = report.Draws.Max(d => d.Date);
                if (latest < expected)
                    Ui.Warn($"Kỳ mới nhất lấy được là {latest:dd/MM/yyyy}, nhưng kết quả ngày {expected:dd/MM/yyyy} lẽ ra đã có — nguồn có thể đang cache.");
                AnsiConsole.WriteLine();
            }
            return failed == 0 ? 0 : failed == games.Count ? 1 : 2;
        });
        return command;
    }

    private static void WriteScheduleNote(Game game)
    {
        var now = DateTimeOffset.Now;
        if (game.Schedule.PendingToday(now) is { } next)
        {
            var local = DrawSchedule.ToVietnamTime(now);
            AnsiConsole.MarkupLine(
                $"[grey]Hôm nay còn kỳ {Markup.Escape(game.Name)} chưa có kết quả (có sau {next.ToString("HH:mm", CultureInfo.InvariantCulture)} giờ VN). " +
                $"Bây giờ là {local.ToString("HH:mm", CultureInfo.InvariantCulture)}.[/]");
        }
    }
}
