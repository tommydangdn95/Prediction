using Power655.Core;
using static Power655.Tests.TestData;

namespace Power655.Tests;

public class DrawTests
{
    private static readonly DateOnly Day = new(2026, 11, 5);

    [Fact]
    public void Valid_draw_sorts_numbers_ascending()
    {
        var d = new Draw(P, 1420, Day, [52, 6, 49, 20, 41, 34], 36, 1, 2, "vietlott");

        d.Numbers.Should().Equal(6, 20, 34, 41, 49, 52);
        d.Special.Should().Be(36);
        d.Source.Should().Be("vietlott");
    }

    public static TheoryData<int, int[], int> InvalidCases => new()
    {
        { 0, [1, 2, 3, 4, 5, 6], 7 },            // DrawId ≤ 0
        { 1, [1, 2, 3, 4, 5], 7 },               // thiếu số
        { 1, [1, 2, 3, 4, 5, 6, 8], 7 },         // thừa số
        { 1, [1, 2, 3, 4, 5, 5], 7 },            // trùng
        { 1, [0, 2, 3, 4, 5, 6], 7 },            // < 1
        { 1, [1, 2, 3, 4, 5, 56], 7 },           // > 55
        { 1, [1, 2, 3, 4, 5, 6], 6 },            // ĐB trùng số chính
        { 1, [1, 2, 3, 4, 5, 6], 56 },           // ĐB ngoài khoảng
        { 1, [1, 2, 3, 4, 5, 6], 0 },
    };

    [Theory]
    [MemberData(nameof(InvalidCases))]
    public void Invalid_draw_throws(int id, int[] numbers, int special)
    {
        var act = () => new Draw(P, id, Day, numbers, special);
        act.Should().Throw<InvalidDrawException>();
    }

    [Fact]
    public void Numbers_cannot_be_mutated_through_original_array()
    {
        int[] input = [1, 2, 3, 4, 5, 6];
        var d = new Draw(P, 1, Day, input, 7);
        input[0] = 50;
        d.Numbers[0].Should().Be(1);
    }

    [Fact]
    public void Equality_compares_number_values()
    {
        var a = new Draw(P, 1, Day, [1, 2, 3, 4, 5, 6], 7);
        var b = new Draw(P, 1, Day, [6, 5, 4, 3, 2, 1], 7);
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
        a.HasSameResultAs(new Draw(P, 9, Day, [1, 2, 3, 4, 5, 6], 7, source: "minhngoc")).Should().BeTrue();
    }
}
