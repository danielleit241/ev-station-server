using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.Commands;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using EV_Station.Domain.Models.Enums;
using MediatR;

namespace EV_Station.Application.IdentityCards.CommandHandlers
{
    public class VerifyIdentityCardHandler : IRequestHandler<VerifyIdentityCard, GenericApiResponse<IdentityCardResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public VerifyIdentityCardHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<IdentityCardResponse>> Handle(VerifyIdentityCard request, CancellationToken cancellationToken)
        {
            var identityCardRepository = _uow.IdentityCards;
            var identityCard = await identityCardRepository.GetIdentityCardByNumber(request.cardNumber);
            if (identityCard == null)
            {
                return GenericApiResponse<IdentityCardResponse>.FailResponse("Không tìm thấy thẻ căn cước công dân");
            }
            if ("verify".Equals(request.status.ToLower().Trim()))
            {
                identityCard.Status = VerificationStatus.Verified;
            }
            else
            {
                identityCard.Status = VerificationStatus.Rejected;
            }

            identityCardRepository.Update(identityCard);
            await _uow.SaveChangesAsync(cancellationToken);
            var identityCardResponse = _mapper.Map<IdentityCardResponse>(identityCard);
            return GenericApiResponse<IdentityCardResponse>.SuccessResponse(identityCardResponse, "Xác thực thẻ căn cước công dân thành công");
        }
    }
}
