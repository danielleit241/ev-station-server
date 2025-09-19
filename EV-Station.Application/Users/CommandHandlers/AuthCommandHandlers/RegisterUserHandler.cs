using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands.AuthCommands;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Domain.Models;
using MediatR;

namespace EV_Station.Application.Users.CommandHandlers.AuthCommandHandlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, GenericApiResponse<UserResponseDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public RegisterUserHandler(IUnitOfWork uow, IMapper mapper, IPasswordService passwordService)
        {
            _uow = uow;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<GenericApiResponse<UserResponseDto>> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var userRepo = _uow.Users;
                if (await userRepo.IsEmailExist(request.dto.Email))
                {
                    return GenericApiResponse<UserResponseDto>.FailResponse("Email này đã được sử dụng.");
                }

                var user = await GetRegisterUserAsync(request);
                await userRepo.AddAsync(user);
                var userResponse = _mapper.Map<UserResponseDto>(user);


                await _uow.SaveChangesAsync(cancellationToken);
                await _uow.CommitAsync();
                return GenericApiResponse<UserResponseDto>.SuccessResponse(userResponse, "Đăng kí tài khoản thành công");
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return GenericApiResponse<UserResponseDto>.FailResponse($"Đăng kí tài khoản thất bại. Lỗi: {ex.Message}");
            }
        }

        private async Task<User> GetRegisterUserAsync(RegisterUser request)
        {
            var role = await _uow.Roles.GetRoleByName("Renter");
            var provider = await _uow.Providers.GetProviderByName("Local");

            var user = _mapper.Map<User>(request.dto);
            user.Id = Guid.NewGuid();
            user.Email = request.dto.Email.ToLower().Trim();
            user.PasswordHash = _passwordService.HashPassword(request.dto.Password);
            user.RoleId = role!.Id;
            user.ProviderId = provider!.Id;

            return user;
        }
    }
}