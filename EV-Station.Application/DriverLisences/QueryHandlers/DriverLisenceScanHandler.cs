using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.DriverLisences.Queries;
using MediatR;

namespace EV_Station.Application.DriverLisences.QueryHandlers
{
    public class DriverLisenceScanHandler : IRequestHandler<DriverLisenceScan, GenericApiResponse<DriverLisenceScanResponse>>
    {
        private readonly ITesseractOcrService _tesseractOcrService;
        private readonly IGeminiAiService _geminiAi;
        public DriverLisenceScanHandler(ITesseractOcrService tesseractOcrService, IGeminiAiService geminiAi)
        {
            _tesseractOcrService = tesseractOcrService;
            _geminiAi = geminiAi;
        }

        public async Task<GenericApiResponse<DriverLisenceScanResponse>> Handle(DriverLisenceScan request, CancellationToken cancellationToken)
        {
            var rawOcrFrontText = await _tesseractOcrService.ExtractTextFromImageAsync(request.dto.FrontImageUrl);
            var rawOcrBackText = await _tesseractOcrService.ExtractTextFromImageAsync(request.dto.BackImageUrl);

            var rawOcrText = rawOcrFrontText + "\n" + rawOcrBackText;
            var result = await _geminiAi.ExtractDriverLisenceInfoAsync(rawOcrText);
            if (result == null)
            {
                return GenericApiResponse<DriverLisenceScanResponse>.FailResponse("Không thể quét ảnh, vui lòng gửi ảnh có độ sắc nét cao.");
            }
            if (!ValidDriverLicense(result))
            {
                return GenericApiResponse<DriverLisenceScanResponse>.FailResponse("Không thể quét ảnh, vui lòng gửi ảnh có độ sắc nét cao.");
            }
            return GenericApiResponse<DriverLisenceScanResponse>.SuccessResponse(result);
        }

        private bool ValidDriverLicense(DriverLisenceScanResponse data, int maxNullAllowed = 3)
        {
            if (data is null)
                return false;
            int nullOrEmptyCount = 0;
            if (string.IsNullOrWhiteSpace(data.LicenseNumber)) return false;
            if (string.IsNullOrWhiteSpace(data.FullName)) nullOrEmptyCount++;
            if (string.IsNullOrWhiteSpace(data.Nationality)) nullOrEmptyCount++;
            if (string.IsNullOrWhiteSpace(data.Address)) nullOrEmptyCount++;
            if (data.LicenseClass == 0) nullOrEmptyCount++;
            if (!data.ExpiresDate.HasValue) nullOrEmptyCount++;
            if (data.DateOfBirth == default) nullOrEmptyCount++;

            return nullOrEmptyCount <= maxNullAllowed;
        }
    }
}
