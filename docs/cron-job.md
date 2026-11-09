# Chạy workflow hằng ngày bằng cron-job.org

Workflow [`.github/workflows/daily.yml`](../.github/workflows/daily.yml) không tự chạy theo lịch: cron-job.org
gọi GitHub API để kích hoạt nó (chính xác giờ hơn `schedule:` của GitHub, vốn hay trễ 10–60 phút).

Mỗi lần chạy: nạp `data/history.csv` → `fetch` kết quả mới cả 3 game → ghi lại `data/history.csv` →
tạo `predictions/YYYY-MM-DD.md` + `predictions/LATEST.md` → commit vào `main`. Báo cáo cũng hiện ở
trang Summary của lần chạy.

`report` còn lưu `predictions/ledger/{game}-{targetDrawId}.json`: bản đầu tiên của mỗi kỳ không bị ghi đè,
gồm thời điểm tạo, kỳ mục tiêu, bộ vé và dấu vết đầu vào. Markdown cùng ngày có thể được tạo lại để hiển thị,
nhưng hai ca Lotto và dự đoán sáng vẫn còn trong ledger. Mega/Power không đếm nhiều ngày dự đoán cùng kỳ thành nhiều kỳ độc lập.
Nếu thiếu kết quả mới hoặc kỳ mục tiêu đã bắt đầu, báo cáo đánh dấu game đó và không tạo dự đoán mới;
workflow vẫn commit phần dữ liệu/dự đoán hợp lệ. Commit trước giờ quay là dấu vết công bố để đối chiếu với timestamp tại máy.

## 1. Tạo token GitHub (fine-grained)

GitHub → Settings → Developer settings → Personal access tokens → **Fine-grained tokens** → Generate new token:

- **Repository access:** Only select repositories → chọn repo này
- **Permissions → Repository permissions → Actions: Read and write** (chỉ cần quyền này)
- Đặt hạn dùng (vd 1 năm) và ghi lại ngày hết hạn để gia hạn.

Token chỉ dán vào cron-job.org, **không** commit vào repo.

## 2. Tạo cron job

cron-job.org → Create cronjob:

| Mục | Giá trị |
|---|---|
| URL | `https://api.github.com/repos/<OWNER>/<REPO>/actions/workflows/daily.yml/dispatches` |
| Schedule | Hằng ngày **07:00**, timezone `Asia/Ho_Chi_Minh` |
| Advanced → Request method | `POST` |
| Advanced → Headers | `Accept: application/vnd.github+json`<br>`Authorization: Bearer <TOKEN>`<br>`X-GitHub-Api-Version: 2022-11-28`<br>`Content-Type: application/json` |
| Advanced → Request body | `{"ref":"main"}` |

GitHub trả **204 No Content** khi thành công (cron-job.org hiển thị "Success"). 401/403 = token sai hoặc thiếu
quyền Actions; 404 = sai owner/repo/tên file hoặc token không được cấp repo này; 422 = sai `ref`.

Tùy chọn: body `{"ref":"main","inputs":{"pages":"5"}}` để lấy nhiều kỳ hơn.

### Vì sao 07:00

Lúc 07:00 kết quả tối hôm trước đã có cho cả 3 game (Mega/Power sau 18:45, Lotto 5/35 kỳ 21:00 sau 21:45).
Kỳ Lotto 5/35 lúc 13:00 sẽ được lấy ở lần chạy sáng hôm sau; muốn cập nhật trong ngày thì thêm một cron
khác lúc 14:00.

## 3. Kiểm tra

- Chạy tay: tab **Actions** → *Daily predictions* → **Run workflow**, hoặc nút "Test run" trên cron-job.org.
- Lỗi "Không lấy được dữ liệu game nào": vietlott/minhngoc có thể chặn IP của GitHub runner. Khi đó job
  báo đỏ (GitHub gửi email), không commit gì; lịch sử cũ trong `data/history.csv` vẫn còn nguyên.
- Fine-grained token hết hạn → cron-job.org báo 401; tạo token mới và thay header `Authorization`.
