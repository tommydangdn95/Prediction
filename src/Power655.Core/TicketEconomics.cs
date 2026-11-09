namespace Power655.Core;

/// <summary>
/// Mô hình tiền giải cơ bản trước thuế. Không tính Jackpot, phần chia thêm của Lotto 5/35,
/// hoặc giá trị thực nhận; không được diễn giải là lợi nhuận thực tế.
/// </summary>
public static class TicketEconomics
{
    public const decimal TicketPrice = 10_000m;
    public const string ModelDescription = "Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35";
    private static readonly decimal[] LottoPrizes = [0m, 10_000_000m, 5_000_000m, 500_000m, 100_000m, 30_000m, 10_000m];
    private static readonly decimal[] MegaPrizes = [0m, 10_000_000m, 300_000m, 30_000m];
    private static readonly decimal[] PowerPrizes = [0m, 0m, 40_000_000m, 500_000m, 50_000m];

    // Cơ cấu giải kiểm tra ngày 09/11/2026:
    // https://info.vietlott-sms.vn/quydinh/game_mega.html
    // https://vietlott.vn/vi/trung-thuong/ket-qua-trung-thuong/655
    // https://vietlott.vn/vi/trung-thuong/ket-qua-trung-thuong/535
    public static decimal BasePrizeAmount(Game game, PrizeTier? prize)
    {
        ArgumentNullException.ThrowIfNull(game);
        if (prize is null) return 0m;
        var index = -1;
        for (var i = 0; i < game.Prizes.Count; i++)
            if (game.Prizes[i] == prize) { index = i; break; }
        if (index < 0) throw new ArgumentException("Hạng giải không thuộc game.", nameof(prize));
        decimal[] amounts = game.Code switch
        {
            "535" => LottoPrizes,
            "645" => MegaPrizes,
            "655" => PowerPrizes,
            _ => throw new ArgumentException("Chưa có cơ cấu giải cơ bản cho game.", nameof(game)),
        };
        return amounts[index];
    }

    public static decimal BasePayout(Game game, Ticket ticket, Draw draw)
    {
        if (draw.Game != game) throw new ArgumentException("Khác game.", nameof(draw));
        return BasePrizeAmount(game, game.Odds.Classify(ticket, draw));
    }

    public static double ExpectedBasePayoutPerTicket(Game game) =>
        game.Prizes.Sum(prize => game.Odds.PrizeProbability(prize) * (double)BasePrizeAmount(game, prize));
}
