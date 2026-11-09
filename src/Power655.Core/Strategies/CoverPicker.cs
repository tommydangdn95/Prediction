namespace Power655.Core.Strategies;

/// <summary>
/// "Phủ số": sinh cả nhóm vé khác nhau, ưu tiên giảm trùng số —
/// vòng đầu rời nhau hoàn toàn (5/35: 7 vé phủ đủ 35 số; 6/45: 7 vé; 6/55: 9 vé), sau đó ưu tiên
/// số ít dùng và cặp số ít đi chung; số ĐB Lotto 5/35 được dùng lần lượt theo chu kỳ 12 số.
/// <para>
/// Xác suất trúng của TỪNG vé không đổi (mọi vé như nhau). P(ít nhất một vé trong nhóm trúng)
/// có thể cao hơn nhóm vé tập trung vào ít số. Xác suất của nhóm cụ thể được tính bằng PortfolioOdds.
/// Đây là thuật toán tham lam, không bảo đảm nhóm tối ưu toàn cục. Không dùng lịch sử.
/// Số vé trúng kỳ vọng không đổi; trúng giải không đồng nghĩa với có lãi.
/// </para>
/// </summary>
public static class CoverPicker
{
    public const string Name = "cover";

    public static IReadOnlyList<Ticket> Pick(Game game, int count, Random rng)
    {
        ArgumentNullException.ThrowIfNull(game);
        ArgumentNullException.ThrowIfNull(rng);
        TicketGroups.ValidateCount(game, count);

        var max = game.MaxNumber;
        var usage = new int[max + 1];
        var together = new int[max + 1, max + 1];
        var inTicket = new bool[max + 1];
        var order = new int[max];
        var specials = new Queue<int>();
        var tickets = new List<Ticket>(count);
        var seen = new HashSet<string>(StringComparer.Ordinal);
        var limit = TicketGroups.AttemptLimit(count);

        for (long attempt = 0; tickets.Count < count && attempt < limit; attempt++)
        {
            var chosen = new List<int>(game.PickCount);
            while (chosen.Count < game.PickCount)
            {
                // số ít dùng nhất, rồi ít đi chung với các số đã chọn trong vé này; hòa thì ngẫu nhiên
                for (var i = 0; i < max; i++) order[i] = i + 1;
                ShuffleInPlace(order, rng);
                int best = 0, bestUsage = int.MaxValue, bestTogether = int.MaxValue;
                foreach (var x in order)
                {
                    if (inTicket[x] || usage[x] > bestUsage) continue;
                    var with = 0;
                    foreach (var y in chosen) with += together[x, y];
                    if (usage[x] < bestUsage || with < bestTogether)
                        (best, bestUsage, bestTogether) = (x, usage[x], with);
                }
                chosen.Add(best);
                inTicket[best] = true;
            }

            int? special = null;
            if (game.TicketHasSpecial)
            {
                // chia đều số ĐB: đủ 12 số khác nhau rồi mới lặp lại
                if (specials.Count == 0)
                {
                    var deck = game.AllSpecialNumbers.ToArray();
                    ShuffleInPlace(deck, rng);
                    foreach (var s in deck) specials.Enqueue(s);
                }
                special = specials.Peek();
            }
            var ticket = new Ticket(chosen.Order().ToArray(), special);
            foreach (var x in chosen) inTicket[x] = false;
            if (!seen.Add(TicketGroups.Key(game, ticket))) continue;
            foreach (var x in chosen)
            {
                usage[x]++;
                foreach (var y in chosen)
                    if (x != y) together[x, y]++;
            }
            if (game.TicketHasSpecial) specials.Dequeue();
            tickets.Add(ticket);
        }
        if (tickets.Count != count) throw TicketGroups.GenerationFailed(count, tickets.Count);
        return tickets;
    }

    private static void ShuffleInPlace(int[] buffer, Random rng)
    {
        for (var i = buffer.Length - 1; i > 0; i--)
        {
            var j = rng.Next(i + 1);
            (buffer[i], buffer[j]) = (buffer[j], buffer[i]);
        }
    }
}
