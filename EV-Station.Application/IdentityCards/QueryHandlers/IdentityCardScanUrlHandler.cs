using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using EV_Station.Application.IdentityCards.Queries;
using MediatR;

namespace EV_Station.Application.IdentityCards.QueryHandlers
{
    public class IdentityCardScanUrlHandler : IRequestHandler<IdentityCardScanUrl, GenericApiResponse<IdentityCardScanResponse>>
    {
        private readonly ITesseractOcrService _tesseractOcrService;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IGeminiAiService _geminiAi;

        public IdentityCardScanUrlHandler(ITesseractOcrService tesseractOcrService, IUnitOfWork uow, IMapper mapper, IGeminiAiService geminiAi)
        {
            _tesseractOcrService = tesseractOcrService;
            _uow = uow;
            _mapper = mapper;
            _geminiAi = geminiAi;
        }

        public async Task<GenericApiResponse<IdentityCardScanResponse>> Handle(IdentityCardScanUrl request, CancellationToken cancellationToken)
        {
            var rawOcrFrontText = await _tesseractOcrService.ExtractTextFromImageUrlAsync(request.dto.FrontImageUrl);
            var rawOcrBackText = await _tesseractOcrService.ExtractTextFromImageUrlAsync(request.dto.BackImageUrl);
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
