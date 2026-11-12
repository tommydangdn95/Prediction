# Gợi ý Vietlott — 12/11/2026

> **Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng.**
> Mọi bộ số — kể cả bộ ngẫu nhiên — có cùng xác suất trúng. Bộ gợi ý chỉ phản ánh đặc điểm dữ liệu quá khứ.

Tạo báo cáo lúc 22:50 12/11/2026 (giờ VN). Mỗi cách chọn là một phương án mua riêng.

## Lotto 5/35

- Kỳ mới nhất: **#01004** 12/11/2026 — `02 05 07 09 22 | 10`
- Lịch sử dùng để gợi ý: 1004 kỳ (#00001 → #01004)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 14,29 % số kỳ; thực tế từ 11,85 % (số 12) đến 16,73 % (số 22).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1004 kỳ: p = 0.0248 → có dấu hiệu lệch tần suất ở ngưỡng 5% chưa hiệu chỉnh nhiều phép thử.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 12/11/2026 → kỳ #01004 (12/11/2026): `02 05 07 09 22 | 10`

| Chiến lược | Bộ số | ĐB | Trùng | Kết quả |
|---|---|---|---|---|
| hot | 04 **22** 27 33 35 | 06 | 1/5 | - |
| hot | **02** 06 12 **22** 35 | 11 | 2/5 | - |
| cold | 03 06 16 23 29 | 02 | 0/5 | - |
| cold | 13 23 30 34 35 | 02 | 0/5 | - |
| balanced | **02** 08 21 29 34 | 12 | 1/5 | - |
| balanced | 08 13 26 27 29 | 07 | 0/5 | - |
| random | 20 **22** 23 26 29 | 04 | 1/5 | - |
| random | 03 06 20 25 35 | 09 | 0/5 | - |
| cover | 06 17 26 29 32 | **10** ✓ | 0/5 + ĐB | 🎉 Khuyến khích |
| cover | 08 14 20 24 31 | 06 | 0/5 | - |
| cover | **02** **09** 28 33 35 | 03 | 2/5 | - |
| cover | 04 10 12 18 19 | 01 | 0/5 | - |
| cover | **07** 11 13 21 25 | 11 | 1/5 | - |
| cover | 03 **05** 16 23 27 | 02 | 1/5 | - |
| cover | 01 15 **22** 30 34 | 05 | 1/5 | - |
| cover | 08 **09** 15 19 23 | 07 | 1/5 | - |

Kỳ này: 11 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 11.4), 1 bộ trúng giải (kỳ vọng 1.5).

<details><summary>Thành tích thực tế cộng dồn (4 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng | Trùng ĐB |
|---|---:|---:|---:|---:|---:|---:|
| balanced | 8 | 0.875 | 0.714 | 2 | 0.8 | 2/8 |
| cold | 8 | 0.750 | 0.714 | 0 | 0.8 | 0/8 |
| cover | 32 | 0.719 | 0.714 | 3 | 3.1 | 3/32 |
| hot | 8 | 0.875 | 0.714 | 2 | 0.8 | 2/8 |
| random | 8 | 0.750 | 0.714 | 1 | 0.8 | 0/8 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01005**, bắt đầu 13:00 13/11/2026 (giờ VN).
- Bộ số tạo lúc 22:50:33 12/11/2026 · seed `20261112` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **45,71 %**, tức khoảng 1 trong 2 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 39,64 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 9,60 %.

| Vé | Bộ số | ĐB | Mua từ vé 1 đến vé này: có ≥1 vé trúng | …có ≥1 vé trùng ≥3 số | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---|---:|---:|---:|
| cover 1 | `06 17 26 29 32` | `10` | **9,60 %** | 1,39 % | 9,60 % |
| cover 2 | `08 14 20 24 31` | `06` | **18,98 %** | 2,77 % | 18,29 % |
| cover 3 | `02 09 28 33 35` | `03` | **28,12 %** | 4,16 % | 26,13 % |
| cover 4 | `04 10 12 18 19` | `01` | **37,03 %** | 5,55 % | 33,23 % |
| cover 5 | `07 11 13 21 25` | `11` | **45,71 %** | 6,93 % | 39,64 % |
| cover 6 | `03 05 16 23 27` | `02` | **54,16 %** | 8,32 % | 45,44 % |
| cover 7 | `01 15 22 30 34` | `05` | **62,38 %** | 9,71 % | 50,68 % |
| cover 8 | `08 09 15 19 23` | `07` | **70,35 %** | 11,04 % | 55,42 % |

- Với Lotto 5/35: giải Khuyến khích (0–2 số + ĐB) chiếm 86 % số lần trúng và chỉ cần trùng số ĐB (01–12), nên số ĐB khác nhau là lợi thế chính: 5 vé phủ 5/12 khả năng của số ĐB; 12 vé có đủ 12 số ĐB thì kỳ nào cũng có vé trúng, nhưng vẫn có thể lỗ tiền.
- Độc đắc (5 số + ĐB): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 486.948, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 984 kỳ đã quay (#00021 → #01004) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 70,61 % (khoảng 7 trong 10 kỳ)**
- random → 55,55 % (khoảng 6 trong 10 kỳ)
- hot → 34,56 % (khoảng 1 trong 3 kỳ)
- cold → 36,51 % (khoảng 1 trong 3 kỳ)
- balanced → 55,31 % (khoảng 6 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 13.164đ với phủ số và 13.408đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | ĐB | Chẵn/Lẻ | Nhỏ/Lớn (≤17) | Tổng |
|---|---|---|---|---|---:|
| hot | `04 07 25 27 33` | `06` | 1/4 | 2/3 | 96 |
| hot | `01 02 07 25 35` | `11` | 1/4 | 3/2 | 70 |
| cold | `03 06 18 23 29` | `02` | 2/3 | 2/3 | 79 |
| cold | `11 23 30 34 35` | `02` | 2/3 | 1/4 | 133 |
| balanced | `02 08 21 29 34` | `12` | 3/2 | 2/3 | 94 |
| balanced | `08 13 26 27 29` | `07` | 2/3 | 2/3 | 103 |
| random (đối chứng) | `20 22 23 26 29` | `04` | 3/2 | 0/5 | 120 |
| random (đối chứng) | `03 06 20 25 35` | `09` | 2/3 | 2/3 | 89 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **9,60 %** · trúng ≥3 số **1,39 %** · Độc đắc **0,0000257 %** (1 / 3.895.584).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 |
|---|---:|---:|---:|---:|---:|---:|
| Xác suất | 43,90 % | 42,21 % | 12,51 % | 1,34 % | 0,0462 % | 0,000308 % |

Số ĐB trùng: 8,33 % (độc lập với số chính).

<details><summary>Backtest walk-forward</summary>

**Từng vé: 984 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.712 | 9,57 % | - | - | - |
| hot | 0.707 | 9,03 % | -0.539 | -1.291 đến +0.212 | 0.1594 |
| cold | 0.722 | 9,58 % | +0.012 | -0.736 đến +0.759 | 0.9759 |
| balanced | 0.715 | 9,58 % | +0.007 | -0.144 đến +0.159 | 0.9229 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 70,61 % | 66,28 % đến 74,93 % | +15.060 | +14.348 đến +15.772 | 1.902E-218 |
| random | 55,55 % | 51,22 % đến 59,87 % | - | - | - |
| hot | 34,56 % | 30,23 % đến 38,89 % | -20.987 | -23.575 đến -18.400 | 6.295E-51 |
| cold | 36,51 % | 32,18 % đến 40,84 % | -19.037 | -21.620 đến -16.454 | 3.91E-43 |
| balanced | 55,31 % | 50,99 % đến 59,64 % | -0.231 | -0.949 đến +0.487 | 0.5285 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 13.164đ | -66.836đ | -83,55% | 1,23 % | 1,23 % |
| random | 13.408đ | -66.592đ | -83,24% | 1,30 % | 1,28 % |
| hot | 12.374đ | -67.626đ | -84,53% | 1,25 % | 1,15 % |
| cold | 14.425đ | -65.575đ | -81,97% | 1,50 % | 1,41 % |
| balanced | 14.167đ | -65.833đ | -82,29% | 1,37 % | 1,33 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 984 kỳ: 78.720.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01004 | 12/11/2026 | `02 05 07 09 22 | 10` | - |
| #01003 | 12/11/2026 | `04 08 10 17 33 | 11` | - |
| #01002 | 11/11/2026 | `07 19 24 25 27 | 11` | - |
| #01001 | 11/11/2026 | `09 10 12 15 31 | 05` | - |
| #01000 | 10/11/2026 | `04 08 12 21 26 | 05` | - |

</details>

## Mega 6/45

- Kỳ mới nhất: **#01587** 11/11/2026 — `03 11 14 23 25 43`
- Lịch sử dùng để gợi ý: 1587 kỳ (#00001 → #01587)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 13,33 % số kỳ; thực tế từ 11,66 % (số 38) đến 14,56 % (số 37).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1587 kỳ: p = 0.9315 → chưa bác bỏ giả thuyết tần suất đều.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 10/11/2026 → kỳ #01587 (11/11/2026): `03 11 14 23 25 43`

| Chiến lược | Bộ số | Trùng | Kết quả |
|---|---|---|---|
| hot | 02 09 20 34 38 40 | 0/6 | - |
| hot | **11** **14** 33 34 36 40 | 2/6 | - |
| cold | 06 12 16 19 29 44 | 0/6 | - |
| cold | 06 08 12 16 32 **43** | 1/6 | - |
| balanced | **14** 18 19 30 31 39 | 1/6 | - |
| balanced | 05 08 20 28 29 35 | 0/6 | - |
| random | 06 12 **14** 15 31 45 | 1/6 | - |
| random | 01 **03** 08 13 20 41 | 1/6 | - |
| cover | 13 27 29 37 38 39 | 0/6 | - |
| cover | 01 02 07 18 32 44 | 0/6 | - |
| cover | 09 15 19 24 30 **43** | 1/6 | - |
| cover | **03** 17 20 31 33 36 | 1/6 | - |
| cover | 10 **11** 12 16 22 35 | 1/6 | - |
| cover | 04 06 08 **14** 21 40 | 1/6 | - |
| cover | 05 **25** 26 41 42 45 | 1/6 | - |
| cover | **23** 24 **25** 28 34 40 | 2/6 | - |

Kỳ này: 13 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 12.8), 0 bộ trúng giải (kỳ vọng 0.4).

<details><summary>Thành tích thực tế cộng dồn (1 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng |
|---|---:|---:|---:|---:|---:|
| balanced | 2 | 0.500 | 0.800 | 0 | 0.0 |
| cold | 2 | 0.500 | 0.800 | 0 | 0.0 |
| cover | 8 | 0.875 | 0.800 | 0 | 0.2 |
| hot | 2 | 1.000 | 0.800 | 0 | 0.0 |
| random | 2 | 1.000 | 0.800 | 0 | 0.0 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01588**, bắt đầu 18:00 13/11/2026 (giờ VN).
- Bộ số tạo lúc 19:15:32 11/11/2026 · seed `20261111` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **11,87 %**, tức khoảng 1 trong 8 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 11,36 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 2,38 %.

| Vé | Bộ số | Mua từ vé 1 đến vé này: có ≥1 vé trúng | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---:|---:|
| cover 1 | `19 26 29 36 42 43` | **2,38 %** | 2,38 % |
| cover 2 | `01 06 18 22 23 38` | **4,76 %** | 4,71 % |
| cover 3 | `09 17 28 39 40 41` | **7,14 %** | 6,98 % |
| cover 4 | `02 08 27 31 33 37` | **9,50 %** | 9,20 % |
| cover 5 | `12 15 20 21 30 35` | **11,87 %** | 11,36 % |
| cover 6 | `04 10 13 24 32 44` | **14,23 %** | 13,47 % |
| cover 7 | `07 11 16 25 34 45` | **16,58 %** | 15,54 % |
| cover 8 | `03 05 14 20 23 27` | **18,81 %** | 17,55 % |

- Với Mega 6/45: vé không có số ĐB và 94 % số lần trúng là Giải Ba (3 số). So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.
- Jackpot (6 số): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 1.018.132, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 1.567 kỳ đã quay (#00021 → #01587) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 18,55 % (khoảng 1 trong 5 kỳ)**
- random → 17,49 % (khoảng 1 trong 6 kỳ)
- hot → 14,14 % (khoảng 1 trong 7 kỳ)
- cold → 13,54 % (khoảng 1 trong 7 kỳ)
- balanced → 17,54 % (khoảng 1 trong 6 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 10.670đ với phủ số và 11.264đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | Chẵn/Lẻ | Nhỏ/Lớn (≤22) | Tổng |
|---|---|---|---|---:|
| hot | `02 03 10 11 14 36` | 4/2 | 5/1 | 76 |
| hot | `03 20 23 25 41 43` | 1/5 | 2/4 | 155 |
| cold | `06 08 12 27 32 35` | 4/2 | 3/3 | 120 |
| cold | `06 12 27 29 31 33` | 2/4 | 2/4 | 138 |
| balanced | `02 05 21 24 34 37` | 3/3 | 3/3 | 123 |
| balanced | `01 09 11 32 34 36` | 3/3 | 3/3 | 123 |
| random (đối chứng) | `06 10 19 31 33 37` | 2/4 | 3/3 | 136 |
| random (đối chứng) | `01 06 13 18 32 39` | 3/3 | 4/2 | 109 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **2,38 %** · trúng ≥3 số **2,38 %** · Jackpot **0,0000123 %** (1 / 8.145.060).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 | 6 |
|---|---:|---:|---:|---:|---:|---:|---:|
| Xác suất | 40,06 % | 42,41 % | 15,15 % | 2,24 % | 0,136 % | 0,00287 % | 0,0000123 % |

<details><summary>Backtest walk-forward</summary>

**Từng vé: 1567 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.799 | 2,40 % | - | - | - |
| hot | 0.802 | 2,35 % | -0.052 | -0.257 đến +0.153 | 0.6169 |
| cold | 0.789 | 2,25 % | -0.152 | -0.353 đến +0.049 | 0.1391 |
| balanced | 0.798 | 2,36 % | -0.039 | -0.104 đến +0.027 | 0.244 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 18,55 % | 15,12 % đến 21,98 % | +1.062 | +0.623 đến +1.502 | 2.343E-06 |
| random | 17,49 % | 14,05 % đến 20,92 % | - | - | - |
| hot | 14,14 % | 10,71 % đến 17,57 % | -3.346 | -4.415 đến -2.277 | 1.056E-09 |
| cold | 13,54 % | 10,11 % đến 16,97 % | -3.948 | -4.999 đến -2.897 | 2.771E-13 |
| balanced | 17,54 % | 14,11 % đến 20,97 % | +0.053 | -0.411 đến +0.518 | 0.8213 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 10.670đ | -69.330đ | -86,66% | 1,12 % | 1,12 % |
| random | 11.264đ | -68.736đ | -85,92% | 1,18 % | 1,18 % |
| hot | 10.837đ | -69.163đ | -86,45% | 1,63 % | 1,63 % |
| cold | 9.981đ | -70.019đ | -87,52% | 1,53 % | 1,53 % |
| balanced | 10.736đ | -69.264đ | -86,58% | 1,22 % | 1,22 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 1567 kỳ: 125.360.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01587 | 11/11/2026 | `03 11 14 23 25 43` | 17.638.002.500đ |
| #01586 | 08/11/2026 | `01 02 14 20 23 36` | 15.740.836.000đ |
| #01585 | 06/11/2026 | `01 10 37 38 39 45` | 13.783.788.500đ |
| #01584 | 04/11/2026 | `07 14 20 30 34 40` | 26.087.005.500đ |
| #01583 | 01/11/2026 | `05 09 17 18 34 36` | 23.200.118.500đ |

</details>

## Power 6/55

- Kỳ mới nhất: **#01423** 12/11/2026 — `17 25 33 34 35 36 | 48`
- Lịch sử dùng để gợi ý: 1423 kỳ (#00001 → #01423)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 10,91 % số kỳ; thực tế từ 8,85 % (số 04) đến 12,72 % (số 22).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1423 kỳ: p = 0.3769 → chưa bác bỏ giả thuyết tần suất đều.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 10/11/2026 → kỳ #01423 (12/11/2026): `17 25 33 34 35 36 | 48`

| Chiến lược | Bộ số | Trùng | Kết quả |
|---|---|---|---|
| hot | 01 05 09 11 18 39 | 0/6 | - |
| hot | 05 07 09 23 27 55 | 0/6 | - |
| cold | 11 14 **17** 37 40 47 | 1/6 | - |
| cold | 03 11 13 14 32 47 | 0/6 | - |
| balanced | 16 **17** **25** **33** 38 42 | 3/6 | 🎉 Giải Ba |
| balanced | **17** 24 26 **33** 37 54 | 2/6 | - |
| random | **17** 18 24 40 43 54 | 1/6 | - |
| random | 01 08 10 26 28 31 | 0/6 | - |
| cover | 03 11 13 16 24 45 | 0/6 | - |
| cover | 04 12 20 **33** **34** **36** | 3/6 | 🎉 Giải Ba |
| cover | 06 10 19 21 38 53 | 0/6 | - |
| cover | 01 09 18 **35** 46 49 | 1/6 | - |
| cover | 31 37 41 47 50 52 | 0/6 | - |
| cover | 07 23 **25** 32 44 51 | 1/6 | - |
| cover | 02 27 28 29 39 40 | 0/6 | - |
| cover | 05 15 **17** 22 43 55 | 1/6 | - |

Kỳ này: 13 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 10.5), 2 bộ trúng giải (kỳ vọng 0.2).

<details><summary>Thành tích thực tế cộng dồn (2 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng |
|---|---:|---:|---:|---:|---:|
| balanced | 4 | 1.500 | 0.655 | 1 | 0.1 |
| cold | 4 | 1.000 | 0.655 | 0 | 0.1 |
| cover | 16 | 0.750 | 0.655 | 1 | 0.2 |
| hot | 4 | 0.000 | 0.655 | 0 | 0.1 |
| random | 4 | 0.500 | 0.655 | 0 | 0.1 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01424**, bắt đầu 18:00 14/11/2026 (giờ VN).
- Bộ số tạo lúc 19:29:36 12/11/2026 · seed `20261112` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **6,65 %**, tức khoảng 1 trong 15 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 6,49 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 1,33 %.

| Vé | Bộ số | Mua từ vé 1 đến vé này: có ≥1 vé trúng | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---:|---:|
| cover 1 | `09 17 19 31 41 46` | **1,33 %** | 1,33 % |
| cover 2 | `05 07 16 22 34 51` | **2,66 %** | 2,65 % |
| cover 3 | `06 13 21 26 33 53` | **3,99 %** | 3,95 % |
| cover 4 | `02 10 29 30 35 55` | **5,32 %** | 5,23 % |
| cover 5 | `14 20 24 42 45 50` | **6,65 %** | 6,49 % |
| cover 6 | `08 11 28 32 43 52` | **7,98 %** | 7,74 % |
| cover 7 | `04 36 40 44 47 49` | **9,30 %** | 8,97 % |
| cover 8 | `12 15 23 38 39 48` | **10,62 %** | 10,18 % |

- Với Power 6/55: số ĐB là quả thứ 7 quay cùng lồng, chỉ dùng cho giải 5 số + ĐB, nên không có giải nào chỉ cần trùng ĐB; 95 % xác suất trúng giải của một vé thuộc Giải Ba (3 số). So sánh lợi ích của nhóm bằng các tỷ lệ ở bảng trên.
- Jackpot 1 (6 số): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 3.623.709, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 1.403 kỳ đã quay (#00021 → #01423) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 10,85 % (khoảng 1 trong 9 kỳ)**
- random → 10,03 % (khoảng 1 trong 10 kỳ)
- hot → 8,51 % (khoảng 1 trong 12 kỳ)
- cold → 8,98 % (khoảng 1 trong 11 kỳ)
- balanced → 10,16 % (khoảng 1 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 13.949đ với phủ số và 12.163đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | Chẵn/Lẻ | Nhỏ/Lớn (≤27) | Tổng |
|---|---|---|---|---:|
| hot | `02 11 14 25 34 38` | 4/2 | 4/2 | 124 |
| hot | `01 02 14 18 23 34` | 4/2 | 5/1 | 92 |
| cold | `08 15 16 23 24 48` | 4/2 | 5/1 | 134 |
| cold | `11 30 32 37 40 47` | 3/3 | 1/5 | 197 |
| balanced | `10 16 25 41 46 53` | 3/3 | 3/3 | 191 |
| balanced | `10 11 12 35 40 43` | 3/3 | 3/3 | 151 |
| random (đối chứng) | `15 17 21 23 34 54` | 2/4 | 4/2 | 164 |
| random (đối chứng) | `01 02 36 47 51 55` | 2/4 | 2/4 | 192 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **1,33 %** · trúng ≥3 số **1,33 %** · Jackpot 1 **0,00000345 %** (1 / 28.989.675).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 | 6 |
|---|---:|---:|---:|---:|---:|---:|---:|
| Xác suất | 48,24 % | 39,47 % | 10,96 % | 1,27 % | 0,0608 % | 0,00101 % | 0,00000345 % |

<details><summary>Backtest walk-forward</summary>

**Từng vé: 1403 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.655 | 1,35 % | - | - | - |
| hot | 0.658 | 1,38 % | +0.031 | -0.130 đến +0.192 | 0.7065 |
| cold | 0.660 | 1,46 % | +0.114 | -0.049 đến +0.277 | 0.1718 |
| balanced | 0.654 | 1,34 % | -0.006 | -0.058 đến +0.046 | 0.8216 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 10,85 % | 7,22 % đến 14,47 % | +0.811 | +0.448 đến +1.174 | 1.292E-05 |
| random | 10,03 % | 6,41 % đến 13,66 % | - | - | - |
| hot | 8,51 % | 4,89 % đến 12,14 % | -1.520 | -2.394 đến -0.646 | 0.0006677 |
| cold | 8,98 % | 5,35 % đến 12,60 % | -1.058 | -1.981 đến -0.134 | 0.02489 |
| balanced | 10,16 % | 6,53 % đến 13,78 % | +0.123 | -0.251 đến +0.498 | 0.5184 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 13.949đ | -66.051đ | -82,56% | 0,578 % | 0,578 % |
| random | 12.163đ | -67.837đ | -84,80% | 0,965 % | 0,965 % |
| hot | 12.227đ | -67.773đ | -84,72% | 2,02 % | 2,02 % |
| cold | 9.782đ | -70.218đ | -87,77% | 2,29 % | 2,29 % |
| balanced | 12.798đ | -67.202đ | -84,00% | 0,957 % | 0,957 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 1403 kỳ: 112.240.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01423 | 12/11/2026 | `17 25 33 34 35 36 | 48` | 35.115.629.250đ |
| #01422 | 10/11/2026 | `01 02 21 38 39 51 | 12` | 32.688.020.100đ |
| #01421 | 07/11/2026 | `05 19 20 26 44 53 | 39` | 22.636.747.050đ |
| #01420 | 05/11/2026 | `06 20 34 41 49 52 | 36` | 42.432.448.800đ |
| #01419 | 03/11/2026 | `10 22 31 39 43 54 | 14` | 39.296.824.950đ |

</details>

