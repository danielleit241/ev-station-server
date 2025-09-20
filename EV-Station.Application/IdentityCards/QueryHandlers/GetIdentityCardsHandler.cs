using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using EV_Station.Application.IdentityCards.Queries;
using MediatR;

namespace EV_Station.Application.IdentityCards.QueryHandlers
{
    public class GetIdentityCardsHandler : IRequestHandler<GetIdentityCards, GenericApiResponse<ICollection<IdentityCardResponse>>>
    {
        public Task<GenericApiResponse<ICollection<IdentityCardResponse>>> Handle(GetIdentityCards request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
