using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;

namespace EV_Station.Application.Users.CommandHandlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, UserResponseDto>
    {
        public Task<UserResponseDto> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
