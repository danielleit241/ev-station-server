using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands.AuthCommands;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Domain.Models;
using MediatR;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace EV_Station.Application.Users.CommandHandlers.AuthCommandHandlers
{

    public class GoogleLoginUserHandler : IRequestHandler<GoogleLoginUser, GenericApiResponse<UserTokensReponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IGoogleAuthService _googleAuthService;

        public GoogleLoginUserHandler(IUnitOfWork uow, IMapper mapper, ITokenService tokenService, IGoogleAuthService googleAuthService)
        {
            _uow = uow;
            _mapper = mapper;
            _tokenService = tokenService;
            _googleAuthService = googleAuthService;
        }

        public async Task<GenericApiResponse<UserTokensReponse>> Handle(GoogleLoginUser request, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var payload = await _googleAuthService.VerifyGoogleTokenAsync(request.dto.IdToken);

                User user;
                if (await _uow.Users.IsEmailExist(payload.Email))
                {
                    user = await _uow.Users.GetByEmail(payload.Email);
                }
                else
                {
                    user = await GetRegisterUserAsync(payload);
                    await _uow.Users.AddAsync(user);
                    await _uow.SaveChangesAsync(cancellationToken);
                }

                var data = new UserTokensReponse
                {
                    User = _mapper.Map<UserResponseDto>(user),
                    AccessToken = _tokenService.GenerateAccessToken(user),
                    RefreshToken = "Not implemented"
                };

                await _uow.CommitAsync();
                return GenericApiResponse<UserTokensReponse>.SuccessResponse(data, "Đăng nhập google thành công");
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return GenericApiResponse<UserTokensReponse>.FailResponse($"Đăng nhập thất bại: {ex.Message}");
            }
        }

        private async Task<User> GetRegisterUserAsync(Payload payload)
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
    }
}
