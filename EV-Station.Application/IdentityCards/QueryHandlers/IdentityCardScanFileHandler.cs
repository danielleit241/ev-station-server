using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using EV_Station.Application.IdentityCards.Queries;
using MediatR;

namespace EV_Station.Application.IdentityCards.QueryHandlers
{
    public class IdentityCardScanFileHandler : IRequestHandler<IdentityCardScanFile, GenericApiResponse<IdentityCardScanResponse>>
    {
        private readonly ITesseractOcrService _tesseractOcrService;
        private readonly IGeminiAiService _geminiAi;
        public IdentityCardScanFileHandler(ITesseractOcrService tesseractOcrService, IGeminiAiService geminiAi)
        {
            _tesseractOcrService = tesseractOcrService;
            _geminiAi = geminiAi;
        }

        public async Task<GenericApiResponse<IdentityCardScanResponse>> Handle(IdentityCardScanFile request, CancellationToken cancellationToken)
        {
            var rawOcrFrontText = await _tesseractOcrService.ExtractTextFromImageFileAsync(request.dto.FrontImage);

            var rawOcrBackText = await _tesseractOcrService.ExtractTextFromImageFileAsync(request.dto.BackImage);

            var rawOcrText = rawOcrFrontText + "\n" + rawOcrBackText;

            var result = await _geminiAi.ExtractIdentityCardInfoAsync(rawOcrText);
            if (result == null)
            {
                return GenericApiResponse<IdentityCardScanResponse>.FailResponse("Không thể quét ảnh, vui lòng gửi ảnh có độ sắc nét cao.");
            }

            return GenericApiResponse<IdentityCardScanResponse>.SuccessResponse(result);
        }


    }
}
