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
    public class LoginUserHandler : IRequestHandler<LoginUser, GenericApiResponse<UserTokensReponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;

        public LoginUserHandler(IUnitOfWork uow, IMapper mapper, ITokenService tokenService, IPasswordService passwordService)
        {
            _uow = uow;
            _mapper = mapper;
            _tokenService = tokenService;
            _passwordService = passwordService;
        }

        public async Task<GenericApiResponse<UserTokensReponse>> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var userRepo = _uow.Users;

                if (!await userRepo.IsEmailExist(request.dto.Email))
                {
                    return GenericApiResponse<UserTokensReponse>.FailResponse("Email is not exist in the system");
                }

                var user = await userRepo.GetByEmail(request.dto.Email);

                if (!_passwordService.VerifyPassword(request.dto.Password, user!.PasswordHash!))
                {
                    return GenericApiResponse<UserTokensReponse>.FailResponse("Password is incorrect");
                }

                var data = await GenerateAndUpdateUserTokensAsync(user, cancellationToken);

                await _uow.SaveChangesAsync(cancellationToken);
                await _uow.CommitAsync();
                return GenericApiResponse<UserTokensReponse>.SuccessResponse(data, "Login user successfully");
            }
            catch
            {
                await _uow.RollbackAsync();
                return GenericApiResponse<UserTokensReponse>.FailResponse("Login user failed");
            }
        }

        private async Task<UserTokensReponse> GenerateAndUpdateUserTokensAsync(User user, CancellationToken cancellationToken)
        {
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var data = new UserTokensReponse
            {
                User = _mapper.Map<UserResponseDto>(user),
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _uow.Users.Update(user);
            await _uow.SaveChangesAsync(cancellationToken);

            return data;
        }
    }
}
