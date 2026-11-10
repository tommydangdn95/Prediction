# Gợi ý Vietlott — 10/11/2026

> **Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng.**
> Mọi bộ số — kể cả bộ ngẫu nhiên — có cùng xác suất trúng. Bộ gợi ý chỉ phản ánh đặc điểm dữ liệu quá khứ.

Tạo báo cáo lúc 18:51 10/11/2026 (giờ VN). Mỗi cách chọn là một phương án mua riêng.

## Lotto 5/35

- Kỳ mới nhất: **#00999** 10/11/2026 — `01 05 18 26 33 | 07`
- Lịch sử dùng để gợi ý: 999 kỳ (#00001 → #00999)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 14,29 % số kỳ; thực tế từ 11,71 % (số 12) đến 16,72 % (số 22).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 999 kỳ: p = 0.0181 → có dấu hiệu lệch tần suất ở ngưỡng 5% chưa hiệu chỉnh nhiều phép thử.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

_Kiểm tra gợi ý cũ: chưa có dự đoán đã lưu nào có kết quả. Mục này hiện ra sau khi kỳ mục tiêu của một dự đoán đã lưu (thư mục `predictions/ledger/`) được quay._

- Kỳ mục tiêu: **#01000**, bắt đầu 21:00 10/11/2026 (giờ VN).
- Bộ số tạo lúc 15:00:35 10/11/2026 · seed `20261110` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **45,71 %**, tức khoảng 1 trong 2 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 39,64 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 9,60 %.

| Vé | Bộ số | ĐB | Mua từ vé 1 đến vé này: có ≥1 vé trúng | …có ≥1 vé trùng ≥3 số | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---|---:|---:|---:|
| cover 1 | `01 14 23 24 26` | `01` | **9,60 %** | 1,39 % | 9,60 % |
| cover 2 | `02 09 10 22 32` | `12` | **18,98 %** | 2,77 % | 18,29 % |
| cover 3 | `04 06 07 13 20` | `10` | **28,12 %** | 4,16 % | 26,13 % |
| cover 4 | `12 18 21 25 35` | `07` | **37,03 %** | 5,55 % | 33,23 % |
| cover 5 | `03 08 15 19 29` | `05` | **45,71 %** | 6,93 % | 39,64 % |
| cover 6 | `05 17 30 33 34` | `04` | **54,16 %** | 8,32 % | 45,44 % |
| cover 7 | `11 16 27 28 31` | `03` | **62,38 %** | 9,71 % | 50,68 % |
| cover 8 | `11 12 19 20 23` | `06` | **70,35 %** | 11,04 % | 55,42 % |

- Với Lotto 5/35: giải Khuyến khích (0–2 số + ĐB) chiếm 86 % số lần trúng và chỉ cần trùng số ĐB (01–12), nên số ĐB khác nhau là lợi thế chính: 5 vé phủ 5/12 khả năng của số ĐB; 12 vé có đủ 12 số ĐB thì kỳ nào cũng có vé trúng, nhưng vẫn có thể lỗ tiền.
- Độc đắc (5 số + ĐB): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 486.948, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 979 kỳ đã quay (#00021 → #00999) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 70,28 % (khoảng 7 trong 10 kỳ)**
- random → 55,30 % (khoảng 6 trong 10 kỳ)
- hot → 34,47 % (khoảng 1 trong 3 kỳ)
- cold → 36,36 % (khoảng 1 trong 3 kỳ)
- balanced → 55,54 % (khoảng 6 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 13.607đ với phủ số và 14.380đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | ĐB | Chẵn/Lẻ | Nhỏ/Lớn (≤17) | Tổng |
|---|---|---|---|---|---:|
| hot | `01 25 29 33 35` | `05` | 0/5 | 1/4 | 123 |
| hot | `10 20 22 26 29` | `01` | 4/1 | 1/4 | 107 |
| cold | `03 08 10 14 28` | `12` | 4/1 | 4/1 | 63 |
| cold | `07 08 19 24 32` | `03` | 3/2 | 2/3 | 90 |
| balanced | `01 02 26 33 34` | `05` | 3/2 | 2/3 | 96 |
| balanced | `06 12 16 21 25` | `04` | 3/2 | 3/2 | 80 |
| random (đối chứng) | `12 15 22 29 35` | `09` | 2/3 | 2/3 | 113 |
| random (đối chứng) | `03 07 09 27 30` | `10` | 1/4 | 3/2 | 76 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **9,60 %** · trúng ≥3 số **1,39 %** · Độc đắc **0,0000257 %** (1 / 3.895.584).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 |
|---|---:|---:|---:|---:|---:|---:|
| Xác suất | 43,90 % | 42,21 % | 12,51 % | 1,34 % | 0,0462 % | 0,000308 % |

Số ĐB trùng: 8,33 % (độc lập với số chính).

<details><summary>Backtest walk-forward</summary>

**Từng vé: 979 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.714 | 9,61 % | - | - | - |
| hot | 0.710 | 9,06 % | -0.553 | -1.298 đến +0.192 | 0.1452 |
| cold | 0.723 | 9,54 % | -0.071 | -0.818 đến +0.675 | 0.8516 |
| balanced | 0.714 | 9,64 % | +0.026 | -0.119 đến +0.170 | 0.7282 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 70,28 % | 65,94 % đến 74,62 % | +14.982 | +14.303 đến +15.661 | 1.765E-229 |
| random | 55,30 % | 50,96 % đến 59,64 % | - | - | - |
| hot | 34,47 % | 30,13 % đến 38,81 % | -20.827 | -23.393 đến -18.260 | 5.906E-51 |
| cold | 36,36 % | 32,02 % đến 40,70 % | -18.938 | -21.509 đến -16.367 | 4.638E-43 |
| balanced | 55,54 % | 51,20 % đến 59,89 % | +0.248 | -0.427 đến +0.924 | 0.4705 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 13.607đ | -66.393đ | -82,99% | 1,35 % | 1,35 % |
| random | 14.380đ | -65.620đ | -82,03% | 1,32 % | 1,28 % |
| hot | 12.331đ | -67.669đ | -84,59% | 1,35 % | 1,28 % |
| cold | 14.648đ | -65.352đ | -81,69% | 1,48 % | 1,39 % |
| balanced | 14.614đ | -65.386đ | -81,73% | 1,28 % | 1,26 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 979 kỳ: 78.320.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

</details>

<details><summary>Xác suất thật mỗi vé</summary>

| Giải | Điều kiện | Xác suất / vé | % |
|---|---|---:|---:|
| Độc đắc | 5 số + ĐB | 1 / 3.895.584 | 0,0000257 % |
| Giải Nhất | 5 số | 1 / 354.144 | 0,000282 % |
| Giải Nhì | 4 số + ĐB | 1 / 25.971 | 0,00385 % |
| Giải Ba | 4 số | 1 / 2.361 | 0,0424 % |
| Giải Tư | 3 số + ĐB | 1 / 896 | 0,112 % |
| Giải Năm | 3 số | 1 / 81 | 1,23 % |
| Khuyến khích | 0–2 số + ĐB | 1 / 12 | 8,22 % |
| _Trúng giải bất kỳ_ | | 1 / 10,4 | 9,60 % |

</details>

<details><summary>5 kỳ gần nhất</summary>

| Kỳ | Ngày | Kết quả | Jackpot 1 |
|---|---|---|---:|
| #00999 | 10/11/2026 | `01 05 18 26 33 | 07` | - |
| #00998 | 09/11/2026 | `06 15 22 27 31 | 01` | - |
| #00997 | 09/11/2026 | `04 16 26 27 29 | 06` | - |
| #00996 | 08/11/2026 | `04 11 17 26 35 | 11` | - |
| #00995 | 08/11/2026 | `02 13 16 18 22 | 05` | - |

</details>

## Mega 6/45

- Kỳ mới nhất: **#01586** 08/11/2026 — `01 02 14 20 23 36`
- Lịch sử dùng để gợi ý: 1586 kỳ (#00001 → #01586)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 13,33 % số kỳ; thực tế từ 11,66 % (số 38) đến 14,56 % (số 37).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1586 kỳ: p = 0.9256 → chưa bác bỏ giả thuyết tần suất đều.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

_Kiểm tra gợi ý cũ: chưa có dự đoán đã lưu nào có kết quả. Mục này hiện ra sau khi kỳ mục tiêu của một dự đoán đã lưu (thư mục `predictions/ledger/`) được quay._

- Kỳ mục tiêu: **#01587**, bắt đầu 18:00 11/11/2026 (giờ VN).
- Bộ số tạo lúc 07:36:03 10/11/2026 · seed `20261110` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **11,87 %**, tức khoảng 1 trong 8 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 11,36 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 2,38 %.

| Vé | Bộ số | Mua từ vé 1 đến vé này: có ≥1 vé trúng | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---:|---:|
| cover 1 | `13 27 29 37 38 39` | **2,38 %** | 2,38 % |
| cover 2 | `01 02 07 18 32 44` | **4,76 %** | 4,71 % |
| cover 3 | `09 15 19 24 30 43` | **7,14 %** | 6,98 % |
| cover 4 | `03 17 20 31 33 36` | **9,50 %** | 9,20 % |
| cover 5 | `10 11 12 16 22 35` | **11,87 %** | 11,36 % |
| cover 6 | `04 06 08 14 21 40` | **14,23 %** | 13,47 % |
| cover 7 | `05 25 26 41 42 45` | **16,58 %** | 15,54 % |
| cover 8 | `23 24 25 28 34 40` | **18,81 %** | 17,55 % |

- Với Mega 6/45: vé không có số ĐB và 94 % số lần trúng là Giải Ba (3 số). So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.
- Jackpot (6 số): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 1.018.132, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 1.566 kỳ đã quay (#00021 → #01586) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 19,01 % (khoảng 1 trong 5 kỳ)**
- random → 17,70 % (khoảng 1 trong 6 kỳ)
- hot → 14,37 % (khoảng 1 trong 7 kỳ)
- cold → 13,70 % (khoảng 1 trong 7 kỳ)
- balanced → 17,60 % (khoảng 1 trong 6 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 11.086đ với phủ số và 11.448đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | Chẵn/Lẻ | Nhỏ/Lớn (≤22) | Tổng |
|---|---|---|---|---:|
| hot | `02 09 20 34 38 40` | 5/1 | 3/3 | 143 |
| hot | `11 14 33 34 36 40` | 4/2 | 2/4 | 168 |
| cold | `06 12 16 19 29 44` | 4/2 | 4/2 | 126 |
| cold | `06 08 12 16 32 43` | 5/1 | 4/2 | 117 |
| balanced | `14 18 19 30 31 39` | 3/3 | 3/3 | 151 |
| balanced | `05 08 20 28 29 35` | 3/3 | 3/3 | 125 |
| random (đối chứng) | `06 12 14 15 31 45` | 3/3 | 4/2 | 123 |
| random (đối chứng) | `01 03 08 13 20 41` | 2/4 | 5/1 | 86 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **2,38 %** · trúng ≥3 số **2,38 %** · Jackpot **0,0000123 %** (1 / 8.145.060).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 | 6 |
|---|---:|---:|---:|---:|---:|---:|---:|
| Xác suất | 40,06 % | 42,41 % | 15,15 % | 2,24 % | 0,136 % | 0,00287 % | 0,0000123 % |

<details><summary>Backtest walk-forward</summary>

**Từng vé: 1566 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.800 | 2,36 % | - | - | - |
| hot | 0.800 | 2,34 % | -0.012 | -0.212 đến +0.188 | 0.9037 |
| cold | 0.788 | 2,21 % | -0.146 | -0.344 đến +0.051 | 0.1461 |
| balanced | 0.801 | 2,37 % | +0.013 | -0.052 đến +0.079 | 0.6887 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 19,01 % | 15,57 % đến 22,44 % | +1.306 | +0.855 đến +1.758 | 1.66E-08 |
| random | 17,70 % | 14,27 % đến 21,13 % | - | - | - |
| hot | 14,37 % | 10,94 % đến 17,80 % | -3.331 | -4.398 đến -2.263 | 1.18E-09 |
| cold | 13,70 % | 10,27 % đến 17,13 % | -3.999 | -5.072 đến -2.926 | 4.218E-13 |
| balanced | 17,60 % | 14,16 % đến 21,03 % | -0.102 | -0.580 đến +0.376 | 0.6762 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 11.086đ | -68.914đ | -86,14% | 1,16 % | 1,16 % |
| random | 11.448đ | -68.552đ | -85,69% | 1,23 % | 1,23 % |
| hot | 9.639đ | -70.361đ | -87,95% | 1,58 % | 1,58 % |
| cold | 10.792đ | -69.208đ | -86,51% | 1,57 % | 1,57 % |
| balanced | 10.962đ | -69.038đ | -86,30% | 1,18 % | 1,18 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 1566 kỳ: 125.280.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

</details>

<details><summary>Xác suất thật mỗi vé</summary>

| Giải | Điều kiện | Xác suất / vé | % |
|---|---|---:|---:|
| Jackpot | 6 số | 1 / 8.145.060 | 0,0000123 % |
| Giải Nhất | 5 số | 1 / 34.808 | 0,00287 % |
| Giải Nhì | 4 số | 1 / 733 | 0,136 % |
| Giải Ba | 3 số | 1 / 45 | 2,24 % |
| _Trúng giải bất kỳ_ | | 1 / 42,0 | 2,38 % |

</details>

<details><summary>5 kỳ gần nhất</summary>

| Kỳ | Ngày | Kết quả | Jackpot 1 |
|---|---|---|---:|
| #01586 | 08/11/2026 | `01 02 14 20 23 36` | 15.740.836.000đ |
| #01585 | 06/11/2026 | `01 10 37 38 39 45` | 13.783.788.500đ |
| #01584 | 04/11/2026 | `07 14 20 30 34 40` | 26.087.005.500đ |
| #01583 | 01/11/2026 | `05 09 17 18 34 36` | 23.200.118.500đ |
| #01582 | 30/10/2026 | `07 21 22 40 41 45` | 20.852.928.000đ |

</details>

## Power 6/55

- Kỳ mới nhất: **#01422** 10/11/2026 — `01 02 21 38 39 51 | 12`
- Lịch sử dùng để gợi ý: 1422 kỳ (#00001 → #01422)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 10,91 % số kỳ; thực tế từ 8,86 % (số 04) đến 12,73 % (số 22).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1422 kỳ: p = 0.3655 → chưa bác bỏ giả thuyết tần suất đều.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 10/11/2026 → kỳ #01422 (10/11/2026): `01 02 21 38 39 51 | 12`

| Chiến lược | Bộ số | Trùng | Kết quả |
|---|---|---|---|
| hot | 06 14 18 20 23 24 | 0/6 | - |
| hot | 06 07 11 23 26 55 | 0/6 | - |
| cold | 11 17 **21** 40 47 **51** | 2/6 | - |
| cold | 03 11 16 32 47 **51** | 1/6 | - |
| balanced | 16 17 25 33 **38** 42 | 1/6 | - |
| balanced | 17 24 26 33 37 54 | 0/6 | - |
| random | 17 18 24 40 43 54 | 0/6 | - |
| random | **01** 08 10 26 28 31 | 1/6 | - |
| cover | 03 11 13 16 24 45 | 0/6 | - |
| cover | 04 12 20 33 34 36 | 0/6 + ĐB | - |
| cover | 06 10 19 **21** **38** 53 | 2/6 | - |
| cover | **01** 09 18 35 46 49 | 1/6 | - |
| cover | 31 37 41 47 50 52 | 0/6 | - |
| cover | 07 23 25 32 44 **51** | 1/6 | - |
| cover | **02** 27 28 29 **39** 40 | 2/6 | - |
| cover | 05 15 17 22 43 55 | 0/6 | - |

Kỳ này: 11 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 10.5), 0 bộ trúng giải (kỳ vọng 0.2).

<details><summary>Thành tích thực tế cộng dồn (1 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng |
|---|---:|---:|---:|---:|---:|
| balanced | 2 | 0.500 | 0.655 | 0 | 0.0 |
| cold | 2 | 1.500 | 0.655 | 0 | 0.0 |
| cover | 8 | 0.750 | 0.655 | 0 | 0.1 |
| hot | 2 | 0.000 | 0.655 | 0 | 0.0 |
| random | 2 | 0.500 | 0.655 | 0 | 0.0 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01423**, bắt đầu 18:00 12/11/2026 (giờ VN).
- Bộ số tạo lúc 18:51:12 10/11/2026 · seed `20261110` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **6,65 %**, tức khoảng 1 trong 15 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 6,49 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 1,33 %.

| Vé | Bộ số | Mua từ vé 1 đến vé này: có ≥1 vé trúng | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---:|---:|
| cover 1 | `03 11 13 16 24 45` | **1,33 %** | 1,33 % |
| cover 2 | `04 12 20 33 34 36` | **2,66 %** | 2,65 % |
| cover 3 | `06 10 19 21 38 53` | **3,99 %** | 3,95 % |
| cover 4 | `01 09 18 35 46 49` | **5,32 %** | 5,23 % |
| cover 5 | `31 37 41 47 50 52` | **6,65 %** | 6,49 % |
| cover 6 | `07 23 25 32 44 51` | **7,98 %** | 7,74 % |
| cover 7 | `02 27 28 29 39 40` | **9,30 %** | 8,97 % |
| cover 8 | `05 15 17 22 43 55` | **10,62 %** | 10,18 % |

- Với Power 6/55: số ĐB là quả thứ 7 quay cùng lồng, chỉ dùng cho giải 5 số + ĐB, nên không có giải nào chỉ cần trùng ĐB; 95 % xác suất trúng giải của một vé thuộc Giải Ba (3 số). So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.
- Jackpot 1 (6 số): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 3.623.709, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 1.402 kỳ đã quay (#00021 → #01422) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 10,38 % (khoảng 1 trong 10 kỳ)**
- random → 10,23 % (khoảng 1 trong 10 kỳ)
- hot → 8,70 % (khoảng 1 trong 11 kỳ)
- cold → 9,10 % (khoảng 1 trong 11 kỳ)
- balanced → 10,17 % (khoảng 1 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 7.215đ với phủ số và 12.319đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | Chẵn/Lẻ | Nhỏ/Lớn (≤27) | Tổng |
|---|---|---|---|---:|
| hot | `01 05 09 11 18 39` | 1/5 | 5/1 | 83 |
| hot | `05 07 09 23 27 55` | 0/6 | 5/1 | 126 |
| cold | `11 14 17 37 40 47` | 2/4 | 3/3 | 166 |
| cold | `03 11 13 14 32 47` | 2/4 | 4/2 | 120 |
| balanced | `16 17 25 33 38 42` | 3/3 | 3/3 | 171 |
| balanced | `17 24 26 33 37 54` | 3/3 | 3/3 | 191 |
| random (đối chứng) | `17 18 24 40 43 54` | 4/2 | 3/3 | 196 |
| random (đối chứng) | `01 08 10 26 28 31` | 4/2 | 4/2 | 104 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **1,33 %** · trúng ≥3 số **1,33 %** · Jackpot 1 **0,00000345 %** (1 / 28.989.675).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 | 6 |
|---|---:|---:|---:|---:|---:|---:|---:|
| Xác suất | 48,24 % | 39,47 % | 10,96 % | 1,27 % | 0,0608 % | 0,00101 % | 0,00000345 % |

<details><summary>Backtest walk-forward</summary>

**Từng vé: 1402 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.652 | 1,31 % | - | - | - |
| hot | 0.656 | 1,36 % | +0.052 | -0.109 đến +0.213 | 0.5269 |
| cold | 0.660 | 1,42 % | +0.114 | -0.044 đến +0.271 | 0.1567 |
| balanced | 0.655 | 1,34 % | +0.038 | -0.012 đến +0.087 | 0.1352 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 10,38 % | 6,75 % đến 14,01 % | +0.148 | -0.229 đến +0.526 | 0.4408 |
| random | 10,23 % | 6,60 % đến 13,86 % | - | - | - |
| hot | 8,70 % | 5,07 % đến 12,32 % | -1.534 | -2.450 đến -0.619 | 0.00104 |
| cold | 9,10 % | 5,48 % đến 12,73 % | -1.126 | -2.047 đến -0.204 | 0.0167 |
| balanced | 10,17 % | 6,55 % đến 13,80 % | -0.056 | -0.443 đến +0.331 | 0.7767 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 7.215đ | -72.785đ | -90,98% | 0,472 % | 0,472 % |
| random | 12.319đ | -67.681đ | -84,60% | 0,948 % | 0,948 % |
| hot | 10.709đ | -69.291đ | -86,61% | 2,06 % | 2,06 % |
| cold | 13.544đ | -66.456đ | -83,07% | 2,30 % | 2,30 % |
| balanced | 12.112đ | -67.888đ | -84,86% | 0,898 % | 0,898 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Chưa phát hiện cách chọn tăng tỷ lệ trúng giải so với random ở ngưỡng đã hiệu chỉnh.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 1402 kỳ: 112.160.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

</details>

<details><summary>Xác suất thật mỗi vé</summary>

| Giải | Điều kiện | Xác suất / vé | % |
|---|---|---:|---:|
| Jackpot 1 | 6 số | 1 / 28.989.675 | 0,00000345 % |
| Jackpot 2 | 5 số + ĐB | 1 / 4.831.612 | 0,0000207 % |
| Giải Nhất | 5 số | 1 / 100.659 | 0,000993 % |
| Giải Nhì | 4 số | 1 / 1.643 | 0,0608 % |
| Giải Ba | 3 số | 1 / 79 | 1,27 % |
| _Trúng giải bất kỳ_ | | 1 / 75,0 | 1,33 % |

</details>

<details><summary>5 kỳ gần nhất</summary>

| Kỳ | Ngày | Kết quả | Jackpot 1 |
|---|---|---|---:|
| #01422 | 10/11/2026 | `01 02 21 38 39 51 | 12` | 32.688.020.100đ |
| #01421 | 07/11/2026 | `05 19 20 26 44 53 | 39` | 22.636.747.050đ |
| #01420 | 05/11/2026 | `06 20 34 41 49 52 | 36` | 42.432.448.800đ |
| #01419 | 03/11/2026 | `10 22 31 39 43 54 | 14` | 39.296.824.950đ |
| #01418 | 31/10/2026 | `12 18 22 26 28 55 | 11` | 36.604.726.050đ |

</details>

