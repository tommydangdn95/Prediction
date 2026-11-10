# Cho workflow gọi vietlott qua router nhà (IP Việt Nam)

vietlott.vn trả **403 Forbidden** cho máy chạy của GitHub (đặt ở nước ngoài), nhưng trả bình thường cho IP
Việt Nam. Không có VPN thì Lotto 5/35 không có kỳ mới (minhngoc không có Lotto 5/35); Mega/Power vẫn lấy
được từ minhngoc.

Cách làm: bật **VPN server** trên router ASUS, cất file cấu hình client vào **GitHub Secrets**. Bước
*Kết nối VPN về router* trong [`daily.yml`](../.github/workflows/daily.yml) chạy
[`vpn-up.sh`](../.github/scripts/vpn-up.sh):

- chỉ các IP của `vietlott.vn` đi qua VPN; GitHub, minhngoc, DNS vẫn đi thẳng;
- kiểm tra vietlott trả HTTP 200 rồi mới fetch; ngắt VPN ngay sau bước fetch;
- VPN lỗi thì chỉ cảnh báo, job vẫn chạy như khi không có VPN;
- không in IP/hostname router ra log (repo công khai, log ai cũng xem được).

Máy tính ở nhà **không cần bật**, chỉ cần router có điện và mạng.

## Điều kiện

- WAN IP trong trang quản trị router **trùng** với IP ở https://ifconfig.me (không bị CGNAT). ✅ đã kiểm tra.
- Bật **DDNS** để router có tên miền cố định dù IP nhà thay đổi:
  *WAN → DDNS* → Enable → Server `WWW.ASUS.COM` → đặt tên `xxx.asuscomm.com` → Apply.
  Bật DDNS **trước** khi xuất file cấu hình VPN, để file dùng tên miền thay vì IP hiện tại.

## Cách 1 — WireGuard (khuyên dùng nếu router có)

Firmware ASUSWRT 3.0.0.4.388 trở lên mới có tab WireGuard. Không thấy thì cập nhật firmware
(*Administration → Firmware Upgrade*) hoặc dùng OpenVPN ở cách 2.

1. Trang quản trị router → *VPN* → *VPN Server* → tab **WireGuard VPN** → bật **Enable WireGuard VPN**.
2. Ở danh sách client bấm **+**, đặt tên `github-actions` (một client riêng, để thu hồi riêng khi cần) → Apply.
3. Bấm biểu tượng **tải xuống / Export** của client đó → được file `.conf`.
   Mở bằng Notepad, file có dạng:
   ```
   [Interface]
   PrivateKey = ...
   Address = 10.6.0.2/32
   DNS = 10.6.0.1
   [Peer]
   PublicKey = ...
   AllowedIPs = 0.0.0.0/0
   Endpoint = xxx.asuscomm.com:51820
   ```
   Không cần sửa gì: script tự bỏ dòng `DNS` và thay `AllowedIPs` bằng IP của vietlott.
4. GitHub → repo → *Settings* → *Secrets and variables* → *Actions* → **New repository secret**:
   - Name: `VPN_WIREGUARD_CONF`
   - Secret: dán **toàn bộ** nội dung file `.conf`.
5. Xóa file `.conf` trên máy (nó chứa khóa riêng).

## Cách 2 — OpenVPN

1. *VPN* → *VPN Server* → tab **OpenVPN** → bật.
2. *VPN Details* → **Advanced Settings** → **Client will use VPN to access: Internet and local network**
   (bắt buộc — để mặc định "Local network only" thì vietlott vẫn trả 403).
3. Thêm một tài khoản riêng (vd `github-actions` + mật khẩu dài) → Apply.
4. Bấm **Export OpenVPN configuration file** → được `client.ovpn`.
5. Tạo 2 secret:
   - `VPN_OPENVPN_CONF`: toàn bộ nội dung `client.ovpn`.
   - `VPN_OPENVPN_AUTH`: 2 dòng — dòng 1 username, dòng 2 password.

Có cả hai secret WireGuard và OpenVPN thì script dùng WireGuard.

## Kiểm tra

Tab **Actions** → *Daily predictions* → **Run workflow**, mở bước *Kết nối VPN về router*:

| Log | Ý nghĩa |
|---|---|
| `VPN OK: vietlott trả HTTP 200 qua router.` | Thành công; bước sau phải có `vietlott Lotto 5/35 trang 1: 8 kỳ`. |
| `Chưa cấu hình VPN ...` | Chưa tạo secret hoặc sai tên secret. |
| `WireGuard/OpenVPN không kết nối được ...` | Router không nhận kết nối: kiểm tra VPN server đã bật, DDNS đúng, nhà mạng không chặn cổng (WireGuard mặc định UDP 51820). |
| `Đã bật VPN nhưng vietlott vẫn trả HTTP 403` | Kết nối được nhưng router không cho client ra Internet: OpenVPN xem lại bước 2; WireGuard kiểm tra client được phép truy cập Internet. |

## Thu hồi

Xóa client `github-actions` trên router (hoặc tắt VPN server) và xóa secret trên GitHub. Workflow tự quay về
chạy không có VPN và chỉ cảnh báo.
