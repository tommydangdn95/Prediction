using Power655.Core;
using Power655.Core.Strategies;
using static Power655.Tests.TestData;

namespace Power655.Tests;

public class StrategyTests
{
    public static TheoryData<string> Names => new() { "random", "hot", "cold", "balanced" };

    public static TheoryData<string, string> NamesByGame()
    {
        var data = new TheoryData<string, string>();
        foreach (var g in Games.Codes)
            foreach (var n in StrategyCatalog.Names)
                data.Add(g, n);
        return data;
    }

    [Theory]
    [MemberData(nameof(NamesByGame))]
    public void Picks_are_valid_tickets_for_every_game(string code, string name)
    {
        var game = Games.Get(code);
        var history = Synthetic(game, 40, seed: 11);
        var strategy = StrategyCatalog.Create(name);
        var rng = new Random(1);
        for (var i = 0; i < 200; i++)
        {
            var t = strategy.Pick(game, history, rng);
            t.Numbers.Should().HaveCount(game.PickCount).And.OnlyHaveUniqueItems().And.BeInAscendingOrder();
            t.Numbers.Should().OnlyContain(n => n >= 1 && n <= game.MaxNumber);
            if (game.TicketHasSpecial)
                t.Special.Should().NotBeNull().And.BeInRange(1, game.SpecialMax);
            else
                t.Special.Should().BeNull();
        }
    }

    [Theory]
    [MemberData(nameof(NamesByGame))]
    public void Works_with_empty_history_for_every_game(string code, string name)
    {
        var game = Games.Get(code);
        StrategyCatalog.Create(name).Pick(game, [], new Random(3)).Numbers.Should().HaveCount(game.PickCount);
    }

    [Theory]
    [MemberData(nameof(Names))]
    public void Same_seed_gives_same_picks(string name)
    {
        var s = StrategyCatalog.Create(name);
        s.Pick(P, Seed, new Random(42)).Should().BeEquivalentTo(s.Pick(P, Seed, new Random(42)));
    }

    [Fact]
    public void Hot_picks_come_from_top18_weighted_score()
    {
        var hot = new HotPick();
        var pool = hot.Pool(P, Seed);
        pool.Should().HaveCount(18).And.Contain(18);
        var rng = new Random(5);
        for (var i = 0; i < 100; i++)
            hot.Pick(P, Seed, rng).Numbers.Should().BeSubsetOf(pool);
    }

    [Fact]
    public void Cold_picks_come_from_top18_gaps()
    {
        var cold = new ColdPick();
        var pool = cold.Pool(P, Seed);
        pool.Should().HaveCount(18).And.Contain([15, 8, 32, 47]);
        var rng = new Random(5);
        for (var i = 0; i < 100; i++)
            cold.Pick(P, Seed, rng).Numbers.Should().BeSubsetOf(pool);
    }

    [Fact]
    public void Pool_size_scales_with_game()
    {
        new HotPick().Pool(Games.Mega645, Synthetic(Games.Mega645, 30, 1)).Should().HaveCount(18);
        new HotPick().Pool(Games.Lotto535, Synthetic(Games.Lotto535, 30, 1)).Should().HaveCount(15);
        new ColdPick().Pool(Games.Lotto535, Synthetic(Games.Lotto535, 30, 1)).Should().HaveCount(15);
    }

    [Fact]
    public void Lotto535_hot_and_cold_special_come_from_special_statistics()
    {
        var game = Games.Lotto535;
        // ĐB 7 ra liên tục ở các kỳ gần nhất; ĐB 1..4 chưa từng ra
        var history = Enumerable.Range(1, 20)
            .Select(i => G(game, i, i > 10 ? 7 : 5 + i % 7, 1, 2, 3, 4, 5)).ToList();

        var hotPool = new HotPick().SpecialPool(game, history);
        hotPool.Should().HaveCount(4).And.Contain(7);
        new ColdPick().SpecialPool(game, history).Should().Equal(1, 2, 3, 4);

        var rng = new Random(2);
        for (var i = 0; i < 50; i++)
        {
            new HotPick().Pick(game, history, rng).Special.Should().BeOneOf(hotPool);
            new ColdPick().Pick(game, history, rng).Special.Should().BeInRange(1, 4);
        }
    }

    [Fact]
    public void Balanced_picks_satisfy_constraints()
    {
        var (min, max) = BalancedPick.SumBounds(P, Seed);
        var rng = new Random(9);
        var s = new BalancedPick();
        for (var i = 0; i < 100; i++)
        {
            var pick = s.Pick(P, Seed, rng).Numbers;
            pick.Count(n => n % 2 == 0).Should().Be(3);
            pick.Count(n => n <= 27).Should().Be(3);
            pick.Sum().Should().BeInRange((int)Math.Ceiling(min), (int)Math.Floor(max));
        }
    }

    [Fact]
    public void Balanced_for_five_numbers_splits_two_three()
    {
        var game = Games.Lotto535;
        var history = Synthetic(game, 40, 4);
        var (min, max) = BalancedPick.SumBounds(game, history);
        var rng = new Random(9);
        for (var i = 0; i < 100; i++)
        {
            var pick = new BalancedPick().Pick(game, history, rng).Numbers;
            pick.Count(n => n % 2 == 0).Should().BeInRange(2, 3);
            pick.Count(n => n <= 17).Should().BeInRange(2, 3);
            pick.Sum().Should().BeInRange((int)Math.Ceiling(min), (int)Math.Floor(max));
        }
    }

    [Fact]
    public void Unknown_strategy_throws()
    {
        var act = () => StrategyCatalog.Create("lucky");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Strategies_reject_history_of_another_game()
    {
        var act = () => new HotPick().Pick(Games.Mega645, Seed, new Random(1));
        act.Should().Throw<ArgumentException>().WithMessage("*lẫn game*");
    }
}

public class BacktestTests
{
    [Fact]
    public void Walk_forward_tests_draws_after_training_window()
    {
        var r = Backtester.Run(P, Seed, new RandomPick(), train: 20, runs: 10, seed: 1);
        r.TestDraws.Should().Be(20);
        r.Runs.Should().Be(10);
        r.MatchDistribution.Values.Sum().Should().BeApproximately(1, 1e-9);
        r.AverageMatches.Should().BeInRange(0, 6);
        r.SpecialHitRate.Should().BeNull();
    }

    [Fact]
    public void Random_baseline_is_close_to_theory_over_many_runs()
    {
        var r = Backtester.Run(P, Seed, new RandomPick(), train: 20, runs: 1000, seed: 42);
        // 20 kỳ × 1000 lần: sai số chuẩn (chỉ do RNG) ≈ 0.005
        r.AverageMatches.Should().BeApproximately(P.Odds.ExpectedMatches, 0.03);
    }

    [Theory]
    [InlineData("535")]
    [InlineData("645")]
    public void Random_baseline_matches_theory_for_other_games(string code)
    {
        var game = Games.Get(code);
        var draws = Synthetic(game, 60, 8);
        var r = Backtester.Run(game, draws, new RandomPick(), train: 20, runs: 1000, seed: 3);
        r.AverageMatches.Should().BeApproximately(game.Odds.ExpectedMatches, 0.03);
        r.AnyPrizeRate.Should().BeApproximately(game.Odds.AnyPrizeProbability, 0.01);
        if (game.TicketHasSpecial)
            r.SpecialHitRate!.Value.Should().BeApproximately(1.0 / 12, 0.01);
    }

    [Fact]
    public void Strategy_never_sees_the_draw_it_is_tested_on()
    {
        var spy = new SpyStrategy();
        Backtester.Run(P, Seed, spy, train: 20, runs: 1);
        spy.HistorySizes.Should().Equal(Enumerable.Range(20, 20));
        spy.MaxIdSeenPerCall.Zip(spy.HistorySizes)
            .Should().OnlyContain(x => x.First < 1381 + x.Second); // id kỳ đang test = 1381 + t
    }

    [Fact]
    public void Is_reproducible_with_seed()
    {
        var a = Backtester.Run(P, Seed, new HotPick(), 20, 50, seed: 7);
        var b = Backtester.Run(P, Seed, new HotPick(), 20, 50, seed: 7);
        a.AverageMatches.Should().Be(b.AverageMatches);
    }

    [Fact]
    public void Rejects_draws_of_another_game()
    {
        var act = () => Backtester.Run(Games.Lotto535, Seed, new RandomPick());
        act.Should().Throw<ArgumentException>();
    }

    private sealed class SpyStrategy : IPickStrategy
    {
        public List<int> HistorySizes { get; } = [];
        public List<int> MaxIdSeenPerCall { get; } = [];
        public string Name => "spy";

        public Ticket Pick(Game game, IReadOnlyList<Draw> history, Random rng)
        {
            HistorySizes.Add(history.Count);
            MaxIdSeenPerCall.Add(history.Max(d => d.DrawId));
            return new Ticket([1, 2, 3, 4, 5, 6]);
        }
    }
}

public class OddsTests
{
    [Fact]
    public void Total_combinations_per_game()
    {
        Games.Power655.Odds.TotalCombinations.Should().Be(28_989_675);
        Games.Mega645.Odds.TotalCombinations.Should().Be(8_145_060);
        Games.Lotto535.Odds.TotalCombinations.Should().Be(324_632);
        Games.Lotto535.Odds.TicketCombinations.Should().Be(3_895_584);
    }

    [Fact]
    public void Power655_table_matches_spec_values()
    {
        P.Odds.Table.Select(o => (o.Prize, o.OneIn)).Should().Equal(
            ("Jackpot 1", 28_989_675L),
            ("Jackpot 2", 4_831_612L),
            ("Giải Nhất", 100_659L),
            ("Giải Nhì", 1_643L),
            ("Giải Ba", 79L));
    }

    [Fact]
    public void Mega645_table()
    {
        // C(45,6)=8.145.060; 5 số: 6·39 = 234; 4 số: 15·C(39,2) = 11.115; 3 số: 20·C(39,3) = 182.780
        Games.Mega645.Odds.Table.Select(o => (o.Prize, o.OneIn)).Should().Equal(
            ("Jackpot", 8_145_060L),
            ("Giải Nhất", 34_808L),
            ("Giải Nhì", 733L),
            ("Giải Ba", 45L));
    }

    [Fact]
    public void Lotto535_table()
    {
        // C(35,5)=324.632, ĐB 1/12 độc lập. 5 số: 1; 4 số: 5·30 = 150; 3 số: 10·C(30,2) = 4.350
        Games.Lotto535.Odds.Table.Select(o => (o.Prize, o.OneIn)).Should().Equal(
            ("Độc đắc", 3_895_584L),       // 324.632 · 12
            ("Giải Nhất", 354_144L),       // 324.632 · 12/11
            ("Giải Nhì", 25_971L),         // 324.632 · 12/150
            ("Giải Ba", 2_361L),           // 324.632 · 12/(150·11)
            ("Giải Tư", 896L),             // 324.632 · 12/4.350
            ("Giải Năm", 81L),             // 324.632 · 12/(4.350·11)
            ("Khuyến khích", 12L));        // 12 · 324.632/320.131
    }

    [Theory]
    [InlineData("535")]
    [InlineData("645")]
    [InlineData("655")]
    public void Match_distribution_is_hypergeometric(string code)
    {
        var odds = Games.Get(code).Odds;
        var k = Games.Get(code).PickCount;
        Enumerable.Range(0, k + 1).Sum(odds.ProbabilityExactMatches).Should().BeApproximately(1, 1e-12);
        Enumerable.Range(0, k + 1).Sum(i => i * odds.ProbabilityExactMatches(i))
            .Should().BeApproximately(odds.ExpectedMatches, 1e-12);
    }

    [Fact]
    public void Classify_applies_game_rules()
    {
        var p = D(1, 7, 1, 2, 3, 4, 5, 6);
        P.Odds.Classify(new Ticket([1, 2, 3, 4, 5, 7]), p)!.Name.Should().Be("Jackpot 2");
        P.Odds.Classify(new Ticket([1, 2, 3, 4, 5, 9]), p)!.Name.Should().Be("Giải Nhất");
        P.Odds.Classify(new Ticket([1, 2, 10, 11, 12, 13]), p).Should().BeNull();

        var l = G(Games.Lotto535, 1, 11, 4, 11, 17, 26, 35);
        var lotto = Games.Lotto535.Odds;
        lotto.Classify(new Ticket([4, 11, 17, 26, 35], 11), l)!.Name.Should().Be("Độc đắc");
        lotto.Classify(new Ticket([4, 11, 17, 26, 35], 1), l)!.Name.Should().Be("Giải Nhất");
        lotto.Classify(new Ticket([4, 11, 17, 1, 2], 11), l)!.Name.Should().Be("Giải Tư");
        lotto.Classify(new Ticket([1, 2, 3, 5, 6], 11), l)!.Name.Should().Be("Khuyến khích");
        lotto.Classify(new Ticket([4, 11, 1, 2, 3], 1), l).Should().BeNull();
    }

    [Fact]
    public void Disclaimer_has_required_text_and_no_banned_words()
    {
        Odds.Disclaimer.Should().Be("Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng.");
        foreach (var banned in new[] { "chắc chắn", "dễ trúng", "soi cầu chuẩn" })
            Odds.Disclaimer.Should().NotContainEquivalentOf(banned);
    }
}

public class DrawScheduleTests
{
    private static DateTimeOffset Vn(int y, int m, int d, int h, int min) => new(y, m, d, h, min, 0, TimeSpan.FromHours(7));

    [Fact]
    public void Draw_days_per_game()
    {
        var p = Games.Power655.Schedule;
        p.IsDrawDay(new DateOnly(2026, 11, 5)).Should().BeTrue();  // thứ 5
        p.IsDrawDay(new DateOnly(2026, 11, 7)).Should().BeTrue();  // thứ 7
        p.IsDrawDay(new DateOnly(2026, 11, 6)).Should().BeFalse(); // thứ 6

        var m = Games.Mega645.Schedule;
        m.IsDrawDay(new DateOnly(2026, 11, 6)).Should().BeTrue();  // thứ 6
        m.IsDrawDay(new DateOnly(2026, 11, 8)).Should().BeTrue();  // CN
        m.IsDrawDay(new DateOnly(2026, 11, 7)).Should().BeFalse(); // thứ 7

        Enumerable.Range(1, 7).Should().OnlyContain(d => Games.Lotto535.Schedule.IsDrawDay(new DateOnly(2026, 11, d)));
    }

    [Fact]
    public void Results_of_today_only_after_18_45()
    {
        var s = Games.Power655.Schedule;
        s.LatestAvailableDrawDate(Vn(2026, 11, 7, 18, 44)).Should().Be(new DateOnly(2026, 11, 5));
        s.LatestAvailableDrawDate(Vn(2026, 11, 7, 18, 45)).Should().Be(new DateOnly(2026, 11, 7));
        s.LatestAvailableDrawDate(Vn(2026, 11, 6, 10, 0)).Should().Be(new DateOnly(2026, 11, 5));
    }

    [Fact]
    public void Next_fetch_time_is_next_draw_day_at_18_45()
    {
        var s = Games.Power655.Schedule;
        s.NextFetchTime(Vn(2026, 11, 7, 10, 0)).Should().Be(Vn(2026, 11, 7, 18, 45));
        s.NextFetchTime(Vn(2026, 11, 7, 19, 0)).Should().Be(Vn(2026, 11, 10, 18, 45));
        s.NextFetchTime(Vn(2026, 11, 6, 19, 0)).Should().Be(Vn(2026, 11, 7, 18, 45));
    }

    [Fact]
    public void Lotto535_has_two_draws_a_day()
    {
        var s = Games.Lotto535.Schedule;
        s.LatestAvailableDrawDate(Vn(2026, 11, 8, 13, 0)).Should().Be(new DateOnly(2026, 11, 7));
        s.LatestAvailableDrawDate(Vn(2026, 11, 8, 14, 0)).Should().Be(new DateOnly(2026, 11, 8));
        s.NextFetchTime(Vn(2026, 11, 8, 10, 0)).Should().Be(Vn(2026, 11, 8, 13, 45));
        s.NextFetchTime(Vn(2026, 11, 8, 14, 0)).Should().Be(Vn(2026, 11, 8, 21, 45));
        s.NextFetchTime(Vn(2026, 11, 8, 22, 0)).Should().Be(Vn(2026, 11, 9, 13, 45));
        s.PendingToday(Vn(2026, 11, 8, 22, 0)).Should().BeNull();
    }
}
