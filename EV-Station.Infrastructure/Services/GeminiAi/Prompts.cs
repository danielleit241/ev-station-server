
namespace EV_Station.Infrastructure.Services.GeminiAi
{
    public class Prompts
    {
        public static string GetFrontOrBackOfCardPrompt(string rawOcrText) => $@"Bạn là một hệ thống kiểm tra giấy tờ.  
Phân tích các thông tin Căn cước công dân (CCCD) hoặc Giấy phép lái xe (GPLX).  
Nhiệm vụ: Xác định thông tin đoạn text này là mặt trước (FRONT) hay mặt sau (BACK).  

Dữ liệu OCR:
{rawOcrText}

Quy tắc nhận diện:
- Dữ liệu OCR có thể chứa lỗi chính tả. Hãy sửa lỗi chính tả và sau đó tập trung vào việc nhận diện các từ khóa quan trọng.
- Từ khóa nhận diện mặt trước CCCD/GPLX: ""CĂN CƯỚC CÔNG DÂN"", ""GIẤY PHÉP LÁI XE"", ""HỌ VÀ TÊN"", ""SỐ"", ""NGÀY SINH"", ""QUỐC TỊCH"", ""ĐỊA CHỈ"".
- Từ khóa nhận diện mặt sau CCCD/GPLX: ""NGÀY CẤP"", ""NƠI CẤP"", ""HẠN ĐẾN"", ""LOẠI XE"", ""HẠNG"", ""SỐ KHUNG"", ""SỐ MÁY"".

Quy tắc trả về:
- Nếu là CCCD hoặc GPLX mặt trước → trả về đúng chữ ""FRONT"".
- Nếu là CCCD hoặc GPLX mặt sau → trả về đúng chữ ""BACK"".
- Không giải thích thêm, không trả về gì khác ngoài ""FRONT"" hoặc ""BACK"".
- Nếu không chắc chắn, trả về chuỗi rỗng ("").
";

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
        public static string DriverLisencePrompt(string rawOcrText) => $@"Hãy chuẩn hóa thông tin giấy phép lái xe Việt Nam dựa trên dữ liệu OCR.

Dữ liệu OCR:
{rawOcrText}

Yêu cầu:
1. Chỉ trích xuất và trả về các thông tin sau dưới dạng JSON tiếng Việt hợp lệ:
    - LicenseNumber (số giấy phép)
    - FullName (họ tên)
    - DateOfBirth (ngày sinh, định dạng yyyy-MM-dd)
    - Nationality (quốc tịch)
    - Address (nơi cư trú)
    - LicenseClass (hạng giấy phép, chỉ nhận các giá trị: A1, A2, B1, B2, C, D)
    - ClassificationOfMotorVehicles (loại phương tiện được phép điều khiển, ví dụ: xe mô tô 2 bánh, ô tô con, xe tải,…)
    - BeginingDate (ngày bắt đầu hiệu lực, định dạng yyyy-MM-dd)
    - ExpiresDate (ngày hết hạn, định dạng yyyy-MM-dd, có thể null nếu vô thời hạn)

2. Nếu giá trị là ngày, định dạng ban đầu có thể là dd-MM-yyyy, hãy chuyển thành yyyy-MM-dd.
3. Nếu dữ liệu không đủ thông tin cho một trường, trả về giá trị rỗng hoặc null cho trường đó.
4. Giữ nguyên cấu trúc trường như trên trong JSON.
5. Sửa lỗi chính tả OCR nếu có.
6. Đảm bảo JSON trả về không có ký tự thừa, chỉ chứa các trường trên.
7. **Cảnh báo:** Nếu có trên 3 trường bị null hoặc rỗng, hãy trả về chuỗi rỗng ("") thay vì JSON. Tuyệt đối không tự bịa, dự đoán, hoặc tạo thông tin không có trong dữ liệu OCR.

8. Quy tắc về ngày hiệu lực (BeginingDate) và ngày hết hạn (ExpiresDate):
   - Ưu tiên lấy từ OCR nếu có.
   - Nếu thiếu, xác định dựa trên LicenseClass + BeginingDate theo luật hiện hành:
     * **Trước 01/01/2025:**
       - A1, A2: Vô thời hạn (ExpiresDate = null).
       - B1, B2, C, D: 10 năm kể từ BeginingDate.
     * **Từ 01/01/2025 trở đi (áp dụng quy định mới):**
       - A1, A2: Vô thời hạn.
       - B1: Có thời hạn đến 60 tuổi (nếu cấp trước 45 tuổi) hoặc 10 năm (nếu cấp sau 45 tuổi).
       - B2, C, D: Thời hạn 10 năm kể từ BeginingDate.

9. Nếu không nhận diện được ClassificationOfMotorVehicles từ OCR, hãy suy luận dựa vào LicenseClass và BeginingDate (ngày trúng tuyển/hiệu lực) theo quy định của Việt Nam. Ví dụ:
   - A1: Xe mô tô hai bánh dung tích ≤ 175cc, xe mô tô 3 bánh cho người khuyết tật.
   - A2: Xe mô tô hai bánh dung tích > 175cc, và tất cả các loại xe được quy định cho hạng A1.
   - B1: Ô tô ≤ 9 chỗ ngồi, xe tải ≤ 3.5 tấn (không kinh doanh vận tải).
   - B2: Ô tô ≤ 9 chỗ ngồi, xe tải ≤ 3.5 tấn (có thể kinh doanh vận tải).
   - C: Ô tô tải ≥ 3.5 tấn, xe đầu kéo kéo rơ-moóc ≤ 3.5 tấn.
   - D: Ô tô chở người từ 10 đến 30 chỗ ngồi (kể cả lái xe).

10. Đảm bảo tính nhất quán giữa BeginingDate và ExpiresDate theo từng giai đoạn luật (trước và sau 2025).
";
    }
}