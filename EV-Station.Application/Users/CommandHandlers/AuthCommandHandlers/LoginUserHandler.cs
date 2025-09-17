using AutoMapper;
using EV_Station.Application.Common.Abstractions.IRepositories.IBaseRepositories;
using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands.AuthCommands;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;

namespace EV_Station.Application.Users.CommandHandlers.AuthCommandHandlers
{
    public class LoginUserHandler : IRequestHandler<LoginUser, GenericApiResponse<UserTokensReponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IJwtService IJwtService;
        private readonly IPasswordService _passwordService;

        public LoginUserHandler(IUnitOfWork uow, IMapper mapper, IJwtService iJwtService, IPasswordService passwordService)
        {
            _uow = uow;
            _mapper = mapper;
            IJwtService = iJwtService;
            _passwordService = passwordService;
        }

        public async Task<GenericApiResponse<UserTokensReponse>> Handle(LoginUser request, CancellationToken cancellationToken)
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

            var data = new UserTokensReponse
            {
                User = _mapper.Map<UserResponseDto>(user),
                AccessToken = IJwtService.GenerateAccessTokenToken(user),
                RefreshToken = "Not implemented"
            };

            return GenericApiResponse<UserTokensReponse>.SuccessResponse(data, "Login user successfully");
        }
    }
}
