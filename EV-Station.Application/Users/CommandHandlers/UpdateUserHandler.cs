using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;

namespace EV_Station.Application.Users.CommandHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUser, GenericApiResponse<UserResponseDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<UserResponseDto>> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var userRepository = _uow.Users;

            var user = await userRepository.GetByIdAsync(request.id);

            if (user == null)
            {
                return GenericApiResponse<UserResponseDto>.FailResponse("Tài khoản không tồn tại.");
            }
            user.Email = request.dto.Email ?? user.Email;
            user.AvatarUrl = request.dto.AvatarUrl ?? user.AvatarUrl;
            user.FullName = request.dto.FullName ?? user.FullName;

            userRepository.Update(user);
            await _uow.SaveChangesAsync(cancellationToken);

            var userResponseDto = _mapper.Map<UserResponseDto>(user);
            return GenericApiResponse<UserResponseDto>.SuccessResponse(userResponseDto, "Cập nhật tài khoản thành công");
        }
    }
}
