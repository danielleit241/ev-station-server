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

**Quy tắc về ngày cấp và ngày hết hạn CCCD**:

8.1. Lấy ngày sinh từ dữ liệu đầu vào.
8.2. Ngày bắt đầu hiệu lực CCCD = ngày sinh + 14 năm.
8.3. Tính các mốc 25 tuổi, 40 tuổi, 60 tuổi từ ngày sinh.
8.4. Ngày hết hạn = mốc gần nhất trong tương lai (sau ngày hiện tại).
8.5. Nếu công dân đã ≥ 60 tuổi thì ngày hết hạn = null (hoặc ""vô thời hạn"").


**Lưu ý:** Nếu không trích xuất được thông tin nào hoặc có trên 3 trường bị thiếu, tuyệt đối không tạo JSON mà chỉ trả về chuỗi rỗng.
";
    }
}