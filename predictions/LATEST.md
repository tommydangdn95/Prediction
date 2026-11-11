# Gợi ý Vietlott — 11/11/2026

> **Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng.**
> Mọi bộ số — kể cả bộ ngẫu nhiên — có cùng xác suất trúng. Bộ gợi ý chỉ phản ánh đặc điểm dữ liệu quá khứ.

Tạo báo cáo lúc 19:15 11/11/2026 (giờ VN). Mỗi cách chọn là một phương án mua riêng.

## Lotto 5/35

- Kỳ mới nhất: **#01001** 11/11/2026 — `09 10 12 15 31 | 05`
- Lịch sử dùng để gợi ý: 1001 kỳ (#00001 → #01001)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 14,29 % số kỳ; thực tế từ 11,89 % (số 12) đến 16,68 % (số 22).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1001 kỳ: p = 0.0240 → có dấu hiệu lệch tần suất ở ngưỡng 5% chưa hiệu chỉnh nhiều phép thử.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 10/11/2026 → kỳ #01000 (10/11/2026): `04 08 12 21 26 | 05`

| Chiến lược | Bộ số | ĐB | Trùng | Kết quả |
|---|---|---|---|---|
| hot | 01 25 29 33 35 | **05** ✓ | 0/5 + ĐB | 🎉 Khuyến khích |
| hot | 10 20 22 **26** 29 | 01 | 1/5 | - |
| cold | 03 **08** 10 14 28 | 12 | 1/5 | - |
| cold | 07 **08** 19 24 32 | 03 | 1/5 | - |
| balanced | 01 02 **26** 33 34 | **05** ✓ | 1/5 + ĐB | 🎉 Khuyến khích |
| balanced | 06 **12** 16 **21** 25 | 04 | 2/5 | - |
| random | **12** 15 22 29 35 | 09 | 1/5 | - |
| random | 03 07 09 27 30 | 10 | 0/5 | - |
| cover | 01 14 23 24 **26** | 01 | 1/5 | - |
| cover | 02 09 10 22 32 | 12 | 0/5 | - |
| cover | **04** 06 07 13 20 | 10 | 1/5 | - |
| cover | **12** 18 **21** 25 35 | 07 | 2/5 | - |
| cover | 03 **08** 15 19 29 | **05** ✓ | 1/5 + ĐB | 🎉 Khuyến khích |
| cover | 05 17 30 33 34 | 04 | 0/5 | - |
| cover | 11 16 27 28 31 | 03 | 0/5 | - |
| cover | 11 **12** 19 20 23 | 06 | 1/5 | - |

Kỳ này: 13 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 11.4), 3 bộ trúng giải (kỳ vọng 1.5).

<details><summary>Thành tích thực tế cộng dồn (1 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng | Trùng ĐB |
|---|---:|---:|---:|---:|---:|---:|
| balanced | 2 | 1.500 | 0.714 | 1 | 0.2 | 1/2 |
| cold | 2 | 1.000 | 0.714 | 0 | 0.2 | 0/2 |
| cover | 8 | 0.750 | 0.714 | 1 | 0.8 | 1/8 |
| hot | 2 | 0.500 | 0.714 | 1 | 0.2 | 1/2 |
| random | 2 | 0.500 | 0.714 | 0 | 0.2 | 0/2 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01002**, bắt đầu 21:00 11/11/2026 (giờ VN).
- Bộ số tạo lúc 15:48:56 11/11/2026 · seed `20261111` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **45,71 %**, tức khoảng 1 trong 2 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 39,64 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 9,60 %.

| Vé | Bộ số | ĐB | Mua từ vé 1 đến vé này: có ≥1 vé trúng | …có ≥1 vé trùng ≥3 số | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---|---:|---:|---:|
| cover 1 | `03 06 08 12 18` | `01` | **9,60 %** | 1,39 % | 9,60 % |
| cover 2 | `14 21 22 29 33` | `08` | **18,98 %** | 2,77 % | 18,29 % |
| cover 3 | `01 17 26 34 35` | `10` | **28,12 %** | 4,16 % | 26,13 % |
| cover 4 | `02 07 09 19 28` | `12` | **37,03 %** | 5,55 % | 33,23 % |
| cover 5 | `05 10 15 31 32` | `05` | **45,71 %** | 6,93 % | 39,64 % |
| cover 6 | `04 11 20 27 30` | `06` | **54,16 %** | 8,32 % | 45,44 % |
| cover 7 | `13 16 23 24 25` | `07` | **62,38 %** | 9,71 % | 50,68 % |
| cover 8 | `09 11 13 26 31` | `03` | **70,35 %** | 11,04 % | 55,42 % |

- Với Lotto 5/35: giải Khuyến khích (0–2 số + ĐB) chiếm 86 % số lần trúng và chỉ cần trùng số ĐB (01–12), nên số ĐB khác nhau là lợi thế chính: 5 vé phủ 5/12 khả năng của số ĐB; 12 vé có đủ 12 số ĐB thì kỳ nào cũng có vé trúng, nhưng vẫn có thể lỗ tiền.
- Độc đắc (5 số + ĐB): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 486.948, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 981 kỳ đã quay (#00021 → #01001) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 70,03 % (khoảng 7 trong 10 kỳ)**
- random → 55,11 % (khoảng 6 trong 10 kỳ)
- hot → 34,91 % (khoảng 1 trong 3 kỳ)
- cold → 36,63 % (khoảng 1 trong 3 kỳ)
- balanced → 55,36 % (khoảng 6 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 13.943đ với phủ số và 12.931đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | ĐB | Chẵn/Lẻ | Nhỏ/Lớn (≤17) | Tổng |
|---|---|---|---|---|---:|
| hot | `02 04 08 18 26` | `06` | 5/0 | 3/2 | 58 |
| hot | `02 04 08 15 35` | `05` | 3/2 | 4/1 | 64 |
| cold | `07 17 19 30 32` | `09` | 2/3 | 2/3 | 105 |
| cold | `03 07 14 17 20` | `02` | 2/3 | 4/1 | 61 |
| balanced | `06 11 23 30 35` | `12` | 2/3 | 2/3 | 105 |
| balanced | `06 16 23 24 33` | `11` | 3/2 | 2/3 | 102 |
| random (đối chứng) | `05 07 12 17 34` | `05` | 2/3 | 4/1 | 75 |
| random (đối chứng) | `04 07 19 27 30` | `04` | 2/3 | 2/3 | 87 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **9,60 %** · trúng ≥3 số **1,39 %** · Độc đắc **0,0000257 %** (1 / 3.895.584).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 |
|---|---:|---:|---:|---:|---:|---:|
| Xác suất | 43,90 % | 42,21 % | 12,51 % | 1,34 % | 0,0462 % | 0,000308 % |

Số ĐB trùng: 8,33 % (độc lập với số chính).

<details><summary>Backtest walk-forward</summary>

**Từng vé: 981 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.715 | 9,60 % | - | - | - |
| hot | 0.710 | 9,19 % | -0.412 | -1.164 đến +0.339 | 0.2821 |
| cold | 0.723 | 9,53 % | -0.073 | -0.820 đến +0.674 | 0.8472 |
| balanced | 0.717 | 9,64 % | +0.039 | -0.114 đến +0.193 | 0.6142 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 70,03 % | 65,69 % đến 74,36 % | +14.910 | +14.205 đến +15.615 | 5.059E-218 |
| random | 55,11 % | 50,78 % đến 59,45 % | - | - | - |
| hot | 34,91 % | 30,57 % đến 39,25 % | -20.206 | -22.771 đến -17.640 | 2.155E-48 |
| cold | 36,63 % | 32,29 % đến 40,96 % | -18.489 | -21.067 đến -15.911 | 4.183E-41 |
| balanced | 55,36 % | 51,02 % đến 59,69 % | +0.242 | -0.469 đến +0.954 | 0.504 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 13.943đ | -66.057đ | -82,57% | 1,17 % | 1,17 % |
| random | 12.931đ | -67.069đ | -83,84% | 1,26 % | 1,25 % |
| hot | 12.183đ | -67.817đ | -84,77% | 1,26 % | 1,17 % |
| cold | 15.069đ | -64.931đ | -81,16% | 1,34 % | 1,28 % |
| balanced | 13.762đ | -66.238đ | -82,80% | 1,26 % | 1,23 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 981 kỳ: 78.480.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01001 | 11/11/2026 | `09 10 12 15 31 | 05` | - |
| #01000 | 10/11/2026 | `04 08 12 21 26 | 05` | - |
| #00999 | 10/11/2026 | `01 05 18 26 33 | 07` | - |
| #00998 | 09/11/2026 | `06 15 22 27 31 | 01` | - |
| #00997 | 09/11/2026 | `04 16 26 27 29 | 06` | - |

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

- **cover → 18,93 % (khoảng 1 trong 5 kỳ)**
- random → 17,47 % (khoảng 1 trong 6 kỳ)
- hot → 14,19 % (khoảng 1 trong 7 kỳ)
- cold → 13,82 % (khoảng 1 trong 7 kỳ)
- balanced → 17,63 % (khoảng 1 trong 6 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 11.092đ với phủ số và 10.926đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

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
| random | 0.801 | 2,38 % | - | - | - |
| hot | 0.800 | 2,37 % | -0.016 | -0.220 đến +0.188 | 0.878 |
| cold | 0.789 | 2,28 % | -0.099 | -0.299 đến +0.100 | 0.3284 |
| balanced | 0.801 | 2,38 % | -0.003 | -0.066 đến +0.061 | 0.9368 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 18,93 % | 15,50 % đến 22,36 % | +1.457 | +1.010 đến +1.905 | 2.161E-10 |
| random | 17,47 % | 14,04 % đến 20,90 % | - | - | - |
| hot | 14,19 % | 10,76 % đến 17,62 % | -3.279 | -4.325 đến -2.233 | 9.925E-10 |
| cold | 13,82 % | 10,38 % đến 17,25 % | -3.655 | -4.719 đến -2.591 | 2.247E-11 |
| balanced | 17,63 % | 14,20 % đến 21,06 % | +0.164 | -0.301 đến +0.629 | 0.4895 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 11.092đ | -68.908đ | -86,13% | 1,11 % | 1,11 % |
| random | 10.926đ | -69.074đ | -86,34% | 1,12 % | 1,12 % |
| hot | 11.038đ | -68.962đ | -86,20% | 1,59 % | 1,59 % |
| cold | 10.032đ | -69.968đ | -87,46% | 1,53 % | 1,53 % |
| balanced | 10.522đ | -69.478đ | -86,85% | 1,14 % | 1,14 % |

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

- **cover → 10,66 % (khoảng 1 trong 9 kỳ)**
- random → 10,11 % (khoảng 1 trong 10 kỳ)
- hot → 8,61 % (khoảng 1 trong 12 kỳ)
- cold → 8,99 % (khoảng 1 trong 11 kỳ)
- balanced → 10,08 % (khoảng 1 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 10.786đ với phủ số và 9.870đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

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
| random | 0.655 | 1,32 % | - | - | - |
| hot | 0.655 | 1,37 % | +0.047 | -0.112 đến +0.206 | 0.5631 |
| cold | 0.659 | 1,49 % | +0.160 | -0.007 đến +0.327 | 0.05992 |
| balanced | 0.654 | 1,34 % | +0.013 | -0.038 đến +0.064 | 0.6214 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 10,66 % | 7,03 % đến 14,29 % | +0.549 | +0.189 đến +0.910 | 0.002826 |
| random | 10,11 % | 6,48 % đến 13,74 % | - | - | - |
| hot | 8,61 % | 4,98 % đến 12,24 % | -1.500 | -2.387 đến -0.613 | 0.0009314 |
| cold | 8,99 % | 5,37 % đến 12,62 % | -1.114 | -2.028 đến -0.200 | 0.0169 |
| balanced | 10,08 % | 6,46 % đến 13,71 % | -0.027 | -0.404 đến +0.350 | 0.8883 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 10.786đ | -69.214đ | -86,52% | 0,580 % | 0,580 % |
| random | 9.870đ | -70.130đ | -87,66% | 0,889 % | 0,889 % |
| hot | 10.577đ | -69.423đ | -86,78% | 1,99 % | 1,99 % |
| cold | 9.793đ | -70.207đ | -87,76% | 2,24 % | 2,24 % |
| balanced | 12.988đ | -67.012đ | -83,76% | 0,968 % | 0,968 % |

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

