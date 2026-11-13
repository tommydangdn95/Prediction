# Gợi ý Vietlott — 13/11/2026

> **Mỗi kỳ quay là độc lập. Thống kê quá khứ không làm tăng xác suất trúng.**
> Mọi bộ số — kể cả bộ ngẫu nhiên — có cùng xác suất trúng. Bộ gợi ý chỉ phản ánh đặc điểm dữ liệu quá khứ.

Tạo báo cáo lúc 14:21 13/11/2026 (giờ VN). Mỗi cách chọn là một phương án mua riêng.

## Lotto 5/35

- Kỳ mới nhất: **#01005** 13/11/2026 — `04 23 27 28 35 | 03`
- Lịch sử dùng để gợi ý: 1005 kỳ (#00001 → #01005)
- Tỉ lệ một số bất kỳ có mặt trong kết quả: kỳ vọng 14,29 % số kỳ; thực tế từ 11,84 % (số 12) đến 16,72 % (số 22).
- Kiểm định tần suất χ² đã hiệu chỉnh rút không hoàn lại trên 1005 kỳ: p = 0.0239 → có dấu hiệu lệch tần suất ở ngưỡng 5% chưa hiệu chỉnh nhiều phép thử.
  Đây là kiểm định tần suất riêng lẻ, không chứng minh tính độc lập hay khả năng dự đoán. p là xấp xỉ; cần thận trọng khi ít dữ liệu và khi thử nhiều game/nhiều lần.

### Kiểm tra gợi ý ngày 12/11/2026 → kỳ #01005 (13/11/2026): `04 23 27 28 35 | 03`

| Chiến lược | Bộ số | ĐB | Trùng | Kết quả |
|---|---|---|---|---|
| hot | **04** 07 25 **27** 33 | 06 | 2/5 | - |
| hot | 01 02 07 25 **35** | 11 | 1/5 | - |
| cold | 03 06 18 **23** 29 | 02 | 1/5 | - |
| cold | 11 **23** 30 34 **35** | 02 | 2/5 | - |
| balanced | 02 08 21 29 34 | 12 | 0/5 | - |
| balanced | 08 13 26 **27** 29 | 07 | 1/5 | - |
| random | 20 22 **23** 26 29 | 04 | 1/5 | - |
| random | 03 06 20 25 **35** | 09 | 1/5 | - |
| cover | 06 17 26 29 32 | 10 | 0/5 | - |
| cover | 08 14 20 24 31 | 06 | 0/5 | - |
| cover | 02 09 **28** 33 **35** | **03** ✓ | 2/5 + ĐB | 🎉 Khuyến khích |
| cover | **04** 10 12 18 19 | 01 | 1/5 | - |
| cover | 07 11 13 21 25 | 11 | 0/5 | - |
| cover | 03 05 16 **23** **27** | 02 | 2/5 | - |
| cover | 01 15 22 30 34 | 05 | 0/5 | - |
| cover | 08 09 15 19 **23** | 07 | 1/5 | - |

Kỳ này: 15 số trùng trên 16 bộ (kỳ vọng ngẫu nhiên 11.4), 1 bộ trúng giải (kỳ vọng 1.5).

<details><summary>Thành tích thực tế cộng dồn (5 kỳ đã đối chiếu)</summary>

| Chiến lược | Số bộ | Trùng TB/bộ | Kỳ vọng | Trúng giải | Kỳ vọng | Trùng ĐB |
|---|---:|---:|---:|---:|---:|---:|
| balanced | 10 | 0.800 | 0.714 | 2 | 1.0 | 2/10 |
| cold | 10 | 0.900 | 0.714 | 0 | 1.0 | 0/10 |
| cover | 40 | 0.725 | 0.714 | 4 | 3.8 | 4/40 |
| hot | 10 | 1.000 | 0.714 | 2 | 1.0 | 2/10 |
| random | 10 | 0.800 | 0.714 | 1 | 1.0 | 0/10 |

Chỉ tính bản đầu tiên mỗi kỳ đã lưu trước giờ quay và khớp lịch sử đầu vào. Thời gian trong file không thay thế bằng chứng công bố độc lập; có thể đối chiếu commit trước giờ quay. Chưa thể suy ra lợi thế dự đoán từ chênh lệch nhỏ trên ít kỳ.

</details>

- Kỳ mục tiêu: **#01006**, bắt đầu 21:00 13/11/2026 (giờ VN).
- Bộ số tạo lúc 14:21:59 13/11/2026 · seed `20261113` · `sets=2;cover=8;unique-v2`. Chạy lại cùng kỳ giữ nguyên bộ đã lưu.

### Phương án phủ số (8 vé)

Các vé giảm trùng số và phân bố số ĐB nếu có. Xác suất dưới đây được tính chính xác cho nhóm này theo mô hình quay đều, độc lập; chưa có chứng minh đây là nhóm tối ưu.
Chi phí 8 vé: **80.000đ**. Trúng giải bất kỳ không đồng nghĩa hòa vốn hoặc có lời.

**Cách đọc cột %:** số ở mỗi dòng là khả năng có **ít nhất 1 vé trúng giải** nếu bạn mua **từ vé 1 đến vé ở dòng đó**. Ví dụ mua 5 vé → mua cover 1–5 → xem dòng cover 5: **45,71 %**, tức khoảng 1 trong 2 kỳ có vé trúng (5 vé ngẫu nhiên khác nhau: 39,64 %). Đó không phải khả năng của riêng vé ở dòng đó: mỗi vé riêng lẻ vẫn chỉ trúng 9,60 %.

| Vé | Bộ số | ĐB | Mua từ vé 1 đến vé này: có ≥1 vé trúng | …có ≥1 vé trùng ≥3 số | Cùng số vé ngẫu nhiên khác nhau |
|---|---|---|---:|---:|---:|
| cover 1 | `13 18 22 27 35` | `01` | **9,60 %** | 1,39 % | 9,60 % |
| cover 2 | `08 09 15 16 23` | `06` | **18,98 %** | 2,77 % | 18,29 % |
| cover 3 | `03 07 19 20 26` | `08` | **28,12 %** | 4,16 % | 26,13 % |
| cover 4 | `04 14 17 29 32` | `02` | **37,03 %** | 5,55 % | 33,23 % |
| cover 5 | `06 12 21 28 30` | `12` | **45,71 %** | 6,93 % | 39,64 % |
| cover 6 | `01 02 05 31 33` | `10` | **54,16 %** | 8,32 % | 45,44 % |
| cover 7 | `10 11 24 25 34` | `03` | **62,38 %** | 9,71 % | 50,68 % |
| cover 8 | `04 08 10 13 21` | `11` | **70,35 %** | 11,04 % | 55,42 % |

- Với Lotto 5/35: giải Khuyến khích (0–2 số + ĐB) chiếm 86 % số lần trúng và chỉ cần trùng số ĐB (01–12), nên số ĐB khác nhau là lợi thế chính: 5 vé phủ 5/12 khả năng của số ĐB; 12 vé có đủ 12 số ĐB thì kỳ nào cũng có vé trúng, nhưng vẫn có thể lỗ tiền.
- Độc đắc (5 số + ĐB): 8 vé khác nhau có khả năng đúng bằng 8 lần một vé — khoảng 1 trên 486.948, chọn số kiểu gì cũng vậy.
- Phủ số thay đổi xác suất cả nhóm có giải; không làm tăng xác suất của từng vé hoặc bảo đảm lợi nhuận.

**Thử lại trên lịch sử:** nếu 985 kỳ đã quay (#00021 → #01005) mỗi kỳ bạn mua 8 vé, số kỳ có ít nhất 1 vé trúng là:

- **cover → 70,19 % (khoảng 7 trong 10 kỳ)**
- random → 55,04 % (khoảng 6 trong 10 kỳ)
- hot → 34,26 % (khoảng 1 trong 3 kỳ)
- cold → 36,65 % (khoảng 1 trong 3 kỳ)
- balanced → 55,31 % (khoảng 6 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 14.353đ với phủ số và 14.162đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

### Bộ theo thống kê (tham khảo)

Chọn theo tần suất (hot), số gan (cold), cân bằng chẵn/lẻ–nhỏ/lớn (balanced). Các quy tắc này chưa chứng minh dự đoán kỳ sau tốt hơn ngẫu nhiên. Mỗi nhóm được loại vé trùng hoàn toàn.

| Chiến lược | Bộ số | ĐB | Chẵn/Lẻ | Nhỏ/Lớn (≤17) | Tổng |
|---|---|---|---|---|---:|
| hot | `02 10 26 27 33` | `06` | 3/2 | 2/3 | 98 |
| hot | `01 07 08 25 27` | `06` | 1/4 | 3/2 | 68 |
| cold | `16 26 29 30 34` | `04` | 4/1 | 1/4 | 135 |
| cold | `16 20 29 32 34` | `09` | 4/1 | 1/4 | 131 |
| balanced | `11 12 16 29 30` | `06` | 3/2 | 3/2 | 98 |
| balanced | `01 10 14 23 34` | `08` | 3/2 | 3/2 | 82 |
| random (đối chứng) | `01 05 13 20 23` | `04` | 1/4 | 3/2 | 62 |
| random (đối chứng) | `02 07 10 15 23` | `02` | 2/3 | 4/1 | 57 |

**Khả năng của mỗi bộ ở kỳ tới** (giống nhau cho mọi bộ, kể cả random): trúng giải bất kỳ **9,60 %** · trúng ≥3 số **1,39 %** · Độc đắc **0,0000257 %** (1 / 3.895.584).

| Trùng số chính | 0 | 1 | 2 | 3 | 4 | 5 |
|---|---:|---:|---:|---:|---:|---:|
| Xác suất | 43,90 % | 42,21 % | 12,51 % | 1,34 % | 0,0462 % | 0,000308 % |

Số ĐB trùng: 8,33 % (độc lập với số chính).

<details><summary>Backtest walk-forward</summary>

**Từng vé: 985 kỳ thật, 300 lần lấy mẫu/kỳ**

| Chiến lược | Số chính trùng TB | Trúng giải | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p trúng giải |
|---|---|---|---|---|---|
| random | 0.715 | 9,57 % | - | - | - |
| hot | 0.710 | 9,08 % | -0.485 | -1.229 đến +0.258 | 0.2005 |
| cold | 0.722 | 9,63 % | +0.065 | -0.693 đến +0.822 | 0.867 |
| balanced | 0.714 | 9,61 % | +0.045 | -0.111 đến +0.201 | 0.5713 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 70,19 % | 65,86 % đến 74,52 % | +15.149 | +14.466 đến +15.832 | 1.229E-231 |
| random | 55,04 % | 50,71 % đến 59,37 % | - | - | - |
| hot | 34,26 % | 29,93 % đến 38,59 % | -20.782 | -23.314 đến -18.250 | 5.623E-52 |
| cold | 36,65 % | 32,32 % đến 40,97 % | -18.395 | -20.986 đến -15.803 | 2.196E-40 |
| balanced | 55,31 % | 50,98 % đến 59,64 % | +0.269 | -0.468 đến +1.006 | 0.474 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 14.353đ | -65.647đ | -82,06% | 1,36 % | 1,36 % |
| random | 14.162đ | -65.838đ | -82,30% | 1,39 % | 1,38 % |
| hot | 12.461đ | -67.539đ | -84,42% | 1,16 % | 1,09 % |
| cold | 13.788đ | -66.212đ | -82,77% | 1,55 % | 1,44 % |
| balanced | 13.862đ | -66.138đ | -82,67% | 1,38 % | 1,36 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Có chênh lệch tăng tỷ lệ trúng giải ở ngưỡng trên trong mẫu này; chưa chứng minh dự đoán tốt hơn hoặc có lợi nhuận ở kỳ mới.

Các lần lặp chỉ giảm nhiễu mô phỏng, không làm tăng số kỳ độc lập. Chốt thuật toán, mục tiêu và mốc --from-draw trước khi thu thập dữ liệu mới; chọn lại mốc sau khi xem kết quả không tạo kiểm chứng ngoài mẫu.

Giải cơ bản trước thuế; loại trừ Jackpot và phần chia thêm Lotto 5/35. Các cột tiền, ROI, thu ≥ chi và thu > chi chỉ thuộc mô hình này; không phải tiền thực nhận hay xác suất có lời đầy đủ. Không lấy toàn bộ quỹ Jackpot làm tiền thưởng cho một vé.

Tổng chi của một chuỗi mua 8 vé qua 985 kỳ: 78.800.000đ. Mỗi cách chọn là phương án thay thế với cùng ngân sách; tiền thưởng và lãi/lỗ trong bảng là trung bình mỗi kỳ, không cộng tiền giữa các lần mô phỏng.

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
| #01005 | 13/11/2026 | `04 23 27 28 35 | 03` | - |
| #01004 | 12/11/2026 | `02 05 07 09 22 | 10` | - |
| #01003 | 12/11/2026 | `04 08 10 17 33 | 11` | - |
| #01002 | 11/11/2026 | `07 19 24 25 27 | 11` | - |
| #01001 | 11/11/2026 | `09 10 12 15 31 | 05` | - |

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

- **cover → 18,75 % (khoảng 1 trong 5 kỳ)**
- random → 17,60 % (khoảng 1 trong 6 kỳ)
- hot → 14,36 % (khoảng 1 trong 7 kỳ)
- cold → 13,80 % (khoảng 1 trong 7 kỳ)
- balanced → 17,34 % (khoảng 1 trong 6 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 10.431đ với phủ số và 10.814đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

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
| hot | 0.800 | 2,36 % | -0.019 | -0.221 đến +0.183 | 0.8527 |
| cold | 0.790 | 2,26 % | -0.114 | -0.315 đến +0.087 | 0.2656 |
| balanced | 0.799 | 2,38 % | +0.001 | -0.064 đến +0.066 | 0.9743 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 18,75 % | 15,32 % đến 22,18 % | +1.154 | +0.696 đến +1.612 | 8.667E-07 |
| random | 17,60 % | 14,17 % đến 21,03 % | - | - | - |
| hot | 14,36 % | 10,93 % đến 17,79 % | -3.232 | -4.296 đến -2.169 | 3.101E-09 |
| cold | 13,80 % | 10,37 % đến 17,23 % | -3.798 | -4.877 đến -2.718 | 7.467E-12 |
| balanced | 17,34 % | 13,91 % đến 20,78 % | -0.252 | -0.723 đến +0.219 | 0.2945 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 10.431đ | -69.569đ | -86,96% | 1,13 % | 1,13 % |
| random | 10.814đ | -69.186đ | -86,48% | 1,15 % | 1,15 % |
| hot | 10.164đ | -69.836đ | -87,30% | 1,61 % | 1,61 % |
| cold | 9.840đ | -70.160đ | -87,70% | 1,46 % | 1,46 % |
| balanced | 11.205đ | -68.795đ | -85,99% | 1,18 % | 1,18 % |

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

- **cover → 10,59 % (khoảng 1 trong 9 kỳ)**
- random → 10,11 % (khoảng 1 trong 10 kỳ)
- hot → 8,64 % (khoảng 1 trong 12 kỳ)
- cold → 9,22 % (khoảng 1 trong 11 kỳ)
- balanced → 10,23 % (khoảng 1 trong 10 kỳ)

Tiền: mỗi kỳ chi 80.000đ, thu về trung bình 10.683đ với phủ số và 12.013đ với vé ngẫu nhiên (chỉ tính giải cố định, chưa trừ thuế, không tính Jackpot). Về lâu dài mọi cách chọn thu về như nhau — chênh lệch tiền ở đây là may rủi. Phủ số chỉ thay đổi **số kỳ có vé trúng**, không làm tăng tiền thưởng.

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
| hot | 0.656 | 1,37 % | +0.025 | -0.134 đến +0.184 | 0.7539 |
| cold | 0.660 | 1,46 % | +0.117 | -0.051 đến +0.285 | 0.1714 |
| balanced | 0.654 | 1,32 % | -0.022 | -0.071 đến +0.028 | 0.392 |

**Cùng 8 vé khác nhau/kỳ, 37 nhóm mô phỏng/kỳ**

| Cách chọn | Có ≥1 giải | Khoảng 95% theo kỳ | Δ với random (điểm %) | CI 95% của Δ (điểm %) | p có ≥1 giải |
|---|---|---|---|---|---|
| cover | 10,59 % | 6,96 % đến 14,22 % | +0.478 | +0.123 đến +0.833 | 0.00836 |
| random | 10,11 % | 6,49 % đến 13,74 % | - | - | - |
| hot | 8,64 % | 5,01 % đến 12,27 % | -1.472 | -2.367 đến -0.576 | 0.001291 |
| cold | 9,22 % | 5,60 % đến 12,85 % | -0.888 | -1.820 đến +0.043 | 0.06167 |
| balanced | 10,23 % | 6,60 % đến 13,85 % | +0.118 | -0.251 đến +0.486 | 0.532 |

**Mô hình tiền giải cơ bản: chi 80.000đ/kỳ**

| Cách chọn | Thưởng TB/kỳ | Lãi/lỗ TB/kỳ | ROI cơ bản | Thu ≥ chi | Thu > chi |
|---|---|---|---|---|---|
| cover | 10.683đ | -69.317đ | -86,65% | 0,543 % | 0,543 % |
| random | 12.013đ | -67.987đ | -84,98% | 0,925 % | 0,925 % |
| hot | 13.726đ | -66.274đ | -82,84% | 2,04 % | 2,04 % |
| cold | 10.911đ | -69.089đ | -86,36% | 2,31 % | 2,31 % |
| balanced | 10.682đ | -69.318đ | -86,65% | 0,944 % | 0,944 % |

Đánh giá trúng giải theo từng kỳ thật; ngưỡng Bonferroni p < 0.00238 cho tối đa 21 so sánh trên các game được chọn. CI ghép cặp là xấp xỉ t 95% chưa hiệu chỉnh; khoảng của tỷ lệ nhóm dùng Hoeffding theo số kỳ. Dấu '-' là chưa đủ thông tin để ước lượng.

Chưa phát hiện cách chọn tăng tỷ lệ trúng giải so với random ở ngưỡng đã hiệu chỉnh.

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

