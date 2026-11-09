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
# Cùng 5 vé/kỳ; đánh giá từ kỳ 1400 nhưng vẫn dùng lịch sử trước đó để chọn số
dotnet run --project src/Power655.Cli -- backtest -g 655 --group-size 5 --groups 50 --from-draw 1400
dotnet run --project src/Power655.Cli -- import --csv data/seed/draws_seed.csv   # seed 6/55
dotnet run --project src/Power655.Cli -- export --csv out.csv
```

Thêm `-v` để xem log chi tiết (từng trang fetch, retry...).

## Chạy tự động hằng ngày (GitHub Actions)

- [`daily.yml`](.github/workflows/daily.yml): nạp `data/history.csv` → `fetch` → lưu lại lịch sử → `report`
  ra [`predictions/LATEST.md`](predictions/LATEST.md), `predictions/YYYY-MM-DD.md` và sổ dự đoán `predictions/ledger/` → commit.
  Kích hoạt bằng cron-job.org qua GitHub API: xem [docs/cron-job.md](docs/cron-job.md).
- [`ci.yml`](.github/workflows/ci.yml): build + test mỗi lần push / PR.

Chạy báo cáo ở máy: `dotnet run --project src/Power655.Cli -- report --out report.md`.

Mỗi báo cáo còn có:
- **Đối chiếu dự đoán đã lưu**: chỉ đọc `predictions/ledger/{game}-{targetDrawId}.json` (đổi thư mục bằng `--predictions`).
  Mỗi game/kỳ lưu bản đầu tiên, gồm giờ tạo, giờ quay mục tiêu, vé, seed, cấu hình, phiên bản chương trình và hash lịch sử.
  Chạy lại giữ nguyên bản này và hiển thị đúng các vé đã lưu, kể cả thay seed hoặc số vé. Hai ca Lotto cùng ngày có hai bản riêng.
  Khi đối chiếu, kiểm tra giờ tạo trước giờ quay, lịch/ca quay và hash đầu vào; một kỳ chỉ được đếm một lần.
  Markdown cũ không có dấu vết này nên không dùng làm bằng chứng dự đoán trước kỳ quay.
- **Khả năng (%) của mỗi bộ**: xác suất trúng giải bất kỳ, ≥3 số, độc đắc và phân phối số trùng 0..k, cùng cột % trong bảng giải.
  Mọi bộ hợp lệ có cùng xác suất.
- **Tần suất + χ²**: hiệu chỉnh thống kê Pearson bằng `(N−1)/(N−k)` vì rút không hoàn lại, với df `N−1`.
  p lớn chỉ là chưa bác bỏ tần suất đều; không chứng minh tính độc lập hoặc khả năng dự đoán. p là xấp xỉ và cần thận trọng khi ít kỳ/nhiều phép thử.
- **Phương án phủ số** (`--cover N`, mặc định = `4 × --sets`): giảm trùng giữa N vé, kèm chi phí và P(≥1 vé trúng)
  **chính xác cho nhóm cụ thể** (`PortfolioOdds` duyệt mọi kết quả quay). Không bảo đảm tìm nhóm tối ưu toàn cục.
  Đối chứng là cùng số vé ngẫu nhiên **khác nhau**, lấy mẫu không hoàn lại trên toàn bộ không gian vé.
- **Backtest theo ngân sách**: đo tỷ lệ trúng giải từng vé, tỷ lệ nhóm có giải, khoảng tin cậy và chênh lệch ghép cặp theo kỳ thật.
  Bảng tiền thể hiện chi phí, thưởng cơ bản, lãi/lỗ trung bình, ROI và tỷ lệ thu ≥ chi / thu > chi.

`report --out` mới ghi sổ dự đoán; không có `--out` là xem trước. Nếu dữ liệu cũ khiến kỳ ngay sau đã bắt đầu,
report đánh dấu và không tạo dự đoán cho game đó (mã thoát 2; workflow vẫn lưu các game còn hợp lệ).
Giờ bắt đầu quay (13:00/21:00/18:00) được tách khỏi giờ dự kiến có kết quả (+45 phút).
Timestamp/hash là dấu vết tại máy, không phải chứng thực bên ngoài: cần commit/công bố trước giờ quay để kiểm chứng thời điểm độc lập.

Tiền trong backtest là **mô hình giải cơ bản trước thuế**, loại trừ Jackpot và phần chia thêm Lotto 5/35;
không phải toàn bộ tiền thực nhận hay xác suất có lời sau thuế. Không lấy cả quỹ Jackpot của kỳ làm tiền nhận của một vé.
Giá vé cơ bản 10.000đ. Cơ cấu giải tham chiếu: [Mega](https://info.vietlott-sms.vn/quydinh/game_mega.html),
[Power](https://vietlott.vn/vi/trung-thuong/ket-qua-trung-thuong/655), [Lotto](https://vietlott.vn/vi/trung-thuong/ket-qua-trung-thuong/535).

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
| `cover` | vòng đầu rời nhau (7 vé 6/45, 9 vé 6/55), sau đó ưu tiên số ít dùng/cặp ít đi chung | 7 vé đầu phủ đủ 35 số; ĐB đi theo chu kỳ 12 số |

`cover` không dùng lịch sử và không đổi xác suất từng vé; nó có thể tăng P(ít nhất một vé trong nhóm trúng).
Trúng giải nhỏ không đồng nghĩa hòa vốn: Lotto có đủ 12 số ĐB trên 12 vé luôn có giải, nhưng vẫn có thể lỗ tiền.
Dùng ở máy: `suggest -g 535 -s cover --sets 8`.

Thử lại trên toàn bộ lịch sử: nếu mỗi kỳ mua **8 vé khác nhau** (80.000đ), tỉ lệ số kỳ có ít nhất 1 vé trúng và
tiền thưởng trung bình thu về mỗi kỳ (giải cố định, không tính Jackpot):

| 8 vé/kỳ | hot | random | cover | Tiền thu về TB/kỳ (random → cover) |
|---|---:|---:|---:|---:|
| Lotto 5/35 (977 kỳ) | 34,6 % | 55,5 % | **70,5 %** | 14.122đ → 13.796đ |
| Mega 6/45 (1.566 kỳ) | 14,2 % | 17,4 % | **18,8 %** | 10.564đ → 10.396đ |
| Power 6/55 (1.401 kỳ) | 8,6 % | 10,0 % | **10,7 %** | 9.251đ → 10.996đ |

Ví dụ Lotto: mua 8 vé phủ số thì khoảng 7 trong 10 kỳ có vé trúng, so với khoảng 5–6 kỳ nếu chọn ngẫu nhiên.
Nhưng tiền thu về vẫn khoảng 14.000đ cho 80.000đ tiền vé với mọi cách chọn: phủ số làm **ít kỳ trắng tay hơn**,
không làm **lời hơn**. hot/cold lấy cả nhóm vé từ cùng ~15–18 số (và 4 số ĐB) nên các vé hay cùng trượt.
(Số liệu ngày 09/11/2026: `backtest --group-size 8 --groups 50 --runs 300`, seed 42.)

Các nhóm vé được loại trùng toàn bộ bộ số (kể cả ĐB với Lotto); không cố ý đổi chiến lược sang random khi không tạo đủ vé khác nhau.
Đối chứng và các cách chọn cùng số vé/giá vé. Ranh giới nhỏ/lớn = MaxNumber/2 (27, 22, 17).
Backtest kiểm định **trúng giải** theo từng kỳ, không dùng số chính trùng trung bình để kết luận về giải thưởng.
Bonferroni tính cả các so sánh từng vé/nhóm và các game đã chọn; CI ghép cặp t 95% chưa hiệu chỉnh, khoảng tỷ lệ nhóm dùng Hoeffding theo số kỳ.
Lặp RNG nhiều lần chỉ giảm nhiễu mô phỏng, không bổ sung kỳ thật. Các kết quả lịch sử vẫn mang tính thăm dò.
`--from-draw` có ở cả `backtest` và `report`: chọn một game, chốt mốc/thuật toán/mục tiêu **trước khi xem dữ liệu kiểm tra**,
sau đó thu thập các kỳ mới. Đổi mốc hoặc thông số sau khi xem kết quả không tạo bằng chứng ngoài mẫu.

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
