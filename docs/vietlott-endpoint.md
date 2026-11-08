# S-0 – Endpoint AJAX Vietlott (Power 6/55, Mega 6/45, Lotto 5/35)

## Cập nhật 2026-11-08 — đã chạy thử cả 3 game

| Game | Trang | Webpart AjaxPro | `ArrayNumbers` |
|---|---|---|---|
| Power 6/55 | `winning-number-655` | `Game655CompareWebPart` | 5 dải × 18 |
| Mega 6/45 | `winning-number-645` | `Game645CompareWebPart` | 6 dải × 18 |
| Lotto 5/35 | `winning-number-535` | `Game535CompareWebPart` | 5 dải × 35 |

- **PageIndex 0-based: đã xác minh** (PageIndex 0 = 8 kỳ mới nhất, 1 = 8 kỳ tiếp theo), cả 3 game.
- Sai kích thước `ArrayNumbers` → `{"Error":true,"InfoMessage":"Index was outside the bounds of the array."}`.
- `Key` **đổi theo mỗi lần tải trang** (không còn là hằng); code đọc lại từ HTML trang của từng game, không có key dự phòng.
- HTML bảng kết quả giống nhau: 6/55 có 7 `span.bong_tron` (số thứ 7 = ĐB), 6/45 có 6, 5/35 có 5 + `span.bong_tron-sperator` + 1 ĐB (1–12).
- minhngoc: 6/45 dùng khối `div.bangkq6x36` (id `DT6X45_*`); trang `lotto-5x35.html` không có kết quả trong HTML (nạp bằng JS) → 5/35 chỉ dùng vietlott.

Phần dưới là ghi chép gốc ngày 2026-11-07 cho 6/55.

Ghi nhận ngày **2026-11-07** bằng cách đọc HTML/JS của trang (không dùng DevTools).

| Mục | Trạng thái |
|---|---|
| URL, method, header, tên tham số | ✅ Xác nhận từ JS proxy AjaxPro do server phát |
| Cấu trúc HTML bảng kết quả | ✅ Xác nhận (trang tĩnh), có test fixture |
| Gọi POST thật, PageIndex bắt đầu từ 0 hay 1 | ⚠️ **Chưa chạy thử** — cần kiểm tra bằng DevTools hoặc `power655 fetch -v` |

## 1. Trang kết quả

`GET https://vietlott.vn/vi/trung-thuong/ket-qua-trung-thuong/winning-number-655`

- HTML tĩnh chứa 8 kỳ trong `#divResultContent`, **có thể cache cũ**: ngày 07/11/2026 bảng chỉ tới kỳ 01420,
  trong khi dropdown `#drpSelectGameDraw` đã có `07/11/2026 (01421)`.
- Trang nhúng các script proxy AjaxPro, ví dụ
  `/ajaxpro/Vietlott.PlugIn.WebParts.Game655CompareWebPart,Vietlott.PlugIn.WebParts.ashx`.

## 2. Bước 1 – lấy RenderInfo

```
POST https://vietlott.vn/ajaxpro/Vietlott.Utility.WebEnvironments,Vietlott.Utility.ashx
X-AjaxPro-Method: ServerSideFrontEndCreateRenderInfo
Content-Type: text/plain; charset=utf-8

{"SiteId":"main.frontend.vi"}
```

Phản hồi (đã chạy thử, OK):

```json
{"value":{"SiteId":"main.frontend.vi","SiteAlias":"main.vi","UserSessionId":"","SiteLang":"vi",
 "IsPageDesign":false,"ExtraParam1":"","ExtraParam2":"","ExtraParam3":"","SiteURL":"","WebPage":null,
 "SiteName":"Vietlott","OrgPageAlias":null,"PageAlias":null,"FullPageAlias":null,"RefKey":null,"System":1}}
```

JS trang gán thêm `ORenderInfo.SiteLang = 'vi'` (đã có sẵn trong phản hồi).

## 3. Bước 2 – lấy trang kết quả

Proxy JS:

```js
ServerSideDrawResult: function(ORenderInfo, Key, GameDrawId, ArrayNumbers, CheckMulti, PageIndex) {
  return this.invoke("ServerSideDrawResult", {"ORenderInfo":ORenderInfo, "Key":Key, "GameDrawId":GameDrawId,
         "ArrayNumbers":ArrayNumbers, "CheckMulti":CheckMulti, "PageIndex":PageIndex}, ...);
}
```

Cách trang gọi:

```js
var CurrentPageIndex = 0;
function NextPage(PageIndex) { CurrentPageIndex = PageIndex; setTimeout('RealDoSearch()', 10); }
AjaxOut = ...ServerSideDrawResult(RenderInfo, 'cae0f881', GameDrawId, ArrNumbers, CheckMulti, CurrentPageIndex).value;
document.getElementById('divResultContent').innerHTML = AjaxOut.HtmlContent;
```

Request tương ứng:

```
POST https://vietlott.vn/ajaxpro/Vietlott.PlugIn.WebParts.Game655CompareWebPart,Vietlott.PlugIn.WebParts.ashx
X-AjaxPro-Method: ServerSideDrawResult
Content-Type: text/plain; charset=utf-8
Referer: https://vietlott.vn/vi/trung-thuong/ket-qua-trung-thuong/winning-number-655

{"ORenderInfo":{...từ bước 1...},
 "Key":"cae0f881",
 "GameDrawId":"",
 "ArrayNumbers":[["",… 18 chuỗi rỗng],[…],[…],[…],[…]],
 "CheckMulti":false,
 "PageIndex":0}
```

- `Key` là hằng trong JS trang (`'cae0f881'` ngày 07/11/2026). Code đọc lại bằng regex mỗi phiên, fallback về giá trị này.
- `GameDrawId = ""` ⇒ "Toàn bộ" (không lọc kỳ).
- `ArrayNumbers` = 5 dải × 18 chuỗi rỗng (không dò số).
- Phản hồi dự kiến: `{"value":{"Error":false,"InfoMessage":"...","HtmlContent":"<div class=\"doso_output_nd ..."}}`,
  có thể kèm hậu tố `;/*` của AjaxPro — code cắt tới dấu `}` cuối.
- Cookie: không thấy token CSRF nào; HttpClient giữ cookie mặc định.

## 4. Giả định cần xác minh

1. **PageIndex 0-based**: `CurrentPageIndex` khởi tạo 0 nên code gửi `PageIndex = page - 1`.
   Nếu sai (trang 2 trả trùng trang 1), `DrawFetchService` sẽ cảnh báo "trùng dữ liệu trang trước" và dừng —
   khi đó đổi công thức trong `VietlottSource.FetchPageAsync`.
2. Số kỳ mỗi trang AJAX (giả định 8 như trang tĩnh).

Cách kiểm tra nhanh: `power655 fetch --pages 2 --source vietlott -v` và xem log từng trang.

## 5. HTML bảng kết quả

```html
<tr>
  <td>05/11/2026</td>
  <td><a href="/vi/.../655?id=01420&nocatche=1">01420</a></td>
  <td><div class="day_so_ket_qua_v2">
    <span class="bong_tron ">06</span> … <span class="bong_tron no-margin-right ">52</span>
    <span class="bong_tron-sperator">|</span><span class="bong_tron no-margin-right">36</span>
  </div></td>
</tr>
```

7 `span.bong_tron`: 6 số chính + số đặc biệt (số thứ 7). Parser: `VietlottHtmlParser`.

## 6. Nguồn dự phòng minhngoc.net.vn

`GET https://www.minhngoc.net.vn/ket-qua-xo-so/dien-toan-vietlott/power-6x55.html` — 10 kỳ gần nhất, **không phân trang**.
Mỗi kỳ là một `div.bangkq6x55`: `span#DT6X45_KY_VE` ("#01420"), ngày trong cùng ô, `ul.result-number div.bool` × 7,
jackpot ở `#DT6X55_G_JACKPOT` / `#DT6X55_G_JACKPOT2`. Parser: `MinhNgocHtmlParser`.
