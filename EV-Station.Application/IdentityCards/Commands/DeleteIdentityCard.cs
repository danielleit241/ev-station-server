using EV_Station.Application.Common.Responses;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using MediatR;

namespace EV_Station.Application.IdentityCards.Commands
{
    public record DeleteIdentityCard(string cardNumber) : IRequest<GenericApiResponse<IdentityCardResponse>>;
}
