using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.Commands;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.IdentityCards.QueryHandlers
{
    public class IdentityCardScanHandler : IRequestHandler<IdentityCardScan, GenericApiResponse<IdentityCardScanResponse>>
    {
        private readonly ITesseractOcrService _tesseractOcrService;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IGeminiAiService _geminiAi;

        public IdentityCardScanHandler(ITesseractOcrService tesseractOcrService, IUnitOfWork uow, IMapper mapper, IGeminiAiService geminiAi)
        {
            _tesseractOcrService = tesseractOcrService;
            _uow = uow;
            _mapper = mapper;
            _geminiAi = geminiAi;
        }

        public async Task<GenericApiResponse<IdentityCardScanResponse>> Handle(IdentityCardScan request, CancellationToken cancellationToken)
        {
            var frontText = await _tesseractOcrService.ExtractTextFromImageAsync(request.dto.FrontImageUrl);
            var backText = await _tesseractOcrService.ExtractTextFromImageAsync(request.dto.BackImageUrl);
            var fullText = frontText + "\n" + backText;

            var result = await _geminiAi.ExtractIdentityCardInfoAsync(fullText);
            if (result == null)
            {
                return GenericApiResponse<IdentityCardScanResponse>.FailResponse("Không thể quét ảnh, vui lòng gửi ảnh có độ sắc nét cao.");
            }
            if (!IsValidIdentityCard(result))
            {
                return GenericApiResponse<IdentityCardScanResponse>.FailResponse("Không thể quét ảnh, vui lòng gửi ảnh có độ sắc nét cao.");
            }


            return GenericApiResponse<IdentityCardScanResponse>.SuccessResponse(result);
        }

        public static bool IsValidIdentityCard(IdentityCardScanResponse data, int maxNullAllowed = 3)
        {
            if (data == null)
                return false;
            int nullOrEmptyCount = 0;

            if (string.IsNullOrWhiteSpace(data.CardNumber)) nullOrEmptyCount++;
            if (string.IsNullOrWhiteSpace(data.FullName)) nullOrEmptyCount++;
            if (string.IsNullOrWhiteSpace(data.Sex)) nullOrEmptyCount++;
            if (string.IsNullOrWhiteSpace(data.Nationality)) nullOrEmptyCount++;
            if (string.IsNullOrWhiteSpace(data.PlaceOfOrigin)) nullOrEmptyCount++;
            if (string.IsNullOrWhiteSpace(data.PlaceOfResidence)) nullOrEmptyCount++;
            if (!data.DateOfBirth.HasValue) nullOrEmptyCount++;
            if (data.CreateDate == default) nullOrEmptyCount++;
            if (data.DayOfExpiry == default) nullOrEmptyCount++;

            return nullOrEmptyCount <= maxNullAllowed;
        }
    }
}
