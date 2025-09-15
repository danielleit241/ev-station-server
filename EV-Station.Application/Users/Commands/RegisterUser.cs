using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.DTOs.Requests;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;

namespace EV_Station.Application.Users.Commands
{
    public record RegisterUser(RegisterUserDto dto) : IRequest<GenericApiResponse<UserResponseDto>>;
}
