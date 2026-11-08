using System.CommandLine;
using System.Text;
using Power655.Cli.Commands;

Console.OutputEncoding = Encoding.UTF8;

var dbOption = new Option<string?>("--db")
{
    Description = $"Đường dẫn file SQLite (mặc định: biến môi trường POWER655_DB hoặc data/power655.db)",
    Recursive = true,
};
var verboseOption = new Option<bool>("--verbose", "-v")
{
    Description = "Hiện log chi tiết",
    Recursive = true,
};
var gameOption = GlobalOptions.CreateGameOption();
var globals = new GlobalOptions(dbOption, verboseOption, gameOption);

var root = new RootCommand(
    "Power655 Analyzer — thu thập kết quả Vietlott Lotto 5/35, Mega 6/45, Power 6/55; thống kê và gợi ý bộ số.")
{
    dbOption,
    verboseOption,
    gameOption,
    FetchCommand.Create(globals),
    ImportCommand.Create(globals),
    StatsCommand.Create(globals),
    SuggestCommand.Create(globals),
    BacktestCommand.Create(globals),
    ExportCommand.Create(globals),
    ReportCommand.Create(globals),
};

return await root.Parse(args).InvokeAsync();
