using Asp.Versioning.Conventions;
using EV_Station.Api.Abstractions;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Commands;
using EV_Station.Application.Users.DTOs.Requests;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EV_Station.Api.Endpoints.Auth
{
    public class AuthEndpoints : IEndpointDefinition
    {

        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup("api/v{version:apiVersion}/auth").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapPost("register", RegisterUserAsync).WithName("RegisterUser");
            v1.MapPost("login", LoginUserAsync).WithName("LoginUser");
            v1.MapPost("google-login", GoogleLoginAsync).WithName("GoogleLoginUser");
        }

        private async Task GoogleLoginAsync(HttpContext context)
        {
            throw new NotImplementedException();
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
