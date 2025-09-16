using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Application.Users.Querries;
using MediatR;

namespace EV_Station.Application.Users.QuerryHandlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsers, GenericApiResponse<ICollection<UserResponseDto>>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<ICollection<UserResponseDto>>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            var users = await _uow.Users.GetAllAsync();
            if (users is null || !users.Any())
            {
                return GenericApiResponse<ICollection<UserResponseDto>>.FailResponse("No users found");
            }
            var usersReponse = _mapper.Map<ICollection<UserResponseDto>>(users);
            return GenericApiResponse<ICollection<UserResponseDto>>.SuccessResponse(usersReponse);
        }
    }
}
