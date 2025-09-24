using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using EV_Station.Application.IdentityCards.Queries;
using MediatR;

namespace EV_Station.Application.IdentityCards.QueryHandlers
{
    public class GetIdentityCardByIdHandler : IRequestHandler<GetIdentityCardById, GenericApiResponse<IdentityCardResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetIdentityCardByIdHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<IdentityCardResponse>> Handle(GetIdentityCardById request, CancellationToken cancellationToken)
        {
            var identityCards = await _uow.IdentityCards.GetAllAsync();
            var identityCard = identityCards.FirstOrDefault(ic => ic.UserId == request.id);
            if (identityCard is null)
            {
                return GenericApiResponse<IdentityCardResponse>.FailResponse("Identity card not found");
            }
            var identityCardResponse = _mapper.Map<IdentityCardResponse>(identityCard);
            return GenericApiResponse<IdentityCardResponse>.SuccessResponse(identityCardResponse);

        }
    }
}
