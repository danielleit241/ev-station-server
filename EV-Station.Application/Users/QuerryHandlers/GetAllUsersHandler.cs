using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Application.Users.Querries;
using MediatR;

namespace EV_Station.Application.Users.QuerryHandlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsers, ICollection<UserResponseDto>>
    {
        public Task<ICollection<UserResponseDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
