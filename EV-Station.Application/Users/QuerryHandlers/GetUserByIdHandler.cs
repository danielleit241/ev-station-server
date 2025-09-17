using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Application.Users.Querries;
using MediatR;

namespace EV_Station.Application.Users.QuerryHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, GenericApiResponse<UserResponseDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<UserResponseDto>> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _uow.Users.GetByIdAsync(request.id);
            if(user is null)
            {
                return GenericApiResponse<UserResponseDto>.FailResponse("User not found");
            }
            var userResponse = _mapper.Map<UserResponseDto>(user);
            return GenericApiResponse<UserResponseDto>.SuccessResponse(userResponse);
        }
    }
}
