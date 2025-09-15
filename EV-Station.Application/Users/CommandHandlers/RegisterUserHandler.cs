using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EV_Station.Application.Users.CommandHandlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, GenericApiResponse<UserResponseDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<UserResponseDto>> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            var userRepo = _uow.Users;
            if (await userRepo.IsEmailExist(request.dto.Email))
            {
                return GenericApiResponse<UserResponseDto>.FailResponse("Email is already in use.");
            }

            var user = _mapper.Map<User>(request.dto);
            user.Id = Guid.NewGuid();
            user.Email = request.dto.Email.ToLower().Trim();
            user.PasswordHash = HashPassword(request.dto.Password);
            user.RoleId = await GetRoleIdByNameAsync("Renter", cancellationToken);
            user.ProviderId = await GetProviderIdByNameAsync("Local", cancellationToken);

            await userRepo.AddAsync(user);
            await _uow.SaveChangesAsync(cancellationToken);

            var userResponse = _mapper.Map<UserResponseDto>(user);
            return GenericApiResponse<UserResponseDto>.SuccessResponse(userResponse, "Register user successfully");
        }

        private async Task<int> GetRoleIdByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            var roleRepo = _uow.Repository<Role>();
            var roles = await roleRepo.GetAllAsync();
            return roles.FirstOrDefault(r => r.Name == roleName)?.Id ?? roles.First().Id;
        }

        private async Task<int> GetProviderIdByNameAsync(string providerName, CancellationToken cancellationToken)
        {
            var providerRepo = _uow.Repository<Provider>();
            var providers = await providerRepo.GetAllAsync();
            return providers.FirstOrDefault(p => p.Name == providerName)?.Id ?? providers.First().Id;
        }

        public static string HashPassword(string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(null!, password);
        }
    }
}