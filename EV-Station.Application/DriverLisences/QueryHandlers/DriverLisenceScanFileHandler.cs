using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.DriverLisences.Queries;
using MediatR;

namespace EV_Station.Application.DriverLisences.QueryHandlers
{
    public class DriverLisenceScanFileHandler : IRequestHandler<DriverLisenceScanFile, GenericApiResponse<DriverLisenceScanResponse>>
    {
        private readonly ITesseractOcrService _tesseractOcrService;
        private readonly IGeminiAiService _geminiAi;
        public DriverLisenceScanFileHandler(ITesseractOcrService tesseractOcrService, IGeminiAiService geminiAiService)
        {
            _tesseractOcrService = tesseractOcrService;
            _geminiAi = geminiAiService;
        }
        public async Task<GenericApiResponse<DriverLisenceScanResponse>> Handle(DriverLisenceScanFile request, CancellationToken cancellationToken)
        {
            var rawOcrFrontText = await _tesseractOcrService.ExtractTextFromImageFileAsync(request.dto.FrontImage);
            if (_geminiAi.DetermineFrontOrBackOfCardAsync(rawOcrFrontText).Result != "FRONT")
            {
                return GenericApiResponse<DriverLisenceScanResponse>.FailResponse("Ảnh mặt trước không đúng định dạng. Vui lòng gửi ảnh mặt trước của Giấy phép lái xe.");
            }

            var rawOcrBackText = await _tesseractOcrService.ExtractTextFromImageFileAsync(request.dto.BackImage);
            if (_geminiAi.DetermineFrontOrBackOfCardAsync(rawOcrBackText).Result != "BACK")
            {
                return GenericApiResponse<DriverLisenceScanResponse>.FailResponse("Ảnh mặt sau không đúng định dạng. Vui lòng gửi ảnh mặt sau của Giấy phép lái xe.");
            }

            var rawOcrText = rawOcrFrontText + "\n" + rawOcrBackText;
            if (string.IsNullOrWhiteSpace(rawOcrText))
            {
                return GenericApiResponse<DriverLisenceScanResponse>.FailResponse("Không thể quét ảnh, vui lòng gửi ảnh có độ sắc nét cao.");
            }

            var result = await _geminiAi.ExtractDriverLisenceInfoAsync(rawOcrText);
            if (result == null)
            {
                return GenericApiResponse<DriverLisenceScanResponse>.FailResponse("Không thể quét ảnh, vui lòng gửi ảnh có độ sắc nét cao.");
            }

            return GenericApiResponse<DriverLisenceScanResponse>.SuccessResponse(result);
        }
    }
}
