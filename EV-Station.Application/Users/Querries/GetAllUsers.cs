using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;

namespace EV_Station.Application.Users.Querries
{
    public record GetAllUsers : IRequest<GenericApiResponse<ICollection<UserResponseDto>>>;
}
