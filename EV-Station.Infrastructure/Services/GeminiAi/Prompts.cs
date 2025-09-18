namespace EV_Station.Infrastructure.Services.GeminiAi
{
    public class Prompts
    {
        public static string IdentityCardPrompt(string rawOcrText) => $@"Hãy chuẩn hóa thông tin căn cước công dân Việt Nam dựa trên dữ liệu OCR.

Dữ liệu OCR:
{rawOcrText}

Yêu cầu:
1. Chỉ trích xuất và trả về các thông tin sau dưới dạng JSON tiếng Việt hợp lệ:
    - CardNumber (số căn cước)
    - FullName (họ tên)
    - Sex (giới tính)
    - Nationality (quốc tịch)
    - DateOfBirth (ngày sinh, định dạng yyyy-MM-dd)
    - PlaceOfOrigin (quê quán)
    - PlaceOfResidence (nơi thường trú)
    - CreateDate (ngày cấp, định dạng yyyy-MM-dd)
    - DayOfExpiry (ngày hết hạn, định dạng yyyy-MM-dd)
2. Nếu giá trị là ngày, định dạng ban đầu có thể là dd-MM-yyyy, hãy chuyển thành yyyy-MM-dd.
3. Nếu dữ liệu không đủ thông tin cho một trường, trả về giá trị rỗng hoặc null cho trường đó.
4. Giữ nguyên cấu trúc trường như trên trong JSON.
5. Sửa lỗi chính tả OCR nếu có.
6. Đảm bảo JSON trả về không có ký tự thừa, chỉ chứa các trường trên.
7. **Cảnh báo:** Nếu có trên 3 trường bị null hoặc rỗng, hãy trả về chuỗi rỗng ("") thay vì JSON. Tuyệt đối không tự bịa, tự dự đoán, hoặc tạo thông tin không có trong dữ liệu OCR. Nếu không thể trích xuất đủ dữ liệu, chỉ trả về chuỗi rỗng.
8. Đảm bảo tính nhất quán giữa ngày cấp và ngày hết hạn căn cước theo quy tắc bên dưới.

**Quy tắc về ngày cấp và ngày hết hạn căn cước:**
- Mốc bắt đầu làm căn cước là ngày 01-01-2021.
- Nếu ngày hết hạn nằm trong năm 2021 (từ 01-01-2021 đến 31-12-2021) thì ngày bắt đầu phải là 01-01-2021, và ngày hết hạn là 2026-01-01 hoặc cùng ngày với mốc 5 năm tiếp theo.
- Nếu ngày hết hạn nằm trong mốc 5 năm tiếp theo (từ 01-01-2026 đến 31-12-2026), thì ngày bắt đầu phải là 01-01-2026.
- Tương tự, sau mỗi mốc 5 năm (01-01-2031, 01-01-2036, ...), nếu ngày hết hạn nằm trong mốc đó thì ngày bắt đầu phải là mốc đó.
- Ngày bắt đầu không bao giờ được nhỏ hơn mốc bắt đầu của lần cấp căn cước đó.

Ví dụ:
- Nếu ngày hết hạn là 2021-06-21 thì ngày bắt đầu là 2021-06-21, ngày hết hạn là 2026-06-21.
- Nếu ngày hết hạn là 2026-03-15 thì ngày bắt đầu là 2026-03-15, ngày hết hạn là 2031-03-15.

**Lưu ý:** Nếu không trích xuất được thông tin nào hoặc có trên 3 trường bị thiếu, tuyệt đối không tạo JSON mà chỉ trả về chuỗi rỗng.
";
    }
}