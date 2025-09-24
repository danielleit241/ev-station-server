using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.IdentityCards.Queries
{
    public record GetAllIdentityCards : IRequest<GenericApiResponse<ICollection<IdentityCardResponse>>>;
}
