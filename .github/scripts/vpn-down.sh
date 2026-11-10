#!/usr/bin/env bash
# Ngắt VPN do vpn-up.sh tạo (nếu có). Không bao giờ làm hỏng job.
set -uo pipefail
STATE="${RUNNER_TEMP:-/tmp}/vpn"
kind=$(cat "$STATE/kind" 2>/dev/null || true)

case "$kind" in
  wg) sudo wg-quick down wg0 >/dev/null 2>&1 || true ;;
  ovpn)
    pid=$(sudo cat "$STATE/ovpn.pid" 2>/dev/null || true)
    [ -n "$pid" ] && sudo kill "$pid" 2>/dev/null || true
    ;;
esac
sudo rm -f /etc/wireguard/wg0.conf "$STATE/client.ovpn" "$STATE/auth.txt" "$STATE/kind" 2>/dev/null || true
[ -n "$kind" ] && echo "Đã ngắt VPN ($kind)."
exit 0
