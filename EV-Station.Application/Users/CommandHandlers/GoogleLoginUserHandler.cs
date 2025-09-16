using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace EV_Station.Application.Users.CommandHandlers
{

    public class GoogleLoginUserHandler : IRequestHandler<GoogleLoginUser, GenericApiResponse<UserTokensReponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IJwtService IJwtService;
        private readonly IConfiguration _config;

        public GoogleLoginUserHandler(IUnitOfWork uow, IMapper mapper, IJwtService jwtService, IConfiguration config)
        {
            _uow = uow;
            _mapper = mapper;
            IJwtService = jwtService;
            _config = config;
        }

        public async Task<GenericApiResponse<UserTokensReponse>> Handle(GoogleLoginUser request, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var payload = await GetPayloadAsync(request.dto.IdToken);

                User user;
                if (await _uow.Users.IsEmailExist(payload.Email))
                {
                    user = await _uow.Users.GetByEmail(payload.Email);
                }
                else
                {
                    user = await GetUserAsync(payload);
                    await _uow.Users.AddAsync(user);
                    await _uow.SaveChangesAsync(cancellationToken);
                }

                var data = new UserTokensReponse
                {
                    User = _mapper.Map<UserResponseDto>(user),
                    AccessToken = IJwtService.GenerateAccessTokenToken(user),
                    RefreshToken = "Not implemented"
                };

                await _uow.CommitAsync();
                return GenericApiResponse<UserTokensReponse>.SuccessResponse(data, "Login with google user successfully");
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return GenericApiResponse<UserTokensReponse>.FailResponse($"Login failed: {ex.Message}");
            }
        }

        private async Task<User> GetUserAsync(Payload payload)
        {
            var role = await _uow.Roles.GetRoleByName("Renter");
            var provider = await _uow.Providers.GetProviderByName("Google");

            return new User
            {
                Id = Guid.NewGuid(),
                AvatarUrl = payload.Picture ?? "",
                Email = payload.Email,
                FullName = payload.Name ?? "",
                RoleId = role!.Id,
                Role = role!,
                ProviderId = provider!.Id,
                Provider = provider!,
            };
        }

        private async Task<Payload> GetPayloadAsync(string idToken)
        {
            var settings = new ValidationSettings
            {
                Audience = new[] {
                    _config["Authentication:Google:ClientId"],
                    _config["Authentication:Google:ClientIdPlayGround"]
                }
            };

            return await ValidateAsync(idToken, settings);
        }
    }
}
