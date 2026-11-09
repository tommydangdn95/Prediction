using Power655.Core;
using static Power655.Tests.TestData;

namespace Power655.Tests;

public class PredictionReviewTests
{
    private const string Report = """
        # Gợi ý Vietlott — 08/11/2026

        ## Lotto 5/35

        - Kỳ mới nhất: **#00996** 08/11/2026 — `04 11 17 26 35 | 11`

        | Chiến lược | Bộ số | ĐB | Chẵn/Lẻ | Nhỏ/Lớn (≤17) | Tổng |
        |---|---|---|---|---|---:|
        | hot | `02 04 20 26 35` | `05` | 4/1 | 2/3 | 87 |
        | hot | `01 13 20 27 29` | `06` | 1/4 | 2/3 | 90 |
        | random (đối chứng) | `03 08 20 30 32` | `09` | 4/1 | 2/3 | 93 |

        | Kỳ | Ngày | Kết quả | Jackpot 1 |
        |---|---|---|---:|
        | #00996 | 08/11/2026 | `04 11 17 26 35 | 11` | - |

        ## Mega 6/45

        - Kỳ mới nhất: **#01586** 08/11/2026 — `01 02 14 20 23 36`

        | Chiến lược | Bộ số | Chẵn/Lẻ | Nhỏ/Lớn (≤22) | Tổng |
        |---|---|---|---|---:|
        | cold | `03 09 15 24 33 40` | 2/4 | 3/3 | 124 |
        """;

    [Fact]
    public void Parses_tickets_per_game_and_ignores_other_tables()
    {
        var sets = PredictionArchive.Parse(Report, new DateOnly(2026, 11, 8));

        sets.Should().HaveCount(2);
        var lotto = sets[0];
        lotto.Game.Should().Be(Games.Lotto535);
        lotto.TargetDrawId.Should().Be(997);
        lotto.Tickets.Select(t => t.Strategy).Should().Equal("hot", "hot", "random");
        lotto.Tickets[1].Ticket.Numbers.Should().Equal(1, 13, 20, 27, 29);
        lotto.Tickets[1].Ticket.Special.Should().Be(6);

        sets[1].Game.Should().Be(Games.Mega645);
        sets[1].Tickets.Should().ContainSingle().Which.Ticket.Special.Should().BeNull();
    }

    [Fact]
    public void Parses_recommended_cover_rows()
    {
        const string report = """
            ## Lotto 5/35

            - Kỳ mới nhất: **#00997** 09/11/2026 — `04 16 26 27 29 | 06`

            ### Bộ khuyến nghị — phủ số (8 vé)

            | Vé | Bộ số | ĐB | Mua vé 1→k: P(≥1 vé trúng giải) | P(≥1 vé trùng ≥3 số) | k vé chọn ngẫu nhiên |
            |---|---|---|---:|---:|---:|
            | cover 1 | `12 15 23 28 29` | `04` | **9,60 %** | 1,39 % | 9,60 % |
            | cover 2 | `03 04 13 14 16` | `09` | **18,98 %** | 2,77 % | 18,29 % |
            """;

        var set = PredictionArchive.Parse(report, new DateOnly(2026, 11, 9)).Should().ContainSingle().Subject;

        set.TargetDrawId.Should().Be(998);
        set.Tickets.Should().HaveCount(2).And.OnlyContain(t => t.Strategy == "cover");
        set.Tickets[1].Ticket.Should().BeEquivalentTo(new Ticket([3, 4, 13, 14, 16], 9));
    }

    [Fact]
    public void Reviews_against_the_next_draw_only()
    {
        var sets = PredictionArchive.Parse(Report, new DateOnly(2026, 11, 8));
        var draw = G(Games.Lotto535, 997, 6, 4, 16, 26, 27, 29);

        var reviews = PredictionArchive.Review(sets, [draw, G(Games.Lotto535, 998, 1, 1, 2, 3, 4, 5)]);

        var review = reviews.Should().ContainSingle().Subject;
        review.Draw.DrawId.Should().Be(997);
        review.Tickets.Select(t => t.Main).Should().Equal(2, 2, 0);
        review.Tickets[0].Hits.Should().Equal(4, 26);
        review.Tickets[0].Prize.Should().BeNull();
        review.Tickets[1].Special.Should().BeTrue();
        review.Tickets[1].Prize!.Name.Should().Be("Khuyến khích");

        var scores = StrategyScore.Summarize(Games.Lotto535, reviews);
        scores.Select(s => s.Strategy).Should().Equal("hot", "random");
        scores[0].TotalMatches.Should().Be(4);
        scores[0].Prizes.Should().Be(1);
    }

    [Fact]
    public void Only_dated_files_before_today_are_loaded()
    {
        PredictionArchive.DateOf("predictions/2026-11-08.md").Should().Be(new DateOnly(2026, 11, 8));
        PredictionArchive.DateOf("predictions/LATEST.md").Should().BeNull();

        var dir = Directory.CreateTempSubdirectory().FullName;
        try
        {
            File.WriteAllText(Path.Combine(dir, "2026-11-08.md"), Report);
            File.WriteAllText(Path.Combine(dir, "2026-11-09.md"), Report);
            File.WriteAllText(Path.Combine(dir, "LATEST.md"), Report);
            PredictionArchive.Load(dir, new DateOnly(2026, 11, 9)).Should().HaveCount(2)
                .And.OnlyContain(s => s.Date == new DateOnly(2026, 11, 8));
        }
        finally
        {
            Directory.Delete(dir, true);
        }
    }
}
