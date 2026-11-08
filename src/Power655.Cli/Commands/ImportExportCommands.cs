using System.CommandLine;
using System.Text;
using Power655.Data;
using Spectre.Console;

namespace Power655.Cli.Commands;

internal static class ImportCommand
{
    public static Command Create(GlobalOptions globals)
    {
        var csv = new Option<FileInfo>("--csv") { Description = "File CSV ([game,]draw_id,date,n1..n5[,n6][,special])", Required = true };
        csv.AcceptExistingOnly();

        var command = new Command("import",
            "Nạp kết quả từ file CSV vào DB. Game lấy từ cột 'game'; nếu CSV không có cột này thì cần --game.") { csv };
        command.SetAction(async (result, ct) =>
        {
            var file = result.GetValue(csv)!;
            CsvReadResult parsed;
            using (var reader = new StreamReader(file.FullName, Encoding.UTF8))
                parsed = CsvDrawFile.Read(reader, globals.SingleGame(result));

            foreach (var e in parsed.Errors) Ui.Warn(e);
            if (parsed.Draws.Count == 0)
            {
                Ui.Error("Không có dòng hợp lệ nào.");
                return 1;
            }

            await using var sp = globals.BuildServices(result);
            var (scope, repo) = await AppServices.OpenRepositoryAsync(sp, ct);
            await using (scope)
            {
                var upsert = await repo.UpsertAsync(parsed.Draws, ct);
                AnsiConsole.MarkupLine(
                    $"Đọc {parsed.Draws.Count} kỳ từ [blue]{Markup.Escape(file.Name)}[/]: " +
                    $"[green]+{upsert.Inserted} mới[/], {upsert.Updated} cập nhật, {upsert.Unchanged} không đổi. " +
                    $"DB có {await repo.CountAsync(null, ct)} kỳ.");
                foreach (var c in upsert.Conflicts)
                    Ui.Warn($"Kỳ {c.Existing.DrawId}: CSV khác DB ({c.Existing.Source}) — "
                        + (c.Overwritten ? "đã ghi đè." : "giữ bản trong DB."));
            }
            return parsed.Errors.Count == 0 ? 0 : 2;
        });
        return command;
    }
}

internal static class ExportCommand
{
    public static Command Create(GlobalOptions globals)
    {
        var csv = new Option<FileInfo>("--csv") { Description = "File CSV đầu ra", Required = true };

        var command = new Command("export", "Xuất lịch sử trong DB (các game được chọn) ra CSV.") { csv };
        command.SetAction(async (result, ct) =>
        {
            var draws = (await globals.LoadDrawsAsync(result, 0, ct)).SelectMany(x => x.Draws).ToList();
            var file = result.GetValue(csv)!;
            if (file.Directory is { Exists: false } dir) dir.Create();
            await using (var writer = new StreamWriter(file.FullName, false, new UTF8Encoding(false)))
                CsvDrawFile.Write(writer, draws);
            AnsiConsole.MarkupLine($"Đã xuất {draws.Count} kỳ ra [blue]{Markup.Escape(file.FullName)}[/].");
            return 0;
        });
        return command;
    }
}
