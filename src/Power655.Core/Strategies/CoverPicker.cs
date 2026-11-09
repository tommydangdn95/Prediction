namespace Power655.Core.Strategies;

/// <summary>
/// "Phủ số": sinh cả nhóm vé cùng lúc sao cho các vé ít trùng số nhau nhất —
/// vòng đầu rời nhau hoàn toàn (5/35: 7 vé phủ đủ 35 số; 6/45: 7 vé; 6/55: 9 vé), các vé sau mỗi vé trùng
/// với mỗi vé trước tối đa ~1 số — và số ĐB (Lotto 5/35) khác nhau.
/// <para>
/// Xác suất trúng của TỪNG vé không đổi (mọi vé như nhau). Cái tăng lên là P(ít nhất một vé trong nhóm trúng)
/// so với cùng số vé chọn ngẫu nhiên/theo hot-cold (các vé đó hay trùng số nhau nên trúng/trượt cùng lúc).
/// Số vé trúng kỳ vọng và tiền thưởng kỳ vọng không đổi. Không dùng lịch sử.
/// Vé được sắp sao cho mua k vé đầu tiên cũng là nhóm phủ tốt cho k vé.
/// </para>
/// </summary>
public static class CoverPicker
{
    public const string Name = "cover";

    public static IReadOnlyList<Ticket> Pick(Game game, int count, Random rng)
    {
        ArgumentNullException.ThrowIfNull(game);
        ArgumentNullException.ThrowIfNull(rng);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        var max = game.MaxNumber;
        var usage = new int[max + 1];
        var together = new int[max + 1, max + 1];
        var inTicket = new bool[max + 1];
        var order = new int[max];
        var specials = new Queue<int>();
        var tickets = new List<Ticket>(count);

        for (var t = 0; t < count; t++)
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

            foreach (var x in chosen)
            {
                usage[x]++;
                inTicket[x] = false;
                foreach (var y in chosen)
                    if (x != y) together[x, y]++;
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
                special = specials.Dequeue();
            }
            tickets.Add(new Ticket(chosen.Order().ToArray(), special));
        }
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
