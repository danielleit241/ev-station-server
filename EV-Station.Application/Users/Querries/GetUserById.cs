using EV_Station.Application.Users.DTOs.Response;
using MediatR;

namespace EV_Station.Application.Users.Querries
{
    public record GetUserById : IRequest<UserResponseDto>;
}
