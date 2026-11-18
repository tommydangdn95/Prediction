# Gợi ý Vietlott — 18/11/2026

> **Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng.**
> Mọi bộ số — kể cả bộ ngẫu nhiên — có cùng xác suất trúng. Bộ gợi ý chỉ phản ánh đặc điểm dữ liệu quá khứ.

Tạo báo cáo lúc 14:12 18/11/2026 (giờ VN). Mỗi cách chọn là một phương án mua riêng.

## Lotto 5/35

- Kỳ mới nhất: **#01015** 18/11/2026 — `01 13 25 28 29 | 08`
- Lịch sử dùng để gợi ý: 1015 kỳ (#00001 → #01015)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 14,29 % số kỳ; thực tế từ 11,82 % (số 12) đến 16,85 % (số 10).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1015 kỳ: p = 0.0105 → có dấu hiệu lệch tần suất ở ngưỡng 5% chưa hiệu chỉnh nhiều phép thử.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 16/11/2026 → kỳ #01012 (16/11/2026): `10 11 15 23 34 | 08`

| Chiến lược | Bộ số | ĐB | Trùng | Kết quả |
|---|---|---|---|---|
| hot | 06 **10** 16 20 26 | 01 | 1/5 | - |
| hot | 04 **11** 20 26 27 | 05 | 1/5 | - |
| cold | 01 12 13 17 27 | 06 | 0/5 | - |
| cold | 01 17 27 30 31 | 07 | 0/5 | - |
| balanced | 01 08 21 25 32 | 12 | 0/5 | - |
| balanced | 01 02 21 26 35 | 01 | 0/5 | - |
| random | 01 07 14 27 33 | 12 | 0/5 | - |
| random | **10** 20 24 26 33 | 10 | 1/5 | - |
| cover | 01 07 13 **15** **23** | 06 | 2/5 | - |
| cover | 04 **11** 12 16 31 | 10 | 1/5 | - |
| cover | 06 17 30 33 **34** | 09 | 1/5 | - |
| cover | 03 08 18 24 27 | 11 | 0/5 | - |
| cover | 02 05 **10** 21 29 | 04 | 1/5 | - |
| cover | 14 22 26 28 32 | 07 | 0/5 | - |
| cover | 09 19 20 25 35 | 03 | 0/5 | - |
| cover | 16 22 24 25 **34** | 02 | 1/5 | - |

Kỳ này: 9 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 11.4), 0 bộ trúng giải (kỳ vọng 1.5).

<details><summary>Thành tích thực tế cộng dồn (9 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng | Trùng ĐB |
|---|---:|---:|---:|---:|---:|---:|
| balanced | 18 | 0.667 | 0.714 | 3 | 1.7 | 3/18 |
| cold | 18 | 0.667 | 0.714 | 1 | 1.7 | 1/18 |
| cover | 72 | 0.722 | 0.714 | 6 | 6.9 | 6/72 |
| hot | 18 | 1.000 | 0.714 | 2 | 1.7 | 2/18 |
| random | 18 | 0.667 | 0.714 | 1 | 1.7 | 0/18 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01016**, bắt đầu 21:00 18/11/2026 (giờ VN).
- Bộ số tạo lúc 14:12:11 18/11/2026 · seed `20261118` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **45,71 %**, tức khoảng 1 trong 2 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 39,64 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 9,60 %.

| Vé | Bộ số | ĐB | Mua từ vé 1 đến vé này: có ≥1 vé trúng | …có ≥1 vé trùng ≥3 số | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---|---:|---:|---:|
| cover 1 | `04 14 25 33 34` | `11` | **9,60 %** | 1,39 % | 9,60 % |
| cover 2 | `02 05 15 19 26` | `12` | **18,98 %** | 2,77 % | 18,29 % |
| cover 3 | `10 12 13 23 29` | `09` | **28,12 %** | 4,16 % | 26,13 % |
| cover 4 | `01 08 09 22 30` | `07` | **37,03 %** | 5,55 % | 33,23 % |
| cover 5 | `03 07 11 17 31` | `04` | **45,71 %** | 6,93 % | 39,64 % |
| cover 6 | `06 20 21 27 35` | `06` | **54,16 %** | 8,32 % | 45,44 % |
| cover 7 | `16 18 24 28 32` | `05` | **62,38 %** | 9,71 % | 50,68 % |
| cover 8 | `11 23 24 26 30` | `03` | **70,35 %** | 11,04 % | 55,42 % |

- Với Lotto 5/35: giải Khuyến khích (0–2 số + ĐB) chiếm 86 % số lần trúng và chỉ cần trùng số ĐB (01–12), nên số ĐB khác nhau là lợi thế chính: 5 vé phủ 5/12 khả năng của số ĐB; 12 vé có đủ 12 số ĐB thì kỳ nào cũng có vé trúng, nhưng vẫn có thể lỗ tiền.
- Độc đắc (5 số + ĐB): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 486.948, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 995 kỳ đã quay (#00021 → #01015) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 70,46 % (khoảng 7 trong 10 kỳ)**
- random → 55,39 % (khoảng 6 trong 10 kỳ)
- hot → 34,42 % (khoảng 1 trong 3 kỳ)
- cold → 36,77 % (khoảng 1 trong 3 kỳ)
- balanced → 55,56 % (khoảng 6 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 13.338đ với phủ số và 13.968đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | ĐB | Chẵn/Lẻ | Nhỏ/Lớn (≤17) | Tổng |
|---|---|---|---|---|---:|
| hot | `10 20 22 26 33` | `05` | 4/1 | 1/4 | 111 |
| hot | `10 11 16 20 26` | `11` | 4/1 | 3/2 | 83 |
| cold | `03 16 21 24 31` | `10` | 2/3 | 2/3 | 95 |
| cold | `03 05 21 24 31` | `11` | 1/4 | 2/3 | 84 |
| balanced | `09 14 22 25 35` | `03` | 2/3 | 2/3 | 105 |
| balanced | `05 16 20 21 27` | `08` | 2/3 | 2/3 | 89 |
| random (đối chứng) | `02 04 11 15 22` | `05` | 3/2 | 4/1 | 54 |
| random (đối chứng) | `01 19 26 32 34` | `01` | 3/2 | 1/4 | 112 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **9,60 %** · trúng ≥3 số **1,39 %** · Độc đắc **0,0000257 %** (1 / 3.895.584).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 |
|---|---:|---:|---:|---:|---:|---:|
| Xác suất | 43,90 % | 42,21 % | 12,51 % | 1,34 % | 0,0462 % | 0,000308 % |

Số ĐB trùng: 8,33 % (độc lập với số chính).

<details><summary>Backtest walk-forward</summary>

**Từng vé: 995 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.713 | 9,65 % | - | - | - |
| hot | 0.711 | 9,05 % | -0.598 | -1.333 đến +0.138 | 0.111 |
| cold | 0.724 | 9,65 % | +0.002 | -0.740 đến +0.744 | 0.9951 |
| balanced | 0.713 | 9,67 % | +0.020 | -0.133 đến +0.173 | 0.7964 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 70,46 % | 66,15 % đến 74,77 % | +15.070 | +14.371 đến +15.769 | 1.789E-224 |
| random | 55,39 % | 51,08 % đến 59,70 % | - | - | - |
| hot | 34,42 % | 30,12 % đến 38,73 % | -20.967 | -23.547 đến -18.387 | 3.744E-51 |
| cold | 36,77 % | 32,47 % đến 41,08 % | -18.617 | -21.180 đến -16.055 | 4.284E-42 |
| balanced | 55,56 % | 51,26 % đến 59,87 % | +0.174 | -0.550 đến +0.898 | 0.6375 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 13.338đ | -66.662đ | -83,33% | 1,30 % | 1,30 % |
| random | 13.968đ | -66.032đ | -82,54% | 1,26 % | 1,22 % |
| hot | 12.260đ | -67.740đ | -84,68% | 1,31 % | 1,23 % |
| cold | 14.325đ | -65.675đ | -82,09% | 1,45 % | 1,37 % |
| balanced | 14.171đ | -65.829đ | -82,29% | 1,34 % | 1,31 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 995 kỳ: 79.600.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01015 | 18/11/2026 | `01 13 25 28 29 | 08` | - |
| #01014 | 17/11/2026 | `08 09 12 27 30 | 05` | - |
| #01013 | 17/11/2026 | `06 10 13 19 22 | 04` | - |
| #01012 | 16/11/2026 | `10 11 15 23 34 | 08` | - |
| #01011 | 16/11/2026 | `04 10 11 20 34 | 03` | - |

</details>

## Mega 6/45

- Kỳ mới nhất: **#01589** 15/11/2026 — `06 22 24 26 40 44`
- Lịch sử dùng để gợi ý: 1589 kỳ (#00001 → #01589)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 13,33 % số kỳ; thực tế từ 11,64 % (số 38) đến 14,60 % (số 44).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1589 kỳ: p = 0.9196 → chưa bác bỏ giả thuyết tần suất đều.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 13/11/2026 → kỳ #01589 (15/11/2026): `06 22 24 26 40 44`

| Chiến lược | Bộ số | Trùng | Kết quả |
|---|---|---|---|
| hot | 01 10 15 25 36 43 | 0/6 | - |
| hot | 07 10 33 36 **40** 41 | 1/6 | - |
| cold | 12 13 16 21 **26** 28 | 1/6 | - |
| cold | 08 16 21 **22** **24** 29 | 2/6 | - |
| balanced | 07 10 17 29 34 42 | 0/6 | - |
| balanced | 03 08 15 **24** 42 43 | 1/6 | - |
| random | 02 07 14 19 23 43 | 0/6 | - |
| random | 14 19 23 37 39 **44** | 1/6 | - |
| cover | 02 04 09 17 37 41 | 0/6 | - |
| cover | 03 08 **24** 27 31 36 | 1/6 | - |
| cover | 10 15 25 29 30 43 | 0/6 | - |
| cover | 01 11 19 **22** 35 **40** | 2/6 | - |
| cover | 07 12 16 23 **26** 34 | 1/6 | - |
| cover | 05 14 21 32 38 45 | 0/6 | - |
| cover | 20 28 33 39 42 **44** | 1/6 | - |
| cover | **06** 13 18 34 35 42 | 1/6 | - |

Kỳ này: 12 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 12.8), 0 bộ trúng giải (kỳ vọng 0.4).

<details><summary>Thành tích thực tế cộng dồn (3 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng |
|---|---:|---:|---:|---:|---:|
| balanced | 6 | 0.500 | 0.800 | 0 | 0.1 |
| cold | 6 | 1.000 | 0.800 | 0 | 0.1 |
| cover | 24 | 0.792 | 0.800 | 0 | 0.6 |
| hot | 6 | 0.500 | 0.800 | 0 | 0.1 |
| random | 6 | 1.167 | 0.800 | 1 | 0.1 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01590**, bắt đầu 18:00 18/11/2026 (giờ VN).
- Bộ số tạo lúc 19:54:38 15/11/2026 · seed `20261115` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **11,87 %**, tức khoảng 1 trong 8 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 11,36 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 2,38 %.

| Vé | Bộ số | Mua từ vé 1 đến vé này: có ≥1 vé trúng | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---:|---:|
| cover 1 | `05 07 23 32 44 45` | **2,38 %** | 2,38 % |
| cover 2 | `16 21 26 31 40 41` | **4,76 %** | 4,71 % |
| cover 3 | `04 08 12 15 33 34` | **7,14 %** | 6,98 % |
| cover 4 | `02 06 13 17 24 30` | **9,50 %** | 9,20 % |
| cover 5 | `01 09 18 25 28 42` | **11,87 %** | 11,36 % |
| cover 6 | `10 20 22 36 38 43` | **14,23 %** | 13,47 % |
| cover 7 | `03 11 19 29 35 37` | **16,58 %** | 15,54 % |
| cover 8 | `14 20 26 27 29 39` | **18,81 %** | 17,55 % |

- Với Mega 6/45: vé không có số ĐB và 94 % số lần trúng là Giải Ba (3 số). So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.
- Jackpot (6 số): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 1.018.132, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 1.569 kỳ đã quay (#00021 → #01589) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 18,55 % (khoảng 1 trong 5 kỳ)**
- random → 17,72 % (khoảng 1 trong 6 kỳ)
- hot → 14,45 % (khoảng 1 trong 7 kỳ)
- cold → 13,60 % (khoảng 1 trong 7 kỳ)
- balanced → 17,59 % (khoảng 1 trong 6 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 10.641đ với phủ số và 10.052đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | Chẵn/Lẻ | Nhỏ/Lớn (≤22) | Tổng |
|---|---|---|---|---:|
| hot | `01 10 22 25 41 43` | 2/4 | 3/3 | 142 |
| hot | `01 10 25 40 41 43` | 2/4 | 2/4 | 160 |
| cold | `04 09 12 21 29 34` | 3/3 | 4/2 | 109 |
| cold | `04 21 28 35 41 42` | 3/3 | 2/4 | 171 |
| balanced | `03 16 22 26 27 33` | 3/3 | 3/3 | 127 |
| balanced | `05 19 20 26 32 39` | 3/3 | 3/3 | 141 |
| random (đối chứng) | `03 05 24 38 43 44` | 3/3 | 2/4 | 157 |
| random (đối chứng) | `06 07 08 09 11 44` | 3/3 | 5/1 | 85 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **2,38 %** · trúng ≥3 số **2,38 %** · Jackpot **0,0000123 %** (1 / 8.145.060).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 | 6 |
|---|---:|---:|---:|---:|---:|---:|---:|
| Xác suất | 40,06 % | 42,41 % | 15,15 % | 2,24 % | 0,136 % | 0,00287 % | 0,0000123 % |

<details><summary>Backtest walk-forward</summary>

**Từng vé: 1569 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.797 | 2,37 % | - | - | - |
| hot | 0.801 | 2,37 % | -0.002 | -0.202 đến +0.198 | 0.9867 |
| cold | 0.791 | 2,30 % | -0.067 | -0.272 đến +0.137 | 0.5198 |
| balanced | 0.803 | 2,39 % | +0.022 | -0.043 đến +0.087 | 0.5043 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 18,55 % | 15,12 % đến 21,98 % | +0.835 | +0.386 đến +1.285 | 0.0002752 |
| random | 17,72 % | 14,29 % đến 21,15 % | - | - | - |
| hot | 14,45 % | 11,02 % đến 17,88 % | -3.268 | -4.347 đến -2.188 | 3.566E-09 |
| cold | 13,60 % | 10,17 % đến 17,03 % | -4.113 | -5.173 đến -3.054 | 4.635E-14 |
| balanced | 17,59 % | 14,16 % đến 21,02 % | -0.124 | -0.578 đến +0.330 | 0.5925 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 10.641đ | -69.359đ | -86,70% | 1,11 % | 1,11 % |
| random | 10.052đ | -69.948đ | -87,44% | 1,15 % | 1,15 % |
| hot | 10.527đ | -69.473đ | -86,84% | 1,70 % | 1,70 % |
| cold | 10.973đ | -69.027đ | -86,28% | 1,55 % | 1,55 % |
| balanced | 11.707đ | -68.293đ | -85,37% | 1,20 % | 1,20 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 1569 kỳ: 125.520.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01589 | 15/11/2026 | `06 22 24 26 40 44` | 13.789.728.500đ |
| #01588 | 13/11/2026 | `01 07 15 19 31 33` | 19.775.734.000đ |
| #01587 | 11/11/2026 | `03 11 14 23 25 43` | 17.638.002.500đ |
| #01586 | 08/11/2026 | `01 02 14 20 23 36` | 15.740.836.000đ |
| #01585 | 06/11/2026 | `01 10 37 38 39 45` | 13.783.788.500đ |

</details>

## Power 6/55

- Kỳ mới nhất: **#01425** 17/11/2026 — `06 20 36 41 49 52 | 34`
- Lịch sử dùng để gợi ý: 1425 kỳ (#00001 → #01425)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 10,91 % số kỳ; thực tế từ 8,84 % (số 04) đến 12,70 % (số 22).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1425 kỳ: p = 0.3950 → chưa bác bỏ giả thuyết tần suất đều.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 15/11/2026 → kỳ #01425 (17/11/2026): `06 20 36 41 49 52 | 34`

| Chiến lược | Bộ số | Trùng | Kết quả |
|---|---|---|---|
| hot | 11 **20** 28 34 42 45 | 1/6 + ĐB | - |
| hot | 05 11 31 34 39 42 | 0/6 + ĐB | - |
| cold | 03 04 13 23 24 27 | 0/6 | - |
| cold | 15 24 27 32 40 50 | 0/6 | - |
| balanced | **06** 11 22 39 46 **49** | 2/6 | - |
| balanced | 13 21 24 32 43 48 | 0/6 | - |
| random | **20** 26 47 50 51 54 | 1/6 | - |
| random | 02 13 27 47 **49** 53 | 1/6 | - |
| cover | **06** 10 18 39 43 50 | 1/6 | - |
| cover | 01 12 30 32 35 40 | 0/6 | - |
| cover | 07 21 26 37 **49** 55 | 1/6 | - |
| cover | 15 31 34 44 **52** 53 | 1/6 + ĐB | - |
| cover | 04 09 11 28 38 46 | 0/6 | - |
| cover | 16 17 24 25 48 51 | 0/6 | - |
| cover | 02 03 19 22 33 42 | 0/6 | - |
| cover | 05 14 **20** 27 29 47 | 1/6 | - |

Kỳ này: 9 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 10.5), 0 bộ trúng giải (kỳ vọng 0.2).

<details><summary>Thành tích thực tế cộng dồn (4 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng |
|---|---:|---:|---:|---:|---:|
| balanced | 8 | 1.000 | 0.655 | 1 | 0.1 |
| cold | 8 | 0.625 | 0.655 | 0 | 0.1 |
| cover | 32 | 0.688 | 0.655 | 1 | 0.4 |
| hot | 8 | 0.125 | 0.655 | 0 | 0.1 |
| random | 8 | 0.750 | 0.655 | 0 | 0.1 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01426**, bắt đầu 18:00 19/11/2026 (giờ VN).
- Bộ số tạo lúc 14:12:11 18/11/2026 · seed `20261118` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **6,65 %**, tức khoảng 1 trong 15 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 6,49 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 1,33 %.

| Vé | Bộ số | Mua từ vé 1 đến vé này: có ≥1 vé trúng | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---:|---:|
| cover 1 | `03 06 14 43 45 47` | **1,33 %** | 1,33 % |
| cover 2 | `08 17 22 23 33 37` | **2,66 %** | 2,65 % |
| cover 3 | `16 21 29 39 48 53` | **3,99 %** | 3,95 % |
| cover 4 | `09 10 41 42 44 46` | **5,32 %** | 5,23 % |
| cover 5 | `04 07 11 25 27 51` | **6,65 %** | 6,49 % |
| cover 6 | `02 05 15 36 40 49` | **7,98 %** | 7,74 % |
| cover 7 | `19 26 34 38 50 55` | **9,30 %** | 8,97 % |
| cover 8 | `12 13 20 30 32 52` | **10,62 %** | 10,18 % |

- Với Power 6/55: số ĐB là quả thứ 7 quay cùng lồng, chỉ dùng cho giải 5 số + ĐB, nên không có giải nào chỉ cần trùng ĐB; 95 % xác suất trúng giải của một vé thuộc Giải Ba (3 số). So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.
- Jackpot 1 (6 số): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 3.623.709, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 1.405 kỳ đã quay (#00021 → #01425) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 10,64 % (khoảng 1 trong 9 kỳ)**
- random → 10,11 % (khoảng 1 trong 10 kỳ)
- hot → 8,53 % (khoảng 1 trong 12 kỳ)
- cold → 9,12 % (khoảng 1 trong 11 kỳ)
- balanced → 9,92 % (khoảng 1 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 10.700đ với phủ số và 8.967đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | Chẵn/Lẻ | Nhỏ/Lớn (≤27) | Tổng |
|---|---|---|---|---:|
| hot | `05 11 18 28 31 38` | 3/3 | 3/3 | 131 |
| hot | `20 31 34 38 41 52` | 4/2 | 1/5 | 216 |
| cold | `03 07 08 11 13 23` | 1/5 | 6/0 | 65 |
| cold | `03 08 14 16 30 50` | 5/1 | 4/2 | 121 |
| balanced | `01 02 21 40 42 53` | 3/3 | 3/3 | 159 |
| balanced | `09 18 19 28 30 49` | 3/3 | 3/3 | 153 |
| random (đối chứng) | `05 13 26 49 51 54` | 2/4 | 3/3 | 198 |
| random (đối chứng) | `05 14 19 20 38 51` | 3/3 | 4/2 | 147 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **1,33 %** · trúng ≥3 số **1,33 %** · Jackpot 1 **0,00000345 %** (1 / 28.989.675).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 | 6 |
|---|---:|---:|---:|---:|---:|---:|---:|
| Xác suất | 48,24 % | 39,47 % | 10,96 % | 1,27 % | 0,0608 % | 0,00101 % | 0,00000345 % |

<details><summary>Backtest walk-forward</summary>

**Từng vé: 1405 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.655 | 1,33 % | - | - | - |
| hot | 0.656 | 1,35 % | +0.023 | -0.134 đến +0.180 | 0.7783 |
| cold | 0.659 | 1,45 % | +0.130 | -0.033 đến +0.293 | 0.118 |
| balanced | 0.656 | 1,35 % | +0.023 | -0.027 đến +0.073 | 0.3657 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 10,64 % | 7,02 % đến 14,26 % | +0.529 | +0.158 đến +0.900 | 0.005231 |
| random | 10,11 % | 6,49 % đến 13,74 % | - | - | - |
| hot | 8,53 % | 4,91 % đến 12,16 % | -1.579 | -2.470 đến -0.689 | 0.0005168 |
| cold | 9,12 % | 5,49 % đến 12,74 % | -0.996 | -1.921 đến -0.072 | 0.03473 |
| balanced | 9,92 % | 6,30 % đến 13,55 % | -0.190 | -0.567 đến +0.186 | 0.3211 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 10.700đ | -69.300đ | -86,62% | 0,556 % | 0,556 % |
| random | 8.967đ | -71.033đ | -88,79% | 0,885 % | 0,885 % |
| hot | 12.998đ | -67.002đ | -83,75% | 2,04 % | 2,04 % |
| cold | 8.757đ | -71.243đ | -89,05% | 2,19 % | 2,19 % |
| balanced | 11.177đ | -68.823đ | -86,03% | 0,916 % | 0,916 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Chưa phát hiện cách chọn tăng tỷ lệ trúng giải so với random ở ngưỡng đã hiệu chỉnh.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 1405 kỳ: 112.400.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01425 | 17/11/2026 | `06 20 36 41 49 52 | 34` | 40.971.513.000đ |
| #01424 | 14/11/2026 | `05 28 36 42 45 47 | 51` | 37.641.706.500đ |
| #01423 | 12/11/2026 | `17 25 33 34 35 36 | 48` | 35.115.629.250đ |
| #01422 | 10/11/2026 | `01 02 21 38 39 51 | 12` | 32.688.020.100đ |
| #01421 | 07/11/2026 | `05 19 20 26 44 53 | 39` | 22.636.747.050đ |

</details>

