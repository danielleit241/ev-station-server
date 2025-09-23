using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.DriverLisences.Queries;
using MediatR;

namespace EV_Station.Application.DriverLisences.QueryHandlers
{
    public class DriverLicenseScanFileHandler : IRequestHandler<DriverLicenseScanFile, GenericApiResponse<DriverLicenseScanResponse>>
    {
        private readonly ITesseractOcrService _tesseractOcrService;
        private readonly IGeminiAiService _geminiAi;
        public DriverLicenseScanFileHandler(ITesseractOcrService tesseractOcrService, IGeminiAiService geminiAiService)
        {
            _tesseractOcrService = tesseractOcrService;
            _geminiAi = geminiAiService;
        }
        public async Task<GenericApiResponse<DriverLicenseScanResponse>> Handle(DriverLicenseScanFile request, CancellationToken cancellationToken)
        {
            var rawOcrFrontText = await _tesseractOcrService.ExtractTextFromImageFileAsync(request.dto.FrontImage);
            await Task.Delay(2000, cancellationToken);
            var isFront = await _geminiAi.DetermineFrontOrBackOfCardAsync(rawOcrFrontText);
            if (isFront != "FRONT")
            {
                return GenericApiResponse<DriverLicenseScanResponse>.FailResponse("Ảnh mặt trước không đúng định dạng. Vui lòng gửi ảnh mặt trước của Giấy phép lái xe.");
            }

            var rawOcrBackText = await _tesseractOcrService.ExtractTextFromImageFileAsync(request.dto.BackImage);
            await Task.Delay(2000, cancellationToken);
            var isBack = await _geminiAi.DetermineFrontOrBackOfCardAsync(rawOcrBackText);
            if (isBack != "BACK")
            {
                return GenericApiResponse<DriverLicenseScanResponse>.FailResponse("Ảnh mặt sau không đúng định dạng. Vui lòng gửi ảnh mặt sau của Giấy phép lái xe.");
            }

            var rawOcrText = rawOcrFrontText + "\n" + rawOcrBackText;
            if (string.IsNullOrWhiteSpace(rawOcrText))
            {
                return GenericApiResponse<DriverLicenseScanResponse>.FailResponse("Không thể quét ảnh, vui lòng gửi ảnh có độ sắc nét cao.");
            }

            var result = await _geminiAi.ExtractDriverLicenseInfoAsync(rawOcrText);
            if (result == null)
            {
                return GenericApiResponse<DriverLicenseScanResponse>.FailResponse("Không thể quét ảnh, vui lòng gửi ảnh có độ sắc nét cao.");
            }

            return GenericApiResponse<DriverLicenseScanResponse>.SuccessResponse(result);
        }
    }
}
