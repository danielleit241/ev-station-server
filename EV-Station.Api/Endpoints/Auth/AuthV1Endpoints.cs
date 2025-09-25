namespace EV_Station.Api.Endpoints.Auth
{
    public class AuthV1Endpoints : IEndpointDefinition
    {

        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup("api/v{version:apiVersion}/auth").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapPost("register", RegisterUserAsync)
                .WithName("RegisterUser")
                .AddEndpointFilter<RegisterUserValidationFilter>();

            v1.MapPost("login", LoginUserAsync)
                .WithName("LoginUser")
                .AddEndpointFilter<LoginUserValidationFilter>();

            v1.MapPost("google-login", GoogleLoginAsync)
                .WithName("GoogleLoginUser")
                .AddEndpointFilter<GoogleLoginValidationFilter>();

            v1.MapPost("refresh-token", RefreshTokenAsync) //chỉ có user đó mới có thể refresh token của họ
                .WithName("RefreshToken")
                .RequireAuthorization();

        }

        private async Task<Results<Ok<GenericApiResponse<UserTokensReponse>>, NotFound>> RefreshTokenAsync(ICurrentUserService currentUserService, [FromBody] string refreshToken, IMediator mediator)
        {
            var request = new UserRefreshTokenDto
            {
                UserId = currentUserService.UserId,
                RefreshToken = refreshToken
            };
            var refreshTokenQuery = new RefreshTokenUser(request);
            var result = await mediator.Send(refreshTokenQuery);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<UserTokensReponse>>, NotFound>> GoogleLoginAsync(GoogleLoginDto dto, IMediator mediator)
        {
            var googleLoginCommand = new GoogleLoginUser(dto);
            var result = await mediator.Send(googleLoginCommand);

            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<UserTokensReponse>>, NotFound>> LoginUserAsync(LoginUserDto dto, IMediator mediator)
        {
            var loginUserCommand = new LoginUser(dto);
            var result = await mediator.Send(loginUserCommand);

            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<UserResponseDto>>, NotFound>> RegisterUserAsync(RegisterUserDto dto, IMediator mediator)
        {
            var registerUserCommand = new RegisterUser(dto);
            var result = await mediator.Send(registerUserCommand);

            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }
    }
}
