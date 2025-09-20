using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using EV_Station.Application.IdentityCards.Queries;
using MediatR;

namespace EV_Station.Application.IdentityCards.QueryHandlers
{
    public class GetIdentityCardByIdHandler : IRequestHandler<GetIdentityCardById, GenericApiResponse<IdentityCardResponse>>
    {
        public Task<GenericApiResponse<IdentityCardResponse>> Handle(GetIdentityCardById request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
