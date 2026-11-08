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

## Chiến lược gợi ý

| Strategy | 6/55, 6/45 | 5/35 |
|---|---|---|
| `random` | ngẫu nhiên đều — luôn in kèm để đối chứng | + ĐB ngẫu nhiên |
| `hot` | random trong top-18 `WeightedScore` | top-15; ĐB trong top-4 ĐB theo `SpecialWeightedScore` |
| `cold` | random trong top-18 số gan | top-15; ĐB trong top-4 ĐB gan |
| `balanced` | 3 chẵn/3 lẻ, 3 nhỏ/3 lớn, tổng ∈ [P25,P75] | 2–3 chẵn, 2–3 nhỏ (≤17), tổng ∈ [P25,P75]; ĐB ngẫu nhiên |

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
