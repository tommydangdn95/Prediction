# SPEC – Power655 Analyzer

> App thu thập kết quả Vietlott Power 6/55, thống kê tần suất và gợi ý bộ số.
> File này dùng làm spec cho AI agent / dev làm việc trong VS Code.

---

## 1. Mục tiêu

| # | Mục tiêu | Ghi chú |
|---|---|---|
| G1 | Tự động lấy kết quả N trang gần nhất (mặc định 5 trang ≈ 40 kỳ) từ vietlott.vn | Có nguồn dự phòng |
| G2 | Lưu lịch sử vào DB local, không trùng kỳ | Upsert theo `DrawId` |
| G3 | Thống kê: tần suất, số nóng/lạnh, số gan, số đặc biệt, cặp số hay đi cùng | |
| G4 | Sinh bộ số gợi ý theo nhiều chiến lược + bộ ngẫu nhiên đối chứng | |
| G5 | Backtest chiến lược và hiển thị xác suất thật | **Bắt buộc**, xem mục 8 |
| G6 | Chạy được qua CLI; sau đó có Web UI | |

**Ngoài phạm vi:** mua vé, kết nối ví/thanh toán, cam kết tỉ lệ trúng.

---

## 2. Tech stack

- **.NET 8**, C# 12
- Solution layout:

```
Power655Analyzer/
├─ src/
│  ├─ Power655.Core/          # Domain + thống kê (không phụ thuộc IO)
│  ├─ Power655.Scraper/       # Lấy dữ liệu (HttpClient + AngleSharp)
│  ├─ Power655.Data/          # EF Core + SQLite
│  ├─ Power655.Cli/           # Console app (System.CommandLine)
│  └─ Power655.Web/           # (Phase 2) Blazor Server hoặc ASP.NET Minimal API + React
├─ tests/
│  └─ Power655.Tests/         # xUnit + FluentAssertions
├─ data/seed/draws_seed.csv   # 40 kỳ mẫu (xem Phụ lục A)
└─ SPEC.md
```

- NuGet: `AngleSharp`, `Microsoft.EntityFrameworkCore.Sqlite`, `System.CommandLine`, `Polly` (retry), `Spectre.Console` (bảng màu CLI), `xunit`, `FluentAssertions`.

---

## 3. Domain model

```csharp
public sealed record Draw(
    int DrawId,              // vd 1420
    DateOnly Date,           // 05/11/2026
    IReadOnlyList<int> Numbers, // 6 số, tăng dần, 1..55, không trùng
    int Special,             // 1..55, không nằm trong Numbers
    long? Jackpot1,
    long? Jackpot2,
    string Source);          // "vietlott" | "minhngoc" | "seed"
```

Validation (throw `InvalidDrawException`):
- `Numbers.Count == 6`, phân biệt, mỗi số ∈ [1,55]
- `Special` ∈ [1,55] và ∉ `Numbers`
- `DrawId > 0`

Bảng SQLite `Draws`: `DrawId` (PK), `Date`, `N1..N6`, `Special`, `Jackpot1`, `Jackpot2`, `Source`, `FetchedAt`.

---

## 4. Thu thập dữ liệu (Power655.Scraper)

### 4.1 Interface
```csharp
public interface IDrawSource
{
    string Name { get; }
    Task<IReadOnlyList<Draw>> FetchPageAsync(int page, CancellationToken ct);
}
```

### 4.2 Nguồn chính – vietlott.vn
- Trang: `https://vietlott.vn/vi/trung-thuong/ket-qua-trung-thuong/winning-number-655`
- **Lưu ý:** HTML tĩnh chỉ chứa 8 kỳ; các trang 2..n được tải bằng **AJAX (AjaxPro)**. GET trang thường có thể trả dữ liệu cache cũ.
- Việc cần làm trước khi code (**TASK S-0**): mở DevTools → Network → bấm sang trang 2, ghi lại:
  - URL endpoint (dự kiến dạng `/ajaxpro/Vietlott.PlugIn.WebParts.Game655CompareWebPart,Vietlott.PlugIn.WebParts.ashx`, **chưa xác minh**)
  - Method, header `X-AjaxPro-Method`, body JSON (tham số page index, ORenderInfo…)
  - Cookie/token cần thiết
  - Ghi kết quả vào `docs/vietlott-endpoint.md`
- Parse phần HTML trả về (trong JSON) bằng AngleSharp.

### 4.3 Nguồn dự phòng – minhngoc.net.vn
- `https://www.minhngoc.net.vn/ket-qua-xo-so/dien-toan-vietlott/power-6x55.html`
- Mỗi kỳ có "Kỳ vé: #01420", 6 số chính + 1 số đặc biệt (số thứ 7).

### 4.4 Yêu cầu chung
- `HttpClient` qua `IHttpClientFactory`, User-Agent chung chung, timeout 15s.
- Polly: retry 3 lần, exponential backoff; delay ≥ 1s giữa các trang (lịch sự với server).
- Nguồn chính lỗi → tự chuyển nguồn dự phòng, log cảnh báo.
- Cross-check: nếu 2 nguồn cùng có một `DrawId` mà số khác nhau → log lỗi, ưu tiên vietlott.
- Lịch quay: **Thứ 3, 5, 7, 18:00–18:30 (UTC+7)**. Chỉ fetch kỳ mới sau 18:45.

---

## 5. Thống kê (Power655.Core) – hàm thuần, có unit test

| Hàm | Mô tả |
|---|---|
| `Frequency(draws)` | `Dictionary<int,int>` số lần xuất hiện số chính |
| `SpecialFrequency(draws)` | tần suất số đặc biệt |
| `Gap(draws)` | số kỳ kể từ lần cuối mỗi số xuất hiện (0 = vừa ra) |
| `HotCold(draws, top)` | top nóng / lạnh |
| `PairFrequency(draws, top)` | cặp số hay ra cùng nhau |
| `WeightedScore(draws, decay=0.95, alpha=1.0)` | tần suất có trọng số thời gian + làm mượt Laplace; trả về "tỉ lệ mô hình" mỗi số |
| `ChiSquareUniform(draws)` | chi², df=54, p-value (dùng `MathNet.Numerics`) |
| `OddEven`, `HighLow`, `SumRange` | phân bố chẵn/lẻ, nhỏ (1–27)/lớn (28–55), tổng 6 số |

Kiểm tra với seed 40 kỳ (Phụ lục A):
- `Frequency[18] == 11`, `Frequency[7] == 9`
- `Gap[15] == 29`
- `ChiSquareUniform` → chi² ≈ 48.3, p ≈ 0.69

---

## 6. Chiến lược gợi ý

```csharp
public interface IPickStrategy
{
    string Name { get; }
    IReadOnlyList<int> Pick(IReadOnlyList<Draw> history, Random rng);
}
```

| Strategy | Logic |
|---|---|
| `RandomPick` | 6 số ngẫu nhiên đều – **luôn hiển thị để đối chứng** |
| `HotPick` | random 6 trong top-18 `WeightedScore` |
| `ColdPick` | random 6 trong top-18 số gan |
| `BalancedPick` | lọc bộ có 3 chẵn/3 lẻ, 3 nhỏ/3 lớn, tổng trong khoảng P25–P75 lịch sử |

`Random` nhận seed từ CLI để tái lập kết quả.

---

## 7. Backtest

- Walk-forward: với mỗi kỳ t (từ kỳ thứ 21 trở đi) chỉ dùng dữ liệu < t để chọn số.
- Output mỗi strategy: số trùng trung bình/kỳ, tỉ lệ ≥3 số (giải Ba), so với lý thuyết 36/55 ≈ 0.655.
- Chạy `RandomPick` 1.000 lần lấy trung bình làm baseline.
- Kết quả hiện tại (seed 40 kỳ, Python prototype): Hot = 0.85, Random = 0.85, nghĩa là **không có lợi thế**.

---

## 8. Yêu cầu minh bạch (bắt buộc)

Mọi màn hình/kết quả gợi ý phải hiển thị:

| Giải | Xác suất / vé |
|---|---|
| Jackpot 1 (6 số) | 1 / 28.989.675 |
| Jackpot 2 (5 số + ĐB) | 1 / 4.831.612 |
| Giải Nhất (5 số) | 1 / 100.659 |
| Giải Nhì (4 số) | 1 / 1.643 |
| Giải Ba (3 số) | 1 / 79 |

Cùng dòng chú thích: *"Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng."*
Không dùng từ "chắc chắn", "dễ trúng", "soi cầu chuẩn".

---

## 9. CLI (Power655.Cli)

```bash
power655 fetch --pages 5 [--source vietlott|minhngoc]   # lấy & lưu DB
power655 import --csv data/seed/draws_seed.csv          # nạp CSV
power655 stats  --last 40                               # bảng tần suất, nóng/lạnh/gan, chi²
power655 suggest --strategy hot|cold|balanced|random --sets 3 --seed 42
power655 backtest --train 20
power655 export --csv out.csv
```

Output bằng `Spectre.Console` (bảng, màu nóng = đỏ, lạnh = xanh).

---

## 10. Web UI (Phase 2)

- Dashboard: heatmap 55 số (tần suất), bar chart số đặc biệt, bảng kỳ gần nhất.
- Trang Gợi ý: chọn strategy, số bộ, nút "Tạo"; luôn kèm bảng xác suất mục 8.
- Trang Backtest: so sánh các strategy với baseline.
- Background job (`IHostedService`) fetch tự động 18:45 thứ 3/5/7 (Asia/Ho_Chi_Minh).

---

## 11. Kế hoạch triển khai (task cho agent)

| ID | Task | Done khi |
|---|---|---|
| T1 | Tạo solution + projects theo mục 2 | `dotnet build` pass |
| T2 | Domain `Draw` + validation + tests | test pass |
| T3 | Import CSV seed → SQLite (EF Core migration) | 40 dòng trong DB |
| T4 | Các hàm thống kê mục 5 + tests với số kiểm tra | test pass |
| T5 | CLI `stats`, `suggest`, `backtest` | output khớp mục 5/7 |
| S-0 | Xác minh endpoint AJAX Vietlott (thủ công) | có `docs/vietlott-endpoint.md` |
| T6 | `VietlottSource` + `MinhNgocSource` + fallback + Polly | `fetch --pages 5` lấy đủ 40 kỳ |
| T7 | Lệnh `fetch` upsert + cross-check | chạy 2 lần không trùng |
| T8 | Web UI Phase 2 | — |

Quy ước: nullable enabled, `TreatWarningsAsErrors`, mỗi PR có test.

---

## Phụ lục A – `data/seed/draws_seed.csv`

```csv
draw_id,date,n1,n2,n3,n4,n5,n6,special
1420,2026-11-05,6,20,34,41,49,52,36
1419,2026-11-03,10,22,31,39,43,54,14
1418,2026-10-31,12,18,22,26,28,55,11
1417,2026-10-29,9,29,33,38,45,46,53
1416,2026-10-27,9,18,27,28,31,35,50
1415,2026-10-24,6,7,18,41,44,50,53
1414,2026-10-22,14,19,24,31,34,42,37
1413,2026-10-20,13,20,23,30,50,55,45
1412,2026-10-17,1,10,11,12,16,37,48
1411,2026-10-15,7,23,25,42,45,51,4
1410,2026-10-13,2,14,27,40,50,53,26
1409,2026-10-10,14,17,21,23,24,39,30
1408,2026-10-08,1,7,12,27,31,52,6
1407,2026-10-06,6,7,18,20,24,27,1
1406,2026-10-03,7,11,13,16,18,54,41
1405,2026-10-01,4,5,13,34,52,55,15
1404,2026-09-29,2,4,13,17,35,36,11
1403,2026-09-26,14,18,21,38,48,52,49
1402,2026-09-24,1,13,23,25,26,28,27
1401,2026-09-22,1,3,9,11,41,46,10
1400,2026-09-19,4,7,11,18,22,25,50
1399,2026-09-17,6,11,25,27,37,45,15
1398,2026-09-15,24,27,32,36,38,47,52
1397,2026-09-12,7,24,31,43,47,54,22
1396,2026-09-10,2,5,28,32,51,53,50
1395,2026-09-08,8,11,14,23,25,54,17
1394,2026-09-05,9,11,24,31,33,47,21
1393,2026-09-03,8,9,16,42,46,47,11
1392,2026-09-01,1,17,41,44,49,55,45
1391,2026-08-29,5,10,15,29,34,45,24
1390,2026-08-27,1,3,11,21,26,44,10
1389,2026-08-25,5,7,13,18,31,40,14
1388,2026-08-22,9,18,19,21,25,36,8
1387,2026-08-20,2,8,29,38,39,51,47
1386,2026-08-18,3,15,18,38,41,48,30
1385,2026-08-15,16,20,25,27,30,50,2
1384,2026-08-13,5,9,27,29,45,46,42
1383,2026-08-11,2,7,19,20,39,50,31
1382,2026-08-08,5,29,33,38,40,45,37
1381,2026-08-06,14,18,23,35,51,55,1
```

> `draw_id` từ 1381–1408 được suy ra theo thứ tự liên tiếp (1408–1420 đã đối chiếu với nguồn). Khi T6 chạy xong, dữ liệu fetch thật sẽ ghi đè.

## Phụ lục B – Prototype tham chiếu
`power655_predict.py` (Python) – logic thống kê/backtest gốc, dùng để đối chiếu kết quả khi port sang C#.
