# Gợi ý Vietlott — 19/11/2026

> **Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng.**
> Mọi bộ số — kể cả bộ ngẫu nhiên — có cùng xác suất trúng. Bộ gợi ý chỉ phản ánh đặc điểm dữ liệu quá khứ.

Tạo báo cáo lúc 22:59 19/11/2026 (giờ VN). Mỗi cách chọn là một phương án mua riêng.

## Lotto 5/35

- Kỳ mới nhất: **#01018** 19/11/2026 — `19 28 31 32 34 | 04`
- Lịch sử dùng để gợi ý: 1018 kỳ (#00001 → #01018)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 14,29 % số kỳ; thực tế từ 11,79 % (số 12) đến 16,90 % (số 10).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1018 kỳ: p = 0.0076 → có dấu hiệu lệch tần suất ở ngưỡng 5% chưa hiệu chỉnh nhiều phép thử.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 18/11/2026 → kỳ #01016 (18/11/2026): `07 19 22 23 28 | 08`

| Chiến lược | Bộ số | ĐB | Trùng | Kết quả |
|---|---|---|---|---|
| hot | 10 20 **22** 26 33 | 05 | 1/5 | - |
| hot | 10 11 16 20 26 | 11 | 0/5 | - |
| cold | 03 16 21 24 31 | 10 | 0/5 | - |
| cold | 03 05 21 24 31 | 11 | 0/5 | - |
| balanced | 09 14 **22** 25 35 | 03 | 1/5 | - |
| balanced | 05 16 20 21 27 | **08** ✓ | 0/5 + ĐB | 🎉 Khuyến khích |
| random | 02 04 11 15 **22** | 05 | 1/5 | - |
| random | 01 **19** 26 32 34 | 01 | 1/5 | - |
| cover | 04 14 25 33 34 | 11 | 0/5 | - |
| cover | 02 05 15 **19** 26 | 12 | 1/5 | - |
| cover | 10 12 13 **23** 29 | 09 | 1/5 | - |
| cover | 01 08 09 **22** 30 | 07 | 1/5 | - |
| cover | 03 **07** 11 17 31 | 04 | 1/5 | - |
| cover | 06 20 21 27 35 | 06 | 0/5 | - |
| cover | 16 18 24 **28** 32 | 05 | 1/5 | - |
| cover | 11 **23** 24 26 30 | 03 | 1/5 | - |

Kỳ này: 10 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 11.4), 1 bộ trúng giải (kỳ vọng 1.5).

<details><summary>Thành tích thực tế cộng dồn (10 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng | Trùng ĐB |
|---|---:|---:|---:|---:|---:|---:|
| balanced | 20 | 0.650 | 0.714 | 4 | 1.9 | 4/20 |
| cold | 20 | 0.600 | 0.714 | 1 | 1.9 | 1/20 |
| cover | 80 | 0.725 | 0.714 | 6 | 7.7 | 6/80 |
| hot | 20 | 0.950 | 0.714 | 2 | 1.9 | 2/20 |
| random | 20 | 0.700 | 0.714 | 1 | 1.9 | 0/20 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01019**, bắt đầu 13:00 20/11/2026 (giờ VN).
- Bộ số tạo lúc 22:59:18 19/11/2026 · seed `20261119` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **45,71 %**, tức khoảng 1 trong 2 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 39,64 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 9,60 %.

| Vé | Bộ số | ĐB | Mua từ vé 1 đến vé này: có ≥1 vé trúng | …có ≥1 vé trùng ≥3 số | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---|---:|---:|---:|
| cover 1 | `04 05 06 21 27` | `05` | **9,60 %** | 1,39 % | 9,60 % |
| cover 2 | `07 20 23 24 28` | `09` | **18,98 %** | 2,77 % | 18,29 % |
| cover 3 | `10 14 25 26 32` | `12` | **28,12 %** | 4,16 % | 26,13 % |
| cover 4 | `15 18 19 33 35` | `01` | **37,03 %** | 5,55 % | 33,23 % |
| cover 5 | `03 09 12 16 22` | `04` | **45,71 %** | 6,93 % | 39,64 % |
| cover 6 | `08 13 29 30 31` | `10` | **54,16 %** | 8,32 % | 45,44 % |
| cover 7 | `01 02 11 17 34` | `03` | **62,38 %** | 9,71 % | 50,68 % |
| cover 8 | `02 08 10 21 28` | `07` | **70,35 %** | 11,04 % | 55,42 % |

- Với Lotto 5/35: giải Khuyến khích (0–2 số + ĐB) chiếm 86 % số lần trúng và chỉ cần trùng số ĐB (01–12), nên số ĐB khác nhau là lợi thế chính: 5 vé phủ 5/12 khả năng của số ĐB; 12 vé có đủ 12 số ĐB thì kỳ nào cũng có vé trúng, nhưng vẫn có thể lỗ tiền.
- Độc đắc (5 số + ĐB): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 486.948, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 998 kỳ đã quay (#00021 → #01018) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 70,64 % (khoảng 7 trong 10 kỳ)**
- random → 55,42 % (khoảng 6 trong 10 kỳ)
- hot → 34,40 % (khoảng 1 trong 3 kỳ)
- cold → 36,71 % (khoảng 1 trong 3 kỳ)
- balanced → 55,68 % (khoảng 6 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 14.051đ với phủ số và 14.392đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | ĐB | Chẵn/Lẻ | Nhỏ/Lớn (≤17) | Tổng |
|---|---|---|---|---|---:|
| hot | `04 20 22 28 29` | `05` | 4/1 | 1/4 | 103 |
| hot | `04 16 25 33 34` | `10` | 3/2 | 2/3 | 112 |
| cold | `02 03 05 11 18` | `06` | 2/3 | 4/1 | 39 |
| cold | `02 03 04 05 15` | `07` | 2/3 | 5/0 | 29 |
| balanced | `09 10 17 20 33` | `08` | 2/3 | 3/2 | 89 |
| balanced | `05 15 18 19 32` | `01` | 2/3 | 2/3 | 89 |
| random (đối chứng) | `02 05 17 18 29` | `03` | 2/3 | 3/2 | 71 |
| random (đối chứng) | `14 23 24 30 34` | `03` | 4/1 | 1/4 | 125 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **9,60 %** · trúng ≥3 số **1,39 %** · Độc đắc **0,0000257 %** (1 / 3.895.584).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 |
|---|---:|---:|---:|---:|---:|---:|
| Xác suất | 43,90 % | 42,21 % | 12,51 % | 1,34 % | 0,0462 % | 0,000308 % |

Số ĐB trùng: 8,33 % (độc lập với số chính).

<details><summary>Backtest walk-forward</summary>

**Từng vé: 998 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.715 | 9,59 % | - | - | - |
| hot | 0.711 | 9,06 % | -0.523 | -1.259 đến +0.212 | 0.1629 |
| cold | 0.722 | 9,61 % | +0.029 | -0.718 đến +0.776 | 0.9391 |
| balanced | 0.714 | 9,62 % | +0.031 | -0.114 đến +0.177 | 0.6729 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 70,64 % | 66,34 % đến 74,93 % | +15.217 | +14.515 đến +15.918 | 1.872E-226 |
| random | 55,42 % | 51,12 % đến 59,72 % | - | - | - |
| hot | 34,40 % | 30,10 % đến 38,70 % | -21.020 | -23.572 đến -18.469 | 2.252E-52 |
| cold | 36,71 % | 32,41 % đến 41,00 % | -18.713 | -21.268 đến -16.158 | 1.046E-42 |
| balanced | 55,68 % | 51,39 % đến 59,98 % | +0.265 | -0.443 đến +0.974 | 0.4623 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 14.051đ | -65.949đ | -82,44% | 1,26 % | 1,26 % |
| random | 14.392đ | -65.608đ | -82,01% | 1,30 % | 1,28 % |
| hot | 13.136đ | -66.864đ | -83,58% | 1,20 % | 1,12 % |
| cold | 14.215đ | -65.785đ | -82,23% | 1,43 % | 1,36 % |
| balanced | 13.243đ | -66.757đ | -83,45% | 1,31 % | 1,27 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 998 kỳ: 79.840.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01018 | 19/11/2026 | `19 28 31 32 34 | 04` | - |
| #01017 | 19/11/2026 | `08 10 14 17 30 | 10` | - |
| #01016 | 18/11/2026 | `07 19 22 23 28 | 08` | - |
| #01015 | 18/11/2026 | `01 13 25 28 29 | 08` | - |
| #01014 | 17/11/2026 | `08 09 12 27 30 | 05` | - |

</details>

## Mega 6/45

- Kỳ mới nhất: **#01590** 18/11/2026 — `06 18 20 27 29 32`
- Lịch sử dùng để gợi ý: 1590 kỳ (#00001 → #01590)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 13,33 % số kỳ; thực tế từ 11,64 % (số 38) đến 14,59 % (số 44).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1590 kỳ: p = 0.9190 → chưa bác bỏ giả thuyết tần suất đều.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 15/11/2026 → kỳ #01590 (18/11/2026): `06 18 20 27 29 32`

| Chiến lược | Bộ số | Trùng | Kết quả |
|---|---|---|---|
| hot | 01 10 22 25 41 43 | 0/6 | - |
| hot | 01 10 25 40 41 43 | 0/6 | - |
| cold | 04 09 12 21 **29** 34 | 1/6 | - |
| cold | 04 21 28 35 41 42 | 0/6 | - |
| balanced | 03 16 22 26 **27** 33 | 1/6 | - |
| balanced | 05 19 **20** 26 **32** 39 | 2/6 | - |
| random | 03 05 24 38 43 44 | 0/6 | - |
| random | **06** 07 08 09 11 44 | 1/6 | - |
| cover | 05 07 23 **32** 44 45 | 1/6 | - |
| cover | 16 21 26 31 40 41 | 0/6 | - |
| cover | 04 08 12 15 33 34 | 0/6 | - |
| cover | 02 **06** 13 17 24 30 | 1/6 | - |
| cover | 01 09 **18** 25 28 42 | 1/6 | - |
| cover | 10 **20** 22 36 38 43 | 1/6 | - |
| cover | 03 11 19 **29** 35 37 | 1/6 | - |
| cover | 14 **20** 26 **27** **29** 39 | 3/6 | 🎉 Giải Ba |

Kỳ này: 13 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 12.8), 1 bộ trúng giải (kỳ vọng 0.4).

<details><summary>Thành tích thực tế cộng dồn (4 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng |
|---|---:|---:|---:|---:|---:|
| balanced | 8 | 0.750 | 0.800 | 0 | 0.2 |
| cold | 8 | 0.875 | 0.800 | 0 | 0.2 |
| cover | 32 | 0.844 | 0.800 | 1 | 0.8 |
| hot | 8 | 0.375 | 0.800 | 0 | 0.2 |
| random | 8 | 1.000 | 0.800 | 1 | 0.2 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01591**, bắt đầu 18:00 20/11/2026 (giờ VN).
- Bộ số tạo lúc 22:59:18 19/11/2026 · seed `20261119` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **11,87 %**, tức khoảng 1 trong 8 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 11,36 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 2,38 %.

| Vé | Bộ số | Mua từ vé 1 đến vé này: có ≥1 vé trúng | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---:|---:|
| cover 1 | `15 16 19 27 34 43` | **2,38 %** | 2,38 % |
| cover 2 | `02 20 22 25 41 44` | **4,76 %** | 4,71 % |
| cover 3 | `17 18 30 37 42 45` | **7,14 %** | 6,98 % |
| cover 4 | `01 05 32 35 38 40` | **9,50 %** | 9,20 % |
| cover 5 | `13 14 21 29 31 33` | **11,87 %** | 11,36 % |
| cover 6 | `03 10 11 24 28 39` | **14,23 %** | 13,47 % |
| cover 7 | `04 06 07 08 09 23` | **16,58 %** | 15,54 % |
| cover 8 | `09 12 26 36 43 45` | **18,81 %** | 17,55 % |

- Với Mega 6/45: vé không có số ĐB và 94 % số lần trúng là Giải Ba (3 số). So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.
- Jackpot (6 số): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 1.018.132, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 1.570 kỳ đã quay (#00021 → #01590) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 18,76 % (khoảng 1 trong 5 kỳ)**
- random → 17,66 % (khoảng 1 trong 6 kỳ)
- hot → 14,28 % (khoảng 1 trong 7 kỳ)
- cold → 13,61 % (khoảng 1 trong 7 kỳ)
- balanced → 17,68 % (khoảng 1 trong 6 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 11.282đ với phủ số và 10.970đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | Chẵn/Lẻ | Nhỏ/Lớn (≤22) | Tổng |
|---|---|---|---|---:|
| hot | `01 09 15 20 31 41` | 1/5 | 4/2 | 117 |
| hot | `06 09 14 25 40 44` | 4/2 | 3/3 | 138 |
| cold | `08 12 17 30 41 45` | 3/3 | 3/3 | 153 |
| cold | `04 08 35 38 39 41` | 3/3 | 2/4 | 165 |
| balanced | `02 06 14 23 43 45` | 3/3 | 3/3 | 133 |
| balanced | `06 07 18 35 38 39` | 3/3 | 3/3 | 143 |
| random (đối chứng) | `01 07 27 29 40 43` | 1/5 | 2/4 | 147 |
| random (đối chứng) | `03 11 16 24 26 44` | 4/2 | 3/3 | 124 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **2,38 %** · trúng ≥3 số **2,38 %** · Jackpot **0,0000123 %** (1 / 8.145.060).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 | 6 |
|---|---:|---:|---:|---:|---:|---:|---:|
| Xác suất | 40,06 % | 42,41 % | 15,15 % | 2,24 % | 0,136 % | 0,00287 % | 0,0000123 % |

<details><summary>Backtest walk-forward</summary>

**Từng vé: 1570 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.801 | 2,40 % | - | - | - |
| hot | 0.799 | 2,34 % | -0.059 | -0.258 đến +0.141 | 0.5641 |
| cold | 0.790 | 2,27 % | -0.133 | -0.335 đến +0.068 | 0.1943 |
| balanced | 0.800 | 2,39 % | -0.015 | -0.078 đến +0.049 | 0.6521 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 18,76 % | 15,33 % đến 22,18 % | +1.100 | +0.660 đến +1.540 | 1.063E-06 |
| random | 17,66 % | 14,23 % đến 21,08 % | - | - | - |
| hot | 14,28 % | 10,85 % đến 17,71 % | -3.374 | -4.445 đến -2.303 | 8.213E-10 |
| cold | 13,61 % | 10,18 % đến 17,04 % | -4.045 | -5.116 đến -2.975 | 2.05E-13 |
| balanced | 17,68 % | 14,25 % đến 21,10 % | +0.021 | -0.435 đến +0.476 | 0.9292 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 11.282đ | -68.718đ | -85,90% | 1,14 % | 1,14 % |
| random | 10.970đ | -69.030đ | -86,29% | 1,11 % | 1,11 % |
| hot | 10.846đ | -69.154đ | -86,44% | 1,60 % | 1,60 % |
| cold | 9.984đ | -70.016đ | -87,52% | 1,43 % | 1,43 % |
| balanced | 10.453đ | -69.547đ | -86,93% | 1,22 % | 1,22 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 1570 kỳ: 125.600.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01590 | 18/11/2026 | `06 18 20 27 29 32` | 15.755.539.500đ |
| #01589 | 15/11/2026 | `06 22 24 26 40 44` | 13.789.728.500đ |
| #01588 | 13/11/2026 | `01 07 15 19 31 33` | 19.775.734.000đ |
| #01587 | 11/11/2026 | `03 11 14 23 25 43` | 17.638.002.500đ |
| #01586 | 08/11/2026 | `01 02 14 20 23 36` | 15.740.836.000đ |

</details>

## Power 6/55

- Kỳ mới nhất: **#01426** 19/11/2026 — `03 13 21 23 38 43 | 16`
- Lịch sử dùng để gợi ý: 1426 kỳ (#00001 → #01426)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 10,91 % số kỳ; thực tế từ 8,84 % (số 04) đến 12,69 % (số 22).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1426 kỳ: p = 0.3812 → chưa bác bỏ giả thuyết tần suất đều.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 18/11/2026 → kỳ #01426 (19/11/2026): `03 13 21 23 38 43 | 16`

| Chiến lược | Bộ số | Trùng | Kết quả |
|---|---|---|---|
| hot | 05 11 18 28 31 **38** | 1/6 | - |
| hot | 20 31 34 **38** 41 52 | 1/6 | - |
| cold | **03** 07 08 11 **13** **23** | 3/6 | 🎉 Giải Ba |
| cold | **03** 08 14 16 30 50 | 1/6 + ĐB | - |
| balanced | 01 02 **21** 40 42 53 | 1/6 | - |
| balanced | 09 18 19 28 30 49 | 0/6 | - |
| random | 05 **13** 26 49 51 54 | 1/6 | - |
| random | 05 14 19 20 **38** 51 | 1/6 | - |
| cover | **03** 06 14 **43** 45 47 | 2/6 | - |
| cover | 08 17 22 **23** 33 37 | 1/6 | - |
| cover | 16 **21** 29 39 48 53 | 1/6 + ĐB | - |
| cover | 09 10 41 42 44 46 | 0/6 | - |
| cover | 04 07 11 25 27 51 | 0/6 | - |
| cover | 02 05 15 36 40 49 | 0/6 | - |
| cover | 19 26 34 **38** 50 55 | 1/6 | - |
| cover | 12 **13** 20 30 32 52 | 1/6 | - |

Kỳ này: 15 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 10.5), 1 bộ trúng giải (kỳ vọng 0.2).

<details><summary>Thành tích thực tế cộng dồn (5 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng |
|---|---:|---:|---:|---:|---:|
| balanced | 10 | 0.900 | 0.655 | 1 | 0.1 |
| cold | 10 | 0.900 | 0.655 | 1 | 0.1 |
| cover | 40 | 0.700 | 0.655 | 1 | 0.5 |
| hot | 10 | 0.300 | 0.655 | 0 | 0.1 |
| random | 10 | 0.800 | 0.655 | 0 | 0.1 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01427**, bắt đầu 18:00 21/11/2026 (giờ VN).
- Bộ số tạo lúc 22:59:18 19/11/2026 · seed `20261119` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **6,65 %**, tức khoảng 1 trong 15 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 6,49 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 1,33 %.

| Vé | Bộ số | Mua từ vé 1 đến vé này: có ≥1 vé trúng | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---:|---:|
| cover 1 | `03 15 42 45 46 49` | **1,33 %** | 1,33 % |
| cover 2 | `20 23 25 31 39 50` | **2,66 %** | 2,65 % |
| cover 3 | `04 13 26 29 30 40` | **3,99 %** | 3,95 % |
| cover 4 | `09 17 18 21 33 35` | **5,32 %** | 5,23 % |
| cover 5 | `11 12 34 36 43 47` | **6,65 %** | 6,49 % |
| cover 6 | `01 10 28 38 48 53` | **7,98 %** | 7,74 % |
| cover 7 | `05 07 19 37 54 55` | **9,30 %** | 8,97 % |
| cover 8 | `02 06 08 14 27 52` | **10,62 %** | 10,18 % |

- Với Power 6/55: số ĐB là quả thứ 7 quay cùng lồng, chỉ dùng cho giải 5 số + ĐB, nên không có giải nào chỉ cần trùng ĐB; 95 % xác suất trúng giải của một vé thuộc Giải Ba (3 số). So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.
- Jackpot 1 (6 số): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 3.623.709, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 1.406 kỳ đã quay (#00021 → #01426) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 10,64 % (khoảng 1 trong 9 kỳ)**
- random → 10,23 % (khoảng 1 trong 10 kỳ)
- hot → 8,49 % (khoảng 1 trong 12 kỳ)
- cold → 9,07 % (khoảng 1 trong 11 kỳ)
- balanced → 9,99 % (khoảng 1 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 9.803đ với phủ số và 8.562đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | Chẵn/Lẻ | Nhỏ/Lớn (≤27) | Tổng |
|---|---|---|---|---:|
| hot | `07 13 20 21 36 38` | 3/3 | 4/2 | 135 |
| hot | `01 06 18 20 28 45` | 4/2 | 4/2 | 118 |
| cold | `04 07 08 09 27 37` | 2/4 | 5/1 | 92 |
| cold | `11 15 16 32 40 48` | 4/2 | 3/3 | 162 |
| balanced | `12 13 24 35 43 52` | 3/3 | 3/3 | 179 |
| balanced | `02 13 17 34 41 42` | 3/3 | 3/3 | 149 |
| random (đối chứng) | `35 36 48 49 51 52` | 3/3 | 0/6 | 271 |
| random (đối chứng) | `03 17 27 28 45 50` | 2/4 | 3/3 | 170 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **1,33 %** · trúng ≥3 số **1,33 %** · Jackpot 1 **0,00000345 %** (1 / 28.989.675).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 | 6 |
|---|---:|---:|---:|---:|---:|---:|---:|
| Xác suất | 48,24 % | 39,47 % | 10,96 % | 1,27 % | 0,0608 % | 0,00101 % | 0,00000345 % |

<details><summary>Backtest walk-forward</summary>

**Từng vé: 1406 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.654 | 1,31 % | - | - | - |
| hot | 0.655 | 1,35 % | +0.043 | -0.114 đến +0.200 | 0.5922 |
| cold | 0.660 | 1,42 % | +0.103 | -0.059 đến +0.265 | 0.2115 |
| balanced | 0.656 | 1,34 % | +0.028 | -0.024 đến +0.080 | 0.2962 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 10,64 % | 7,02 % đến 14,26 % | +0.413 | +0.039 đến +0.787 | 0.03041 |
| random | 10,23 % | 6,61 % đến 13,85 % | - | - | - |
| hot | 8,49 % | 4,86 % đến 12,11 % | -1.742 | -2.628 đến -0.855 | 0.0001224 |
| cold | 9,07 % | 5,44 % đến 12,69 % | -1.163 | -2.108 đến -0.218 | 0.01594 |
| balanced | 9,99 % | 6,37 % đến 13,62 % | -0.235 | -0.608 đến +0.139 | 0.2183 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 9.803đ | -70.197đ | -87,75% | 0,517 % | 0,517 % |
| random | 8.562đ | -71.438đ | -89,30% | 0,967 % | 0,967 % |
| hot | 12.155đ | -67.845đ | -84,81% | 2,07 % | 2,07 % |
| cold | 14.408đ | -65.592đ | -81,99% | 2,25 % | 2,25 % |
| balanced | 11.267đ | -68.733đ | -85,92% | 0,934 % | 0,934 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Chưa phát hiện cách chọn tăng tỷ lệ trúng giải so với random ở ngưỡng đã hiệu chỉnh.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 1406 kỳ: 112.480.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01426 | 19/11/2026 | `03 13 21 23 38 43 | 16` | 42.887.095.500đ |
| #01425 | 17/11/2026 | `06 20 36 41 49 52 | 34` | 40.971.513.000đ |
| #01424 | 14/11/2026 | `05 28 36 42 45 47 | 51` | 37.641.706.500đ |
| #01423 | 12/11/2026 | `17 25 33 34 35 36 | 48` | 35.115.629.250đ |
| #01422 | 10/11/2026 | `01 02 21 38 39 51 | 12` | 32.688.020.100đ |

</details>

