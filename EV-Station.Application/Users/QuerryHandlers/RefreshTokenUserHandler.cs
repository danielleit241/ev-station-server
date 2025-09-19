using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.DTOs.Response;
using EV_Station.Application.Users.Querries;
using MediatR;

namespace EV_Station.Application.Users.QuerryHandlers
{
    public class RefreshTokenUserHandler : IRequestHandler<RefreshTokenUser, GenericApiResponse<UserTokensReponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public RefreshTokenUserHandler(IUnitOfWork uow, IMapper mapper, ITokenService tokenService)
        {
            _uow = uow;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<GenericApiResponse<UserTokensReponse>> Handle(RefreshTokenUser request, CancellationToken cancellationToken)
        {
            var userRepository = _uow.Users;
            var user = await userRepository.GetByIdAsync(request.dto.userId, u => u.Role);

            if (user == null) {
                return GenericApiResponse<UserTokensReponse>.FailResponse("User not found");
            }
            if(user.RefreshToken != request.dto.refreshToken)
            {
                return GenericApiResponse<UserTokensReponse>.FailResponse("Invalid refresh token");
            }
            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return GenericApiResponse<UserTokensReponse>.FailResponse("Refresh token has expired");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(user);

            var response = new UserTokensReponse
            {
                User = _mapper.Map<UserResponseDto>(user),
                AccessToken = newAccessToken,
                RefreshToken = request.dto.refreshToken
            };

            return GenericApiResponse<UserTokensReponse>.SuccessResponse(response, "Token refreshed successfully");
        }
    }
}
