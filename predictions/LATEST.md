# Gợi ý Vietlott — 15/11/2026

> **Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng.**
> Mọi bộ số — kể cả bộ ngẫu nhiên — có cùng xác suất trúng. Bộ gợi ý chỉ phản ánh đặc điểm dữ liệu quá khứ.

Tạo báo cáo lúc 13:36 15/11/2026 (giờ VN). Mỗi cách chọn là một phương án mua riêng.

## Lotto 5/35

- Kỳ mới nhất: **#01009** 15/11/2026 — `10 16 19 20 26 | 12`
- Lịch sử dùng để gợi ý: 1009 kỳ (#00001 → #01009)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 14,29 % số kỳ; thực tế từ 11,79 % (số 12) đến 16,75 % (số 22).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1009 kỳ: p = 0.0172 → có dấu hiệu lệch tần suất ở ngưỡng 5% chưa hiệu chỉnh nhiều phép thử.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 13/11/2026 → kỳ #01007 (14/11/2026): `02 16 20 28 35 | 01`

| Chiến lược | Bộ số | ĐB | Trùng | Kết quả |
|---|---|---|---|---|
| hot | **02** 04 08 22 26 | 06 | 1/5 | - |
| hot | 10 15 **20** 26 33 | 06 | 1/5 | - |
| cold | 06 29 30 31 34 | 04 | 0/5 | - |
| cold | 06 13 29 32 34 | 09 | 0/5 | - |
| balanced | 11 12 **16** 29 30 | 06 | 1/5 | - |
| balanced | 01 10 14 23 34 | 08 | 0/5 | - |
| random | 01 05 13 **20** 23 | 04 | 1/5 | - |
| random | **02** 07 10 15 23 | 02 | 1/5 | - |
| cover | 13 18 22 27 **35** | **01** ✓ | 1/5 + ĐB | 🎉 Khuyến khích |
| cover | 08 09 15 **16** 23 | 06 | 1/5 | - |
| cover | 03 07 19 **20** 26 | 08 | 1/5 | - |
| cover | 04 14 17 29 32 | 02 | 0/5 | - |
| cover | 06 12 21 **28** 30 | 12 | 1/5 | - |
| cover | 01 **02** 05 31 33 | 10 | 1/5 | - |
| cover | 10 11 24 25 34 | 03 | 0/5 | - |
| cover | 04 08 10 13 21 | 11 | 0/5 | - |

Kỳ này: 10 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 11.4), 1 bộ trúng giải (kỳ vọng 1.5).

<details><summary>Thành tích thực tế cộng dồn (7 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng | Trùng ĐB |
|---|---:|---:|---:|---:|---:|---:|
| balanced | 14 | 0.643 | 0.714 | 2 | 1.3 | 2/14 |
| cold | 14 | 0.786 | 0.714 | 0 | 1.3 | 0/14 |
| cover | 56 | 0.714 | 0.714 | 5 | 5.4 | 5/56 |
| hot | 14 | 1.071 | 0.714 | 2 | 1.3 | 2/14 |
| random | 14 | 0.786 | 0.714 | 1 | 1.3 | 0/14 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01010**, bắt đầu 21:00 15/11/2026 (giờ VN).
- Bộ số tạo lúc 13:36:37 15/11/2026 · seed `20261115` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **45,71 %**, tức khoảng 1 trong 2 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 39,64 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 9,60 %.

| Vé | Bộ số | ĐB | Mua từ vé 1 đến vé này: có ≥1 vé trúng | …có ≥1 vé trùng ≥3 số | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---|---:|---:|---:|
| cover 1 | `05 07 21 28 34` | `07` | **9,60 %** | 1,39 % | 9,60 % |
| cover 2 | `04 13 23 29 31` | `03` | **18,98 %** | 2,77 % | 18,29 % |
| cover 3 | `08 10 17 27 33` | `10` | **28,12 %** | 4,16 % | 26,13 % |
| cover 4 | `01 02 03 15 16` | `06` | **37,03 %** | 5,55 % | 33,23 % |
| cover 5 | `09 11 24 26 32` | `04` | **45,71 %** | 6,93 % | 39,64 % |
| cover 6 | `12 18 20 22 30` | `01` | **54,16 %** | 8,32 % | 45,44 % |
| cover 7 | `06 14 19 25 35` | `12` | **62,38 %** | 9,71 % | 50,68 % |
| cover 8 | `01 04 12 17 26` | `09` | **70,35 %** | 11,04 % | 55,42 % |

- Với Lotto 5/35: giải Khuyến khích (0–2 số + ĐB) chiếm 86 % số lần trúng và chỉ cần trùng số ĐB (01–12), nên số ĐB khác nhau là lợi thế chính: 5 vé phủ 5/12 khả năng của số ĐB; 12 vé có đủ 12 số ĐB thì kỳ nào cũng có vé trúng, nhưng vẫn có thể lỗ tiền.
- Độc đắc (5 số + ĐB): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 486.948, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 989 kỳ đã quay (#00021 → #01009) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 70,52 % (khoảng 7 trong 10 kỳ)**
- random → 55,60 % (khoảng 6 trong 10 kỳ)
- hot → 34,65 % (khoảng 1 trong 3 kỳ)
- cold → 36,54 % (khoảng 1 trong 3 kỳ)
- balanced → 55,46 % (khoảng 6 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 14.816đ với phủ số và 13.243đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | ĐB | Chẵn/Lẻ | Nhỏ/Lớn (≤17) | Tổng |
|---|---|---|---|---|---:|
| hot | `02 08 16 27 35` | `06` | 3/2 | 3/2 | 88 |
| hot | `15 18 22 33 35` | `06` | 2/3 | 1/4 | 123 |
| cold | `03 17 24 30 31` | `04` | 2/3 | 2/3 | 105 |
| cold | `06 21 24 31 32` | `09` | 3/2 | 1/4 | 114 |
| balanced | `04 07 29 30 32` | `09` | 3/2 | 2/3 | 102 |
| balanced | `11 12 17 22 23` | `03` | 2/3 | 3/2 | 85 |
| random (đối chứng) | `02 23 24 28 35` | `05` | 3/2 | 1/4 | 112 |
| random (đối chứng) | `01 03 09 14 21` | `03` | 1/4 | 4/1 | 48 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **9,60 %** · trúng ≥3 số **1,39 %** · Độc đắc **0,0000257 %** (1 / 3.895.584).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 |
|---|---:|---:|---:|---:|---:|---:|
| Xác suất | 43,90 % | 42,21 % | 12,51 % | 1,34 % | 0,0462 % | 0,000308 % |

Số ĐB trùng: 8,33 % (độc lập với số chính).

<details><summary>Backtest walk-forward</summary>

**Từng vé: 989 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.714 | 9,56 % | - | - | - |
| hot | 0.712 | 9,06 % | -0.500 | -1.239 đến +0.238 | 0.1841 |
| cold | 0.724 | 9,57 % | +0.019 | -0.733 đến +0.770 | 0.9614 |
| balanced | 0.713 | 9,53 % | -0.024 | -0.174 đến +0.126 | 0.7509 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 70,52 % | 66,20 % đến 74,84 % | +14.918 | +14.198 đến +15.638 | 4.551E-213 |
| random | 55,60 % | 51,28 % đến 59,92 % | - | - | - |
| hot | 34,65 % | 30,33 % đến 38,96 % | -20.955 | -23.514 đến -18.395 | 8.84E-52 |
| cold | 36,54 % | 32,22 % đến 40,86 % | -19.061 | -21.644 đến -16.478 | 3E-43 |
| balanced | 55,46 % | 51,14 % đến 59,78 % | -0.139 | -0.875 đến +0.596 | 0.7101 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 14.816đ | -65.184đ | -81,48% | 1,22 % | 1,22 % |
| random | 13.243đ | -66.757đ | -83,45% | 1,37 % | 1,34 % |
| hot | 12.243đ | -67.757đ | -84,70% | 1,18 % | 1,11 % |
| cold | 14.659đ | -65.341đ | -81,68% | 1,54 % | 1,41 % |
| balanced | 13.095đ | -66.905đ | -83,63% | 1,32 % | 1,30 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 989 kỳ: 79.120.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01009 | 15/11/2026 | `10 16 19 20 26 | 12` | - |
| #01008 | 14/11/2026 | `05 15 18 20 33 | 02` | - |
| #01007 | 14/11/2026 | `02 16 20 28 35 | 01` | - |
| #01006 | 13/11/2026 | `08 20 22 25 26 | 05` | - |
| #01005 | 13/11/2026 | `04 23 27 28 35 | 03` | - |

</details>

## Mega 6/45

- Kỳ mới nhất: **#01588** 13/11/2026 — `01 07 15 19 31 33`
- Lịch sử dùng để gợi ý: 1588 kỳ (#00001 → #01588)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 13,33 % số kỳ; thực tế từ 11,65 % (số 38) đến 14,55 % (số 07).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1588 kỳ: p = 0.9276 → chưa bác bỏ giả thuyết tần suất đều.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 11/11/2026 → kỳ #01588 (13/11/2026): `01 07 15 19 31 33`

| Chiến lược | Bộ số | Trùng | Kết quả |
|---|---|---|---|
| hot | 02 03 10 11 14 36 | 0/6 | - |
| hot | 03 20 23 25 41 43 | 0/6 | - |
| cold | 06 08 12 27 32 35 | 0/6 | - |
| cold | 06 12 27 29 **31** **33** | 2/6 | - |
| balanced | 02 05 21 24 34 37 | 0/6 | - |
| balanced | **01** 09 11 32 34 36 | 1/6 | - |
| random | 06 10 **19** **31** **33** 37 | 3/6 | 🎉 Giải Ba |
| random | **01** 06 13 18 32 39 | 1/6 | - |
| cover | **19** 26 29 36 42 43 | 1/6 | - |
| cover | **01** 06 18 22 23 38 | 1/6 | - |
| cover | 09 17 28 39 40 41 | 0/6 | - |
| cover | 02 08 27 **31** **33** 37 | 2/6 | - |
| cover | 12 **15** 20 21 30 35 | 1/6 | - |
| cover | 04 10 13 24 32 44 | 0/6 | - |
| cover | **07** 11 16 25 34 45 | 1/6 | - |
| cover | 03 05 14 20 23 27 | 0/6 | - |

Kỳ này: 13 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 12.8), 1 bộ trúng giải (kỳ vọng 0.4).

<details><summary>Thành tích thực tế cộng dồn (2 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng |
|---|---:|---:|---:|---:|---:|
| balanced | 4 | 0.500 | 0.800 | 0 | 0.1 |
| cold | 4 | 0.750 | 0.800 | 0 | 0.1 |
| cover | 16 | 0.812 | 0.800 | 0 | 0.4 |
| hot | 4 | 0.500 | 0.800 | 0 | 0.1 |
| random | 4 | 1.500 | 0.800 | 1 | 0.1 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01589**, bắt đầu 18:00 15/11/2026 (giờ VN).
- Bộ số tạo lúc 21:07:24 13/11/2026 · seed `20261113` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **11,87 %**, tức khoảng 1 trong 8 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 11,36 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 2,38 %.

| Vé | Bộ số | Mua từ vé 1 đến vé này: có ≥1 vé trúng | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---:|---:|
| cover 1 | `02 04 09 17 37 41` | **2,38 %** | 2,38 % |
| cover 2 | `03 08 24 27 31 36` | **4,76 %** | 4,71 % |
| cover 3 | `10 15 25 29 30 43` | **7,14 %** | 6,98 % |
| cover 4 | `01 11 19 22 35 40` | **9,50 %** | 9,20 % |
| cover 5 | `07 12 16 23 26 34` | **11,87 %** | 11,36 % |
| cover 6 | `05 14 21 32 38 45` | **14,23 %** | 13,47 % |
| cover 7 | `20 28 33 39 42 44` | **16,58 %** | 15,54 % |
| cover 8 | `06 13 18 34 35 42` | **18,81 %** | 17,55 % |

- Với Mega 6/45: vé không có số ĐB và 94 % số lần trúng là Giải Ba (3 số). So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.
- Jackpot (6 số): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 1.018.132, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 1.568 kỳ đã quay (#00021 → #01588) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 18,71 % (khoảng 1 trong 5 kỳ)**
- random → 17,74 % (khoảng 1 trong 6 kỳ)
- hot → 14,26 % (khoảng 1 trong 7 kỳ)
- cold → 13,77 % (khoảng 1 trong 7 kỳ)
- balanced → 17,54 % (khoảng 1 trong 6 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 11.817đ với phủ số và 11.281đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | Chẵn/Lẻ | Nhỏ/Lớn (≤22) | Tổng |
|---|---|---|---|---:|
| hot | `01 10 15 25 36 43` | 2/4 | 3/3 | 130 |
| hot | `07 10 33 36 40 41` | 3/3 | 2/4 | 167 |
| cold | `12 13 16 21 26 28` | 4/2 | 4/2 | 116 |
| cold | `08 16 21 22 24 29` | 4/2 | 4/2 | 120 |
| balanced | `07 10 17 29 34 42` | 3/3 | 3/3 | 139 |
| balanced | `03 08 15 24 42 43` | 3/3 | 3/3 | 135 |
| random (đối chứng) | `02 07 14 19 23 43` | 2/4 | 4/2 | 108 |
| random (đối chứng) | `14 19 23 37 39 44` | 2/4 | 2/4 | 176 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **2,38 %** · trúng ≥3 số **2,38 %** · Jackpot **0,0000123 %** (1 / 8.145.060).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 | 6 |
|---|---:|---:|---:|---:|---:|---:|---:|
| Xác suất | 40,06 % | 42,41 % | 15,15 % | 2,24 % | 0,136 % | 0,00287 % | 0,0000123 % |

<details><summary>Backtest walk-forward</summary>

**Từng vé: 1568 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.799 | 2,40 % | - | - | - |
| hot | 0.799 | 2,35 % | -0.056 | -0.256 đến +0.144 | 0.5841 |
| cold | 0.789 | 2,25 % | -0.158 | -0.355 đến +0.040 | 0.1182 |
| balanced | 0.802 | 2,44 % | +0.035 | -0.030 đến +0.100 | 0.2923 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 18,71 % | 15,28 % đến 22,14 % | +0.972 | +0.534 đến +1.410 | 1.424E-05 |
| random | 17,74 % | 14,31 % đến 21,17 % | - | - | - |
| hot | 14,26 % | 10,83 % đến 17,69 % | -3.480 | -4.563 đến -2.397 | 3.755E-10 |
| cold | 13,77 % | 10,34 % đến 17,20 % | -3.968 | -5.030 đến -2.905 | 3.806E-13 |
| balanced | 17,54 % | 14,11 % đến 20,97 % | -0.196 | -0.653 đến +0.260 | 0.3984 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 11.817đ | -68.183đ | -85,23% | 1,09 % | 1,09 % |
| random | 11.281đ | -68.719đ | -85,90% | 1,17 % | 1,17 % |
| hot | 10.674đ | -69.326đ | -86,66% | 1,54 % | 1,54 % |
| cold | 10.935đ | -69.065đ | -86,33% | 1,54 % | 1,54 % |
| balanced | 11.137đ | -68.863đ | -86,08% | 1,23 % | 1,23 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 1568 kỳ: 125.440.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01588 | 13/11/2026 | `01 07 15 19 31 33` | 19.775.734.000đ |
| #01587 | 11/11/2026 | `03 11 14 23 25 43` | 17.638.002.500đ |
| #01586 | 08/11/2026 | `01 02 14 20 23 36` | 15.740.836.000đ |
| #01585 | 06/11/2026 | `01 10 37 38 39 45` | 13.783.788.500đ |
| #01584 | 04/11/2026 | `07 14 20 30 34 40` | 26.087.005.500đ |

</details>

## Power 6/55

- Kỳ mới nhất: **#01424** 14/11/2026 — `05 28 36 42 45 47 | 51`
- Lịch sử dùng để gợi ý: 1424 kỳ (#00001 → #01424)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 10,91 % số kỳ; thực tế từ 8,85 % (số 04) đến 12,71 % (số 22).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1424 kỳ: p = 0.3908 → chưa bác bỏ giả thuyết tần suất đều.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 12/11/2026 → kỳ #01424 (14/11/2026): `05 28 36 42 45 47 | 51`

| Chiến lược | Bộ số | Trùng | Kết quả |
|---|---|---|---|
| hot | 02 11 14 25 34 38 | 0/6 | - |
| hot | 01 02 14 18 23 34 | 0/6 | - |
| cold | 08 15 16 23 24 48 | 0/6 | - |
| cold | 11 30 32 37 40 **47** | 1/6 | - |
| balanced | 10 16 25 41 46 53 | 0/6 | - |
| balanced | 10 11 12 35 40 43 | 0/6 | - |
| random | 15 17 21 23 34 54 | 0/6 | - |
| random | 01 02 **36** **47** 51 55 | 2/6 + ĐB | - |
| cover | 09 17 19 31 41 46 | 0/6 | - |
| cover | **05** 07 16 22 34 51 | 1/6 + ĐB | - |
| cover | 06 13 21 26 33 53 | 0/6 | - |
| cover | 02 10 29 30 35 55 | 0/6 | - |
| cover | 14 20 24 **42** **45** 50 | 2/6 | - |
| cover | 08 11 **28** 32 43 52 | 1/6 | - |
| cover | 04 **36** 40 44 **47** 49 | 2/6 | - |
| cover | 12 15 23 38 39 48 | 0/6 | - |

Kỳ này: 9 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 10.5), 0 bộ trúng giải (kỳ vọng 0.2).

<details><summary>Thành tích thực tế cộng dồn (3 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng |
|---|---:|---:|---:|---:|---:|
| balanced | 6 | 1.000 | 0.655 | 1 | 0.1 |
| cold | 6 | 0.833 | 0.655 | 0 | 0.1 |
| cover | 24 | 0.750 | 0.655 | 1 | 0.3 |
| hot | 6 | 0.000 | 0.655 | 0 | 0.1 |
| random | 6 | 0.667 | 0.655 | 0 | 0.1 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01425**, bắt đầu 18:00 17/11/2026 (giờ VN).
- Bộ số tạo lúc 13:36:37 15/11/2026 · seed `20261115` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **6,65 %**, tức khoảng 1 trong 15 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 6,49 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 1,33 %.

| Vé | Bộ số | Mua từ vé 1 đến vé này: có ≥1 vé trúng | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---:|---:|
| cover 1 | `06 10 18 39 43 50` | **1,33 %** | 1,33 % |
| cover 2 | `01 12 30 32 35 40` | **2,66 %** | 2,65 % |
| cover 3 | `07 21 26 37 49 55` | **3,99 %** | 3,95 % |
| cover 4 | `15 31 34 44 52 53` | **5,32 %** | 5,23 % |
| cover 5 | `04 09 11 28 38 46` | **6,65 %** | 6,49 % |
| cover 6 | `16 17 24 25 48 51` | **7,98 %** | 7,74 % |
| cover 7 | `02 03 19 22 33 42` | **9,30 %** | 8,97 % |
| cover 8 | `05 14 20 27 29 47` | **10,62 %** | 10,18 % |

- Với Power 6/55: số ĐB là quả thứ 7 quay cùng lồng, chỉ dùng cho giải 5 số + ĐB, nên không có giải nào chỉ cần trùng ĐB; 95 % xác suất trúng giải của một vé thuộc Giải Ba (3 số). So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.
- Jackpot 1 (6 số): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 3.623.709, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 1.404 kỳ đã quay (#00021 → #01424) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 10,79 % (khoảng 1 trong 9 kỳ)**
- random → 10,27 % (khoảng 1 trong 10 kỳ)
- hot → 8,72 % (khoảng 1 trong 11 kỳ)
- cold → 9,03 % (khoảng 1 trong 11 kỳ)
- balanced → 9,96 % (khoảng 1 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 8.583đ với phủ số và 12.978đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | Chẵn/Lẻ | Nhỏ/Lớn (≤27) | Tổng |
|---|---|---|---|---:|
| hot | `11 20 28 34 42 45` | 4/2 | 2/4 | 180 |
| hot | `05 11 31 34 39 42` | 2/4 | 2/4 | 162 |
| cold | `03 04 13 23 24 27` | 2/4 | 6/0 | 94 |
| cold | `15 24 27 32 40 50` | 4/2 | 3/3 | 188 |
| balanced | `06 11 22 39 46 49` | 3/3 | 3/3 | 173 |
| balanced | `13 21 24 32 43 48` | 3/3 | 3/3 | 181 |
| random (đối chứng) | `20 26 47 50 51 54` | 4/2 | 2/4 | 248 |
| random (đối chứng) | `02 13 27 47 49 53` | 1/5 | 3/3 | 191 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **1,33 %** · trúng ≥3 số **1,33 %** · Jackpot 1 **0,00000345 %** (1 / 28.989.675).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 | 6 |
|---|---:|---:|---:|---:|---:|---:|---:|
| Xác suất | 48,24 % | 39,47 % | 10,96 % | 1,27 % | 0,0608 % | 0,00101 % | 0,00000345 % |

<details><summary>Backtest walk-forward</summary>

**Từng vé: 1404 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.656 | 1,32 % | - | - | - |
| hot | 0.657 | 1,36 % | +0.042 | -0.119 đến +0.202 | 0.612 |
| cold | 0.661 | 1,44 % | +0.126 | -0.037 đến +0.289 | 0.1295 |
| balanced | 0.656 | 1,33 % | +0.015 | -0.034 đến +0.065 | 0.544 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 10,79 % | 7,17 % đến 14,41 % | +0.524 | +0.140 đến +0.908 | 0.00757 |
| random | 10,27 % | 6,64 % đến 13,89 % | - | - | - |
| hot | 8,72 % | 5,09 % đến 12,34 % | -1.548 | -2.455 đến -0.641 | 0.0008393 |
| cold | 9,03 % | 5,41 % đến 12,66 % | -1.232 | -2.147 đến -0.317 | 0.008318 |
| balanced | 9,96 % | 6,33 % đến 13,58 % | -0.310 | -0.691 đến +0.071 | 0.1107 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 8.583đ | -71.417đ | -89,27% | 0,576 % | 0,576 % |
| random | 12.978đ | -67.022đ | -83,78% | 0,943 % | 0,943 % |
| hot | 10.838đ | -69.162đ | -86,45% | 2,08 % | 2,08 % |
| cold | 13.410đ | -66.590đ | -83,24% | 2,11 % | 2,11 % |
| balanced | 8.843đ | -71.157đ | -88,95% | 0,897 % | 0,897 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Chưa phát hiện cách chọn tăng tỷ lệ trúng giải so với random ở ngưỡng đã hiệu chỉnh.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 1404 kỳ: 112.320.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01424 | 14/11/2026 | `05 28 36 42 45 47 | 51` | 37.641.706.500đ |
| #01423 | 12/11/2026 | `17 25 33 34 35 36 | 48` | 35.115.629.250đ |
| #01422 | 10/11/2026 | `01 02 21 38 39 51 | 12` | 32.688.020.100đ |
| #01421 | 07/11/2026 | `05 19 20 26 44 53 | 39` | 22.636.747.050đ |
| #01420 | 05/11/2026 | `06 20 34 41 49 52 | 36` | 42.432.448.800đ |

</details>

