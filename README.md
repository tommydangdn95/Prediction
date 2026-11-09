# Power655 Analyzer

Thu thập kết quả Vietlott **Lotto 5/35, Mega 6/45, Power 6/55**, thống kê tần suất và gợi ý bộ số. Spec gốc (6/55): [SPEC (1).md](SPEC%20(1).md).

> Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng.

## Các game

| | Lotto 5/35 (`535`) | Mega 6/45 (`645`) | Power 6/55 (`655`) |
|---|---|---|---|
| Vé | 5 số 01–35 **+ 1 số ĐB 01–12** | 6 số 01–45 | 6 số 01–55 |
| Số ĐB khi quay | lồng riêng 1–12 (có thể trùng số chính) | không có | số thứ 7, cùng lồng |
| Lịch quay (giờ VN) | 13:00 và 21:00 mỗi ngày | T4, T6, CN 18:00 | T3, T5, T7 18:00 |
| Nguồn | vietlott | vietlott, minhngoc | vietlott, minhngoc |
| Giải | Độc đắc 5+ĐB · Nhất 5 · Nhì 4+ĐB · Ba 4 · Tư 3+ĐB · Năm 3 · KK 0–2+ĐB | Jackpot 6 · Nhất 5 · Nhì 4 · Ba 3 | JP1 6 · JP2 5+ĐB · Nhất 5 · Nhì 4 · Ba 3 |

Xác suất mỗi hạng được tính bằng tổ hợp (`GameOdds`) và luôn in kèm mọi gợi ý/backtest.

## Yêu cầu

- .NET SDK 8 trở lên (project target `net8.0`)

## Bắt đầu

```bash
dotnet build
dotnet test

# chạy CLI (DB mặc định: data/power655.db, đổi bằng --db hoặc biến POWER655_DB)
# --game / -g: 535 | 645 | 655 | all (mặc định all)
dotnet run --project src/Power655.Cli -- fetch --pages 5            # cả 3 game, ~40 kỳ mỗi game
dotnet run --project src/Power655.Cli -- suggest --strategy hot --sets 3 --seed 42
dotnet run --project src/Power655.Cli -- suggest -g 535 -s balanced  # chỉ Lotto 5/35 (kèm số ĐB)
dotnet run --project src/Power655.Cli -- stats -g 645 --last 40
dotnet run --project src/Power655.Cli -- backtest --train 20
dotnet run --project src/Power655.Cli -- import --csv data/seed/draws_seed.csv   # seed 6/55
dotnet run --project src/Power655.Cli -- export --csv out.csv
```

Thêm `-v` để xem log chi tiết (từng trang fetch, retry...).

## Chạy tự động hằng ngày (GitHub Actions)

- [`daily.yml`](.github/workflows/daily.yml): nạp `data/history.csv` → `fetch` → lưu lại lịch sử → `report`
  ra [`predictions/LATEST.md`](predictions/LATEST.md) và `predictions/YYYY-MM-DD.md` → commit.
  Kích hoạt bằng cron-job.org qua GitHub API: xem [docs/cron-job.md](docs/cron-job.md).
- [`ci.yml`](.github/workflows/ci.yml): build + test mỗi lần push / PR.

Chạy báo cáo ở máy: `dotnet run --project src/Power655.Cli -- report --out report.md`.

Mỗi báo cáo còn có:
- **Kiểm tra gợi ý ngày trước**: đọc lại `predictions/yyyy-MM-dd.md` cũ (đổi bằng `--predictions <dir>`), so từng bộ với
  kết quả thật của kỳ ngay sau “Kỳ mới nhất” trong file đó (số trùng in đậm, hạng giải), kèm bảng thành tích cộng dồn
  theo chiến lược so với kỳ vọng ngẫu nhiên.
- **Khả năng (%) của mỗi bộ**: xác suất trúng giải bất kỳ, ≥3 số, độc đắc và phân phối số trùng 0..k, cùng cột % trong bảng giải.
  Mọi bộ hợp lệ có cùng xác suất.
- **Tỉ lệ xuất hiện từng số + kiểm định χ²** trên toàn bộ lịch sử: p lớn nghĩa là không có dấu hiệu lệch để khai thác.
- **Bộ khuyến nghị — phủ số** (đứng đầu mỗi game; `--cover N`, mặc định = tổng số bộ theo thống kê): N vé ít trùng
  nhau nhất kèm P(≥1 vé trúng) **chính xác** khi mua k vé đầu (`PortfolioOdds` duyệt mọi kết quả quay), so với k vé
  ngẫu nhiên và với các bộ theo thống kê (hot/cold/balanced, in ngay sau để tham khảo).
- **Backtest theo nhóm vé** (`Backtester.RunGroups`/`RunCover`): mua N vé mỗi kỳ trên lịch sử thật → tỉ lệ kỳ có ≥1 vé
  trúng của từng cách chọn, so với lý thuyết.

`data/history.csv` chứa toàn bộ lịch sử cả 3 game: Lotto 5/35 (từ kỳ #00001, 29/06/2025), Mega 6/45 (từ #00001,
20/07/2016), Power 6/55 (từ #00001, 01/08/2017).
Nạp lại toàn bộ một game: `fetch -g 645 --pages 220 --source vietlott` (dừng khi hết trang, nghỉ 1s/trang).

## Chiến lược gợi ý

| Strategy | 6/55, 6/45 | 5/35 |
|---|---|---|
| `random` | ngẫu nhiên đều — luôn in kèm để đối chứng | + ĐB ngẫu nhiên |
| `hot` | random trong top-18 `WeightedScore` | top-15; ĐB trong top-4 ĐB theo `SpecialWeightedScore` |
| `cold` | random trong top-18 số gan | top-15; ĐB trong top-4 ĐB gan |
| `balanced` | 3 chẵn/3 lẻ, 3 nhỏ/3 lớn, tổng ∈ [P25,P75] | 2–3 chẵn, 2–3 nhỏ (≤17), tổng ∈ [P25,P75]; ĐB ngẫu nhiên |
| `cover` | nhóm vé: vòng đầu rời nhau (7 vé 6/45, 9 vé 6/55), sau đó mỗi cặp vé trùng ≤ 1 số | 7 vé phủ đủ 35 số; ĐB khác nhau |

`cover` không dùng lịch sử và không đổi xác suất từng vé; nó chỉ tăng P(ít nhất một vé trong nhóm trúng). Backtest
mua 8 vé mỗi kỳ trên toàn bộ lịch sử — tỉ lệ kỳ có ≥1 vé trúng:

| 8 vé/kỳ | hot | random | cover |
|---|---:|---:|---:|
| Lotto 5/35 (977 kỳ) | 34,6 % | 55,1 % | 70,5 % |
| Mega 6/45 (1.566 kỳ) | 14,4 % | 17,5 % | 18,9 % |
| Power 6/55 (1.401 kỳ) | 8,4 % | 10,3 % | 10,8 % |

hot/cold lấy cả nhóm vé từ cùng ~15–18 số (và 4 số ĐB) nên các vé hay cùng trượt. Số vé trúng kỳ vọng và tiền thưởng
kỳ vọng như nhau với mọi cách chọn. Dùng ở máy: `suggest -g 535 -s cover --sets 8`.

Ranh giới nhỏ/lớn = MaxNumber/2 (27, 22, 17). Backtest so sánh với random bằng chênh lệch ghép cặp theo từng kỳ,
kèm p-value và hiệu chỉnh Bonferroni khi thử nhiều chiến lược. Kết quả trên mẫu nhỏ chỉ mang tính thăm dò và cần
được kiểm chứng bằng dữ liệu mới.

## Cấu trúc

| Project | Nội dung |
|---|---|
| `Power655.Core` | `Game`/`Games` (luật, lịch, cơ cấu giải), `Draw` + validation, `Ticket`, `DrawStatistics`, chiến lược, `Backtester`, `GameOdds`, `DrawSchedule` |
| `Power655.Data` | EF Core + SQLite (`Draws`, khóa `(Game, DrawId)`), migration, `DrawRepository.UpsertAsync`, CSV |
| `Power655.Scraper` | `VietlottSource` (AjaxPro, cả 3 game), `MinhNgocSource` (6/55, 6/45), `DrawFetchService` (fallback + cross-check), Polly |
| `Power655.Cli` | `power655` – System.CommandLine + Spectre.Console |
| `Power655.Tests` | xUnit + FluentAssertions |

Migration mới: `dotnet tool restore && dotnet ef migrations add <Tên> --project src/Power655.Data`.
Migration `MultiGame` gán `Game = '655'` cho dữ liệu cũ.

## Quy tắc dữ liệu

- Upsert theo `(Game, DrawId)`; chạy `import`/`fetch` nhiều lần không tạo bản trùng. Cùng số kỳ ở hai game khác nhau là hai dòng khác nhau.
- Khi hai nguồn khác số: ưu tiên `vietlott` > `minhngoc` > `seed`, luôn in cảnh báo.
- `fetch` mặc định lấy vietlott rồi lấy trang 1 minhngoc để cross-check và bổ sung kỳ mới (trang vietlott có thể cache). Lotto 5/35 chỉ có nguồn vietlott.
- CSV: `game,draw_id,date,n1..n6,special,jackpot1,jackpot2,source`; `n6` trống với 5/35, `special` trống với 6/45. CSV không có cột `game` cần `--game`.
- Endpoint AJAX vietlott: xem [docs/vietlott-endpoint.md](docs/vietlott-endpoint.md).
