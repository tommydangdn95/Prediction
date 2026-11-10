#!/usr/bin/env bash
# Kết nối runner GitHub về router nhà (IP Việt Nam) vì vietlott.vn trả 403 cho IP của GitHub.
# Chỉ các địa chỉ của vietlott.vn đi qua VPN; GitHub, minhngoc, DNS vẫn đi thẳng.
# Không bao giờ làm hỏng job: lỗi thì cảnh báo và chạy tiếp không có VPN (Mega/Power vẫn còn minhngoc).
# Repo công khai → không in IP/hostname của router ra log.
#
# Biến môi trường (từ GitHub Secrets, xem docs/vpn-router.md):
#   WG_CONF    nội dung file .conf WireGuard xuất từ router (ưu tiên)
#   OVPN_CONF  nội dung file .ovpn OpenVPN xuất từ router
#   OVPN_AUTH  2 dòng: username, password của tài khoản OpenVPN (nếu router yêu cầu)
set -uo pipefail

TARGET_HOST="vietlott.vn"
CHECK_URL="https://vietlott.vn/vi/trung-thuong/ket-qua-trung-thuong/winning-number-535"
STATE="${RUNNER_TEMP:-/tmp}/vpn"
mkdir -p "$STATE"

warn() { echo "::warning::$1"; }
mask() { sed -E 's/([0-9]{1,3}\.){3}[0-9]{1,3}/x.x.x.x/g; s/[A-Za-z0-9-]+\.asuscomm\.com/***.asuscomm.com/g'; }
check_vietlott() { curl -s -o /dev/null -w '%{http_code}' --max-time 20 -A 'Mozilla/5.0' "$CHECK_URL"; }

if [ -z "${WG_CONF:-}" ] && [ -z "${OVPN_CONF:-}" ]; then
  warn "Chưa cấu hình VPN (secret VPN_WIREGUARD_CONF hoặc VPN_OPENVPN_CONF): vietlott có thể trả 403 và Lotto 5/35 sẽ thiếu kỳ mới. Xem docs/vpn-router.md."
  exit 0
fi

before=$(check_vietlott)
echo "vietlott trước khi bật VPN: HTTP $before"

ips=$(getent ahostsv4 "$TARGET_HOST" | awk '{print $1}' | sort -u)
if [ -z "$ips" ]; then
  warn "Không phân giải được $TARGET_HOST — bỏ qua VPN."
  exit 0
fi

if [ -n "${WG_CONF:-}" ]; then
  echo "Kết nối WireGuard về router..."
  sudo apt-get install -y -qq wireguard-tools >/dev/null 2>&1 || { warn "Không cài được wireguard-tools — bỏ qua VPN."; exit 0; }
  allowed=$(printf '%s/32, ' $ips); allowed=${allowed%, }
  # Bỏ DNS (cần resolvconf, và DNS vẫn đi thẳng); chỉ định tuyến IP của vietlott qua đường hầm.
  printf '%s\n' "$WG_CONF" | tr -d '\r' \
    | sed -E '/^[[:space:]]*DNS[[:space:]]*=/d; s|^[[:space:]]*AllowedIPs[[:space:]]*=.*|AllowedIPs = '"$allowed"'|' \
    | sudo tee /etc/wireguard/wg0.conf >/dev/null
  sudo chmod 600 /etc/wireguard/wg0.conf
  if ! sudo wg-quick up wg0 >"$STATE/wg.log" 2>&1; then
    warn "WireGuard không kết nối được — chạy tiếp không có VPN."
    mask <"$STATE/wg.log" | tail -n 5
    exit 0
  fi
  echo wg >"$STATE/kind"
else
  echo "Kết nối OpenVPN về router..."
  sudo apt-get install -y -qq openvpn >/dev/null 2>&1 || { warn "Không cài được openvpn — bỏ qua VPN."; exit 0; }
  printf '%s\n' "$OVPN_CONF" | tr -d '\r' >"$STATE/client.ovpn"
  args=(--config "$STATE/client.ovpn" --route-nopull --daemon --log "$STATE/ovpn.log" --writepid "$STATE/ovpn.pid")
  for ip in $ips; do args+=(--route "$ip" 255.255.255.255); done
  if [ -n "${OVPN_AUTH:-}" ]; then
    printf '%s\n' "$OVPN_AUTH" | tr -d '\r' >"$STATE/auth.txt"
    chmod 600 "$STATE/auth.txt"
    args+=(--auth-user-pass "$STATE/auth.txt")
  fi
  sudo openvpn "${args[@]}"
  echo ovpn >"$STATE/kind"
  for _ in $(seq 1 40); do
    sudo grep -q "Initialization Sequence Completed" "$STATE/ovpn.log" 2>/dev/null && break
    sleep 1
  done
  if ! sudo grep -q "Initialization Sequence Completed" "$STATE/ovpn.log" 2>/dev/null; then
    warn "OpenVPN không kết nối được sau 40 giây — chạy tiếp không có VPN."
    sudo cat "$STATE/ovpn.log" | mask | tail -n 8
    bash "$(dirname "$0")/vpn-down.sh"
    exit 0
  fi
fi

after=$(check_vietlott)
if [ "$after" = "200" ]; then
  echo "VPN OK: vietlott trả HTTP 200 qua router."
else
  warn "Đã bật VPN nhưng vietlott vẫn trả HTTP $after (trước đó $before). Kiểm tra router cho client VPN ra Internet — xem docs/vpn-router.md."
fi
exit 0
