# Gợi ý Vietlott — 16/11/2026

> **Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng.**
> Mọi bộ số — kể cả bộ ngẫu nhiên — có cùng xác suất trúng. Bộ gợi ý chỉ phản ánh đặc điểm dữ liệu quá khứ.

Tạo báo cáo lúc 13:10 16/11/2026 (giờ VN). Mỗi cách chọn là một phương án mua riêng.

## Lotto 5/35

- Kỳ mới nhất: **#01011** 16/11/2026 — `04 10 11 20 34 | 03`
- Lịch sử dùng để gợi ý: 1011 kỳ (#00001 → #01011)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 14,29 % số kỳ; thực tế từ 11,77 % (số 12) đến 16,72 % (số 10).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1011 kỳ: p = 0.0159 → có dấu hiệu lệch tần suất ở ngưỡng 5% chưa hiệu chỉnh nhiều phép thử.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 15/11/2026 → kỳ #01010 (15/11/2026): `04 06 11 16 29 | 09`

| Chiến lược | Bộ số | ĐB | Trùng | Kết quả |
|---|---|---|---|---|
| hot | 02 08 **16** 27 35 | 06 | 1/5 | - |
| hot | 15 18 22 33 35 | 06 | 0/5 | - |
| cold | 03 17 24 30 31 | 04 | 0/5 | - |
| cold | **06** 21 24 31 32 | **09** ✓ | 1/5 + ĐB | 🎉 Khuyến khích |
| balanced | **04** 07 **29** 30 32 | **09** ✓ | 2/5 + ĐB | 🎉 Khuyến khích |
| balanced | **11** 12 17 22 23 | 03 | 1/5 | - |
| random | 02 23 24 28 35 | 05 | 0/5 | - |
| random | 01 03 09 14 21 | 03 | 0/5 | - |
| cover | 05 07 21 28 34 | 07 | 0/5 | - |
| cover | **04** 13 23 **29** 31 | 03 | 2/5 | - |
| cover | 08 10 17 27 33 | 10 | 0/5 | - |
| cover | 01 02 03 15 **16** | 06 | 1/5 | - |
| cover | 09 **11** 24 26 32 | 04 | 1/5 | - |
| cover | 12 18 20 22 30 | 01 | 0/5 | - |
| cover | **06** 14 19 25 35 | 12 | 1/5 | - |
| cover | 01 **04** 12 17 26 | **09** ✓ | 1/5 + ĐB | 🎉 Khuyến khích |

Kỳ này: 11 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 11.4), 3 bộ trúng giải (kỳ vọng 1.5).

<details><summary>Thành tích thực tế cộng dồn (8 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng | Trùng ĐB |
|---|---:|---:|---:|---:|---:|---:|
| balanced | 16 | 0.750 | 0.714 | 3 | 1.5 | 3/16 |
| cold | 16 | 0.750 | 0.714 | 1 | 1.5 | 1/16 |
| cover | 64 | 0.719 | 0.714 | 6 | 6.1 | 6/64 |
| hot | 16 | 1.000 | 0.714 | 2 | 1.5 | 2/16 |
| random | 16 | 0.688 | 0.714 | 1 | 1.5 | 0/16 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01012**, bắt đầu 21:00 16/11/2026 (giờ VN).
- Bộ số tạo lúc 13:10:50 16/11/2026 · seed `20261116` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **45,71 %**, tức khoảng 1 trong 2 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 39,64 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 9,60 %.

| Vé | Bộ số | ĐB | Mua từ vé 1 đến vé này: có ≥1 vé trúng | …có ≥1 vé trùng ≥3 số | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---|---:|---:|---:|
| cover 1 | `01 07 13 15 23` | `06` | **9,60 %** | 1,39 % | 9,60 % |
| cover 2 | `04 11 12 16 31` | `10` | **18,98 %** | 2,77 % | 18,29 % |
| cover 3 | `06 17 30 33 34` | `09` | **28,12 %** | 4,16 % | 26,13 % |
| cover 4 | `03 08 18 24 27` | `11` | **37,03 %** | 5,55 % | 33,23 % |
| cover 5 | `02 05 10 21 29` | `04` | **45,71 %** | 6,93 % | 39,64 % |
| cover 6 | `14 22 26 28 32` | `07` | **54,16 %** | 8,32 % | 45,44 % |
| cover 7 | `09 19 20 25 35` | `03` | **62,38 %** | 9,71 % | 50,68 % |
| cover 8 | `16 22 24 25 34` | `02` | **70,35 %** | 11,04 % | 55,42 % |

- Với Lotto 5/35: giải Khuyến khích (0–2 số + ĐB) chiếm 86 % số lần trúng và chỉ cần trùng số ĐB (01–12), nên số ĐB khác nhau là lợi thế chính: 5 vé phủ 5/12 khả năng của số ĐB; 12 vé có đủ 12 số ĐB thì kỳ nào cũng có vé trúng, nhưng vẫn có thể lỗ tiền.
- Độc đắc (5 số + ĐB): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 486.948, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 991 kỳ đã quay (#00021 → #01011) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 70,32 % (khoảng 7 trong 10 kỳ)**
- random → 55,44 % (khoảng 6 trong 10 kỳ)
- hot → 34,57 % (khoảng 1 trong 3 kỳ)
- cold → 36,55 % (khoảng 1 trong 3 kỳ)
- balanced → 55,29 % (khoảng 6 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 13.317đ với phủ số và 14.683đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | ĐB | Chẵn/Lẻ | Nhỏ/Lớn (≤17) | Tổng |
|---|---|---|---|---|---:|
| hot | `06 10 16 20 26` | `01` | 5/0 | 3/2 | 78 |
| hot | `04 11 20 26 27` | `05` | 3/2 | 2/3 | 88 |
| cold | `01 12 13 17 27` | `06` | 1/4 | 4/1 | 70 |
| cold | `01 17 27 30 31` | `07` | 1/4 | 2/3 | 106 |
| balanced | `01 08 21 25 32` | `12` | 2/3 | 2/3 | 87 |
| balanced | `01 02 21 26 35` | `01` | 2/3 | 2/3 | 85 |
| random (đối chứng) | `01 07 14 27 33` | `12` | 1/4 | 3/2 | 82 |
| random (đối chứng) | `10 20 24 26 33` | `10` | 4/1 | 1/4 | 113 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **9,60 %** · trúng ≥3 số **1,39 %** · Độc đắc **0,0000257 %** (1 / 3.895.584).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 |
|---|---:|---:|---:|---:|---:|---:|
| Xác suất | 43,90 % | 42,21 % | 12,51 % | 1,34 % | 0,0462 % | 0,000308 % |

Số ĐB trùng: 8,33 % (độc lập với số chính).

<details><summary>Backtest walk-forward</summary>

**Từng vé: 991 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.713 | 9,51 % | - | - | - |
| hot | 0.711 | 9,10 % | -0.405 | -1.150 đến +0.339 | 0.2858 |
| cold | 0.723 | 9,63 % | +0.121 | -0.632 đến +0.874 | 0.753 |
| balanced | 0.717 | 9,65 % | +0.148 | -0.009 đến +0.304 | 0.06431 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 70,32 % | 66,01 % đến 74,63 % | +14.883 | +14.203 đến +15.563 | 2.175E-228 |
| random | 55,44 % | 51,12 % đến 59,75 % | - | - | - |
| hot | 34,57 % | 30,26 % đến 38,89 % | -20.863 | -23.415 đến -18.311 | 1.168E-51 |
| cold | 36,55 % | 32,23 % đến 40,86 % | -18.892 | -21.479 đến -16.304 | 1.869E-42 |
| balanced | 55,29 % | 50,98 % đến 59,61 % | -0.142 | -0.882 đến +0.598 | 0.7069 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 13.317đ | -66.683đ | -83,35% | 1,15 % | 1,15 % |
| random | 14.683đ | -65.317đ | -81,65% | 1,28 % | 1,25 % |
| hot | 12.701đ | -67.299đ | -84,12% | 1,31 % | 1,23 % |
| cold | 13.574đ | -66.426đ | -83,03% | 1,52 % | 1,45 % |
| balanced | 13.359đ | -66.641đ | -83,30% | 1,31 % | 1,30 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 991 kỳ: 79.280.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01011 | 16/11/2026 | `04 10 11 20 34 | 03` | - |
| #01010 | 15/11/2026 | `04 06 11 16 29 | 09` | - |
| #01009 | 15/11/2026 | `10 16 19 20 26 | 12` | - |
| #01008 | 14/11/2026 | `05 15 18 20 33 | 02` | - |
| #01007 | 14/11/2026 | `02 16 20 28 35 | 01` | - |

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

- **cover → 18,88 % (khoảng 1 trong 5 kỳ)**
- random → 17,43 % (khoảng 1 trong 6 kỳ)
- hot → 14,18 % (khoảng 1 trong 7 kỳ)
- cold → 13,46 % (khoảng 1 trong 7 kỳ)
- balanced → 17,61 % (khoảng 1 trong 6 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 10.670đ với phủ số và 11.110đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

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
| random | 0.799 | 2,37 % | - | - | - |
| hot | 0.801 | 2,36 % | -0.006 | -0.207 đến +0.195 | 0.9521 |
| cold | 0.789 | 2,25 % | -0.115 | -0.314 đến +0.084 | 0.256 |
| balanced | 0.799 | 2,37 % | +0.006 | -0.060 đến +0.071 | 0.8685 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 18,88 % | 15,45 % đến 22,30 % | +1.450 | +1.003 đến +1.898 | 2.734E-10 |
| random | 17,43 % | 14,00 % đến 20,85 % | - | - | - |
| hot | 14,18 % | 10,75 % đến 17,61 % | -3.245 | -4.297 đến -2.194 | 1.766E-09 |
| cold | 13,46 % | 10,03 % đến 16,89 % | -3.969 | -5.036 đến -2.901 | 4.848E-13 |
| balanced | 17,61 % | 14,18 % đến 21,03 % | +0.181 | -0.276 đến +0.637 | 0.4373 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 10.670đ | -69.330đ | -86,66% | 1,09 % | 1,09 % |
| random | 11.110đ | -68.890đ | -86,11% | 1,13 % | 1,13 % |
| hot | 10.466đ | -69.534đ | -86,92% | 1,66 % | 1,66 % |
| cold | 9.483đ | -70.517đ | -88,15% | 1,52 % | 1,52 % |
| balanced | 10.747đ | -69.253đ | -86,57% | 1,17 % | 1,17 % |

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

- **cover → 10,64 % (khoảng 1 trong 9 kỳ)**
- random → 10,21 % (khoảng 1 trong 10 kỳ)
- hot → 8,56 % (khoảng 1 trong 12 kỳ)
- cold → 8,94 % (khoảng 1 trong 11 kỳ)
- balanced → 10,26 % (khoảng 1 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 10.103đ với phủ số và 15.249đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

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
| random | 0.655 | 1,34 % | - | - | - |
| hot | 0.654 | 1,36 % | +0.019 | -0.143 đến +0.181 | 0.8177 |
| cold | 0.660 | 1,41 % | +0.068 | -0.094 đến +0.230 | 0.4112 |
| balanced | 0.655 | 1,36 % | +0.022 | -0.031 đến +0.075 | 0.4103 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 10,64 % | 7,01 % đến 14,26 % | +0.427 | +0.070 đến +0.785 | 0.01909 |
| random | 10,21 % | 6,58 % đến 13,83 % | - | - | - |
| hot | 8,56 % | 4,94 % đến 12,19 % | -1.644 | -2.521 đến -0.767 | 0.0002447 |
| cold | 8,94 % | 5,31 % đến 12,56 % | -1.272 | -2.190 đến -0.355 | 0.006594 |
| balanced | 10,26 % | 6,63 % đến 13,88 % | +0.050 | -0.338 đến +0.438 | 0.8002 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 10.103đ | -69.897đ | -87,37% | 0,620 % | 0,620 % |
| random | 15.249đ | -64.751đ | -80,94% | 0,955 % | 0,955 % |
| hot | 10.980đ | -69.020đ | -86,27% | 2,01 % | 2,01 % |
| cold | 9.439đ | -70.561đ | -88,20% | 2,15 % | 2,15 % |
| balanced | 8.249đ | -71.751đ | -89,69% | 0,887 % | 0,887 % |

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

