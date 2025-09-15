using EV_Station.Application.Users.DTOs.Requests;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;

namespace EV_Station.Application.Users.Commands
{
    public record GoogleLoginUser(GoogleLoginDto request) : IRequest<UserResponseDto>;
}
