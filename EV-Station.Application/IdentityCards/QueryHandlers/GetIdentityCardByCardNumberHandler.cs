using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using EV_Station.Application.IdentityCards.Queries;
using MediatR;

namespace EV_Station.Application.IdentityCards.QueryHandlers
{
    public class GetIdentityCardByCardNumberHandler : IRequestHandler<GetIdentityCardByCardNumber, GenericApiResponse<IdentityCardResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetIdentityCardByCardNumberHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<GenericApiResponse<IdentityCardResponse>> Handle(GetIdentityCardByCardNumber request, CancellationToken cancellationToken)
        {
            var identityCardRepository = _uow.IdentityCards;
            var identityCards = await identityCardRepository.GetIdentityCardByNumber(request.cardNumber);
            if (identityCards == null)
            {
                return GenericApiResponse<IdentityCardResponse>.FailResponse("Identity card not found for the specified card number.");
            }
            var response = _mapper.Map<IdentityCardResponse>(identityCards);
            return GenericApiResponse<IdentityCardResponse>.SuccessResponse(response, "Identity card retrieved successfully.");
        }
    }
}
