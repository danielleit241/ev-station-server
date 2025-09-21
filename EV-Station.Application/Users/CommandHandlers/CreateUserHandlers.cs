using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Domain.Models;
using MediatR;

namespace EV_Station.Application.Users.CommandHandlers
{
    public class CreateUserHandlers : IRequestHandler<CreateUser, GenericApiResponse<UserResponseDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public CreateUserHandlers(IUnitOfWork uow, IMapper mapper, IPasswordService passwordService)
        {
            _uow = uow;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<GenericApiResponse<UserResponseDto>> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var userRepository = _uow.Users;

                if (await userRepository.IsEmailExist(request.dto.email))
                {
                    return GenericApiResponse<UserResponseDto>.FailResponse("Email này đã được sử dụng.");
                }

                var role = await _uow.Roles.GetRoleByName("Renter");
                var provider = await _uow.Providers.GetProviderByName("Local");
                if (role is null || provider is null)
                {
                    return GenericApiResponse<UserResponseDto>.FailResponse("Role hoặc Provider không tồn tại.");
                }

                var user = GetRegisterUser(request, role, provider);
                userRepository.Add(user);

                var userResponse = _mapper.Map<UserResponseDto>(user);

                await _uow.SaveChangesAsync(cancellationToken);
                await _uow.CommitAsync();
                return GenericApiResponse<UserResponseDto>.SuccessResponse(userResponse, "Tạo người dùng thành công");
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return GenericApiResponse<UserResponseDto>.FailResponse($"Đăng kí người dùng thất bại. Lỗi: {ex.Message}");
            }
        }

        private User GetRegisterUser(CreateUser request, Role role, Provider provider)
        {
            var user = _mapper.Map<User>(request.dto);
            user.Id = Guid.NewGuid();
            user.Email = request.dto.email.ToLower().Trim();
            user.PasswordHash = _passwordService.HashPassword(request.dto.password);
            user.RoleId = role.Id;
            user.ProviderId = provider.Id;
            return user;
        }
    }
}
