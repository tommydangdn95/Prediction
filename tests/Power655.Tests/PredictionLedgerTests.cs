using Power655.Core;

namespace Power655.Tests;

public sealed class PredictionLedgerTests : IDisposable
{
    private readonly string _directory = Directory.CreateTempSubdirectory("prediction-ledger-tests-").FullName;
    private static DateTimeOffset At(int day, int hour, int minute = 0) => new(2026, 11, day, hour, minute, 0, TimeSpan.FromHours(7));
    private static Draw Lotto(int id, int day) => new(Games.Lotto535, id, new DateOnly(2026, 11, day), [1, 2, 3, 4, 5], 1);
    private static readonly Draw[] LottoHistory = [Lotto(995, 8), Lotto(996, 8)];
    private static readonly SuggestedTicket[] LottoTickets = [new("random", new Ticket([6, 7, 8, 9, 10], 1))];
    private static LedgerPrediction Morning() => PredictionLedger.Create(Games.Lotto535, LottoHistory, At(9, 7), 42, "sets=1", "test-v1", LottoTickets);
    private sealed class FrozenClock(DateTimeOffset value) : TimeProvider { public override DateTimeOffset GetUtcNow() => value; }

    [Fact]
    public void Rerun_retains_first_forecast_bytes_seed_and_ticket()
    {
        var original = Morning();
        PredictionLedger.SaveOrLoad(_directory, original, new FrozenClock(At(9, 7)));
        var path = Directory.GetFiles(Path.Combine(_directory, "ledger")).Single();
        var bytes = File.ReadAllBytes(path);
        var different = original with { Seed = 999, GeneratedAt = At(9, 8), Tickets = [new("hot", new Ticket([20, 21, 22, 23, 24], 2))] };
        var retained = PredictionLedger.SaveOrLoad(_directory, different, new FrozenClock(At(9, 8)));
        retained.Seed.Should().Be(42);
        retained.Tickets[0].Ticket.Numbers.Should().Equal(6, 7, 8, 9, 10);
        File.ReadAllBytes(path).Should().Equal(bytes);
        PredictionLedger.Load(_directory).Should().ContainSingle();
    }

    [Fact]
    public void Saves_both_lotto_slots_without_overwriting_the_morning_forecast()
    {
        var morning = Morning();
        var afternoon = PredictionLedger.Create(Games.Lotto535, [.. LottoHistory, Lotto(997, 9)], At(9, 14), 42, "sets=1", "v1", LottoTickets);
        PredictionLedger.SaveOrLoad(_directory, morning, new FrozenClock(At(9, 7)));
        PredictionLedger.SaveOrLoad(_directory, afternoon, new FrozenClock(At(9, 14)));
        PredictionLedger.Load(_directory).Select(p => p.TargetDrawId).Should().Equal(997, 998);
        afternoon.TargetDrawAt.Should().Be(At(9, 21));
        var reviews = PredictionLedger.Review(PredictionLedger.Load(_directory), [.. LottoHistory, Lotto(997, 9), Lotto(998, 9)]);
        reviews.Select(r => r.Draw.DrawId).Should().Equal(997, 998);
    }

    [Theory]
    [InlineData(13, 0)]
    [InlineData(13, 10)]
    [InlineData(22, 0)]
    public void Stale_history_is_rejected_from_draw_start_not_fetch_time(int hour, int minute)
    {
        PredictionLedger.AssessTarget(Games.Lotto535, LottoHistory, At(9, hour, minute)).IsReady.Should().BeFalse();
    }

    [Fact]
    public void Missing_evening_result_is_detected_even_when_today_has_a_noon_result()
    {
        PredictionLedger.AssessTarget(Games.Lotto535, [.. LottoHistory, Lotto(997, 9)], At(9, 21)).IsReady.Should().BeFalse();
    }

    [Fact]
    public void Future_or_ambiguous_input_is_rejected()
    {
        PredictionLedger.AssessTarget(Games.Lotto535, [Lotto(997, 9)], At(9, 14)).IsReady.Should().BeFalse();
        PredictionLedger.AssessTarget(Games.Lotto535, [.. LottoHistory, Lotto(997, 9)], At(9, 12)).IsReady.Should().BeFalse();
    }

    [Fact]
    public void Cannot_save_a_new_forecast_after_cutoff_even_if_generated_earlier()
    {
        var act = () => PredictionLedger.SaveOrLoad(_directory, Morning(), new FrozenClock(At(9, 13)));
        act.Should().Throw<InvalidOperationException>();
        PredictionLedger.Load(_directory).Should().BeEmpty();
    }

    [Fact]
    public void Reviews_reject_late_wrong_slot_and_changed_input_and_deduplicate_real_draws()
    {
        var valid = Morning();
        Draw[] actual = [.. LottoHistory, Lotto(997, 9)];
        PredictionLedger.Review([valid, valid with { GeneratedAt = At(9, 8) }], actual).Should().ContainSingle();
        PredictionLedger.Review([valid with { GeneratedAt = At(9, 13) }], actual).Should().BeEmpty();
        PredictionLedger.Review([valid with { TargetDrawAt = At(9, 21) }], actual).Should().BeEmpty();
        Draw[] changedInput = [LottoHistory[0], new(Games.Lotto535, 996, new DateOnly(2026, 11, 8), [2, 3, 4, 5, 6], 1), Lotto(997, 9)];
        PredictionLedger.Review([valid], changedInput).Should().BeEmpty();
    }

    [Fact]
    public void Source_upgrade_or_late_jackpot_does_not_drop_the_forecast_from_review()
    {
        // Ngày đầu vietlott lỗi → kết quả lấy từ minhngoc; hôm sau vietlott ghi đè cùng kết quả kèm Jackpot.
        Draw[] fetchedFromFallback = [
            new(Games.Mega645, 1585, new DateOnly(2026, 11, 6), [1, 2, 3, 4, 5, 6], null, source: DrawSources.MinhNgoc),
            new(Games.Mega645, 1586, new DateOnly(2026, 11, 8), [7, 8, 9, 10, 11, 12], null, source: DrawSources.MinhNgoc)];
        SuggestedTicket[] tickets = [new("random", new Ticket([7, 8, 9, 10, 11, 12]))];
        var entry = PredictionLedger.Create(Games.Mega645, fetchedFromFallback, At(9, 7), 42, "test", "v1", tickets);

        Draw[] upgraded = [.. fetchedFromFallback.Select(d => d.With(jackpot1: 15_740_836_000, source: DrawSources.Vietlott)),
            new(Games.Mega645, 1587, new DateOnly(2026, 11, 11), [7, 8, 9, 10, 11, 12], null, source: DrawSources.Vietlott)];

        PredictionLedger.HashInputs(upgraded.Take(2)).Should().Be(entry.InputHash);
        PredictionLedger.Review([entry], upgraded).Should().ContainSingle();
    }

    [Fact]
    public void Legacy_markdown_is_not_counted_as_a_timestamped_forecast()
    {
        File.WriteAllText(Path.Combine(_directory, "2026-11-08.md"), "## Lotto 5/35");
        PredictionLedger.Load(_directory).Should().BeEmpty();
    }

    [Fact]
    public void Corrupt_canonical_file_is_not_overwritten()
    {
        Directory.CreateDirectory(Path.Combine(_directory, "ledger"));
        var file = Path.Combine(_directory, "ledger", "535-000997.json");
        File.WriteAllText(file, "invalid json");
        var act = () => PredictionLedger.SaveOrLoad(_directory, Morning(), new FrozenClock(At(9, 7)));
        act.Should().Throw<InvalidDataException>();
        File.ReadAllText(file).Should().Be("invalid json");
    }

    [Fact]
    public void Mega_non_draw_days_share_one_target_forecast()
    {
        Draw[] history = [new(Games.Mega645, 1586, new DateOnly(2026, 11, 8), [1, 2, 3, 4, 5, 6], null)];
        SuggestedTicket[] tickets = [new("random", new Ticket([7, 8, 9, 10, 11, 12]))];
        var first = PredictionLedger.Create(Games.Mega645, history, At(9, 7), 42, "test", "v1", tickets);
        var next = PredictionLedger.Create(Games.Mega645, history, At(10, 7), 43, "test", "v1", tickets);
        PredictionLedger.SaveOrLoad(_directory, first, new FrozenClock(At(9, 7)));
        PredictionLedger.SaveOrLoad(_directory, next, new FrozenClock(At(10, 7))).Seed.Should().Be(42);
        var actual = new Draw(Games.Mega645, 1587, new DateOnly(2026, 11, 11), [7, 8, 9, 10, 11, 12], null);
        PredictionLedger.Review(PredictionLedger.Load(_directory), [.. history, actual]).Should().ContainSingle();
    }

    public void Dispose() => Directory.Delete(_directory, recursive: true);
}
