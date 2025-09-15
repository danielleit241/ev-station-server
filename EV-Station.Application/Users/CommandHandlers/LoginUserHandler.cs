using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EV_Station.Application.Users.CommandHandlers
{
    public class LoginUserHandler : IRequestHandler<LoginUser, GenericApiResponse<UserTokensReponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IJwtService IJwtService;

        public LoginUserHandler(IUnitOfWork uow, IMapper mapper, IJwtService iJwtService)
        {
            _uow = uow;
            _mapper = mapper;
            IJwtService = iJwtService;
        }

        public async Task<GenericApiResponse<UserTokensReponse>> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var userRepo = _uow.Users;

            if (!await userRepo.IsEmailExist(request.dto.Email))
            {
                return GenericApiResponse<UserTokensReponse>.FailResponse("Email is not exist in the system");
            }

            var user = await userRepo.GetByEmail(request.dto.Email);

            if (!VerifyPassword(user, user!.PasswordHash, request.dto.Password))
            {
                return GenericApiResponse<UserTokensReponse>.FailResponse("Password is incorrect");
            }

            var data = new UserTokensReponse
            {
                User = _mapper.Map<UserResponseDto>(user),
                AccessToken = IJwtService.GenerateAccessTokenToken(user),
                RefreshToken = "Not implemented"
            };

            return GenericApiResponse<UserTokensReponse>.SuccessResponse(data, "Login user successfully");
        }

        private bool VerifyPassword(User user, string hashedPassword, string basePassword)
        {
            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(user, hashedPassword, basePassword);

            return result == PasswordVerificationResult.Success;
        }
    }
}
