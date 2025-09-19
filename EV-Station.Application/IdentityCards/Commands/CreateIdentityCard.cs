using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.DTOs.Requests;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.IdentityCards.Commands
{
    public record CreateIdentityCard(Guid userId, IdentityCardRequest dto) : IRequest<GenericApiResponse<IdentityCardResponse>>;
}
