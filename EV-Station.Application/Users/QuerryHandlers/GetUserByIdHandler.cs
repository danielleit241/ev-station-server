using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Application.Users.Querries;
using MediatR;

namespace EV_Station.Application.Users.QuerryHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, UserResponseDto>
    {
        public Task<UserResponseDto> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
