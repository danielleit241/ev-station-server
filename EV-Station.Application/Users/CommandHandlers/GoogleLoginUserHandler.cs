using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;

namespace EV_Station.Application.Users.CommandHandlers
{

    public class GoogleLoginUserHandler : IRequestHandler<GoogleLoginUser, UserResponseDto>
    {
        public Task<UserResponseDto> Handle(GoogleLoginUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
