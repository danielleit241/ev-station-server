using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;

namespace EV_Station.Application.Users.CommandHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserById, GenericApiResponse<UserResponseDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeleteUserHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<UserResponseDto>> Handle(DeleteUserById request, CancellationToken cancellationToken)
        {
            var userRepository = _uow.Users;
            var user = await userRepository.GetByIdAsync(request.id);
            if (user is null)
            {
                return GenericApiResponse<UserResponseDto>.FailResponse("Tài khoản không tồn tại.");
            }
            userRepository.Delete(user);
            await _uow.SaveChangesAsync(cancellationToken);

            var userResponse = _mapper.Map<UserResponseDto>(user);
            return GenericApiResponse<UserResponseDto>.SuccessResponse(userResponse, "Xóa tài khoản thành công");
        }
    }
}
