using Power655.Core;
using static Power655.Tests.TestData;

namespace Power655.Tests;

/// <summary>Số kiểm tra theo spec mục 5 với seed 40 kỳ.</summary>
public class StatisticsTests
{
    [Fact]
    public void Seed_has_40_draws() => Seed.Should().HaveCount(40);

    [Fact]
    public void Frequency_matches_spec()
    {
        var f = DrawStatistics.Frequency(P, Seed);
        f.Should().HaveCount(55);
        f[18].Should().Be(11);
        f[7].Should().Be(9);
        f.Values.Sum().Should().Be(240);
    }

    [Fact]
    public void Gap_matches_spec()
    {
        var g = DrawStatistics.Gap(P, Seed);
        g[15].Should().Be(29);
        g[6].Should().Be(0);   // kỳ 1420 có số 06
        g[20].Should().Be(0);
        g[10].Should().Be(1);  // kỳ 1419
    }

    [Fact]
    public void Gap_is_independent_of_input_order()
    {
        DrawStatistics.Gap(P, Seed.Reverse()).Should().Equal(DrawStatistics.Gap(P, Seed));
    }

    [Fact]
    public void Gap_of_never_drawn_number_is_draw_count()
    {
        var draws = new[] { D(1, 7, 1, 2, 3, 4, 5, 6), D(2, 8, 1, 2, 3, 4, 5, 9) };
        DrawStatistics.Gap(P, draws)[55].Should().Be(2);
        DrawStatistics.Gap(P, draws)[6].Should().Be(1);
    }

    [Fact]
    public void ChiSquare_matches_spec()
    {
        var chi = DrawStatistics.ChiSquareUniform(P, Seed);
        chi.ChiSquare.Should().BeApproximately(48.3, 0.05);
        chi.DegreesOfFreedom.Should().Be(54);
        chi.PValue.Should().BeApproximately(0.69, 0.01);
    }

    [Fact]
    public void ChiSquare_requires_data()
    {
        var act = () => DrawStatistics.ChiSquareUniform(P, []);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void SpecialFrequency_counts_one_per_draw()
    {
        var s = DrawStatistics.SpecialFrequency(P, Seed);
        s.Values.Sum().Should().Be(40);
        s[11].Should().Be(3);
        s[50].Should().Be(3);
    }

    [Fact]
    public void HotCold_orders_by_frequency()
    {
        var hc = DrawStatistics.HotCold(P, Seed, 3);
        hc.Hot.Select(x => x.Number).Should().Equal(18, 7, 11);
        hc.Cold.Should().OnlyContain(x => x.Count == 2);
        hc.Cold.Select(x => x.Number).Should().Equal(15, 30, 32);
    }

    [Fact]
    public void PairFrequency_top_pair_is_7_18()
    {
        var pairs = DrawStatistics.PairFrequency(P, Seed, 3);
        pairs[0].Should().Be(new PairCount(7, 18, 5));
        pairs.Should().BeInDescendingOrder(p => p.Count);
    }

    [Fact]
    public void WeightedScore_is_a_probability_distribution()
    {
        var w = DrawStatistics.WeightedScore(P, Seed);
        w.Should().HaveCount(55);
        w.Values.Sum().Should().BeApproximately(1.0, 1e-9);
        w.Values.Should().OnlyContain(p => p > 0);
        w.OrderByDescending(kv => kv.Value).First().Key.Should().Be(18);
    }

    [Fact]
    public void WeightedScore_without_decay_or_smoothing_equals_frequency_share()
    {
        var w = DrawStatistics.WeightedScore(P, Seed, decay: 1.0, alpha: 0);
        w[18].Should().BeApproximately(11 / 240.0, 1e-12);
    }

    [Fact]
    public void WeightedScore_of_empty_history_is_uniform()
    {
        DrawStatistics.WeightedScore(P, []).Values.Should().OnlyContain(p => Math.Abs(p - 1 / 55.0) < 1e-12);
    }

    [Fact]
    public void WeightedScore_favours_recent_draws()
    {
        // 1 ra ở kỳ cũ, 10 ra ở kỳ mới: cùng tần suất nhưng 10 có điểm cao hơn
        var draws = new[] { D(1, 9, 1, 2, 3, 4, 5, 6), D(2, 9, 10, 20, 30, 40, 50, 55) };
        var w = DrawStatistics.WeightedScore(P, draws, decay: 0.5);
        w[10].Should().BeGreaterThan(w[1]);
    }

    [Fact]
    public void OddEven_and_HighLow_cover_all_draws()
    {
        DrawStatistics.OddEven(P, Seed).Values.Sum().Should().Be(40);
        DrawStatistics.HighLow(P, Seed).Values.Sum().Should().Be(40);
        DrawStatistics.OddEven(P, [D(1, 9, 2, 4, 6, 8, 10, 11)])[5].Should().Be(1);
        DrawStatistics.HighLow(P, [D(1, 9, 1, 2, 27, 28, 40, 55)])[3].Should().Be(1);
    }

    [Fact]
    public void SumRange_uses_linear_percentiles()
    {
        var r = DrawStatistics.SumRange(P, Seed);
        r.Min.Should().Be(87);
        r.Max.Should().Be(207);
        r.P25.Should().BeLessThan(r.Median);
        r.Median.Should().BeLessThan(r.P75);
        DrawStatistics.Percentile([1, 2, 3, 4], 25).Should().BeApproximately(1.75, 1e-12);
    }
}
