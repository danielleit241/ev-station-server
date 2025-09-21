using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.DriverLisences.Queries;
using MediatR;

namespace EV_Station.Application.DriverLisences.QueryHandlers
{
    public class DriverLisenceScanUrlHandler : IRequestHandler<DriverLisenceScanUrl, GenericApiResponse<DriverLisenceScanResponse>>
    {
        private readonly ITesseractOcrService _tesseractOcrService;
        private readonly IGeminiAiService _geminiAi;
        public DriverLisenceScanUrlHandler(ITesseractOcrService tesseractOcrService, IGeminiAiService geminiAi)
        {
            _tesseractOcrService = tesseractOcrService;
            _geminiAi = geminiAi;
        }

        public async Task<GenericApiResponse<DriverLisenceScanResponse>> Handle(DriverLisenceScanUrl request, CancellationToken cancellationToken)
        {
            var rawOcrFrontText = await _tesseractOcrService.ExtractTextFromImageUrlAsync(request.dto.FrontImageUrl);
            var rawOcrBackText = await _tesseractOcrService.ExtractTextFromImageUrlAsync(request.dto.BackImageUrl);

            var rawOcrText = rawOcrFrontText + "\n" + rawOcrBackText;
            var result = await _geminiAi.ExtractDriverLisenceInfoAsync(rawOcrText);
            if (result == null)
            {
                return GenericApiResponse<DriverLisenceScanResponse>.FailResponse("Không thể quét ảnh, vui lòng gửi ảnh có độ sắc nét cao.");
            }
            return GenericApiResponse<DriverLisenceScanResponse>.SuccessResponse(result);
        }
    }
}
