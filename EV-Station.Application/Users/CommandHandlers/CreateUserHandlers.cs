using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EV_Station.Application.Users.CommandHandlers
{
    public class CreateUserHandlers : IRequestHandler<CreateUser, GenericApiResponse<UserResponseDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateUserHandlers(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<UserResponseDto>> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var userRepository = _uow.Users;

            if(await userRepository.IsEmailExist(request.dto.email))
            {
                return GenericApiResponse<UserResponseDto>.FailResponse("Email is already in use.");
            }

            
            var role = await _uow.Roles.GetRoleByName("Renter");
            var provider = await _uow.Providers.GetProviderByName("Local");
            if (role is null || provider is null)
            {
                return GenericApiResponse<UserResponseDto>.FailResponse("Role or Provider not found.");
            }
            
            var user = GetRegisterUser(request, role, provider);
            await userRepository.AddAsync(user);
            await _uow.SaveChangesAsync(cancellationToken);

            var userResponse = _mapper.Map<UserResponseDto>(user);
            return GenericApiResponse<UserResponseDto>.SuccessResponse(userResponse, "Create user successfully");
        }

        private User GetRegisterUser(CreateUser request, Role role, Provider provider)
        {
            var user = _mapper.Map<User>(request.dto);
            user.Id = Guid.NewGuid();
            user.Email = request.dto.email.ToLower().Trim();
            user.PasswordHash = HashPassword(request.dto.password);
            user.RoleId = role.Id;
            user.ProviderId = provider.Id;
            return user;
        }


        public static string HashPassword(string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(null!, password);
        }
    }
}
