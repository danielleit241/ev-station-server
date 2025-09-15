using Asp.Versioning.Conventions;
using EV_Station.Api.Abstractions;
using EV_Station.Application.Users.DTOs.Requests;
using EV_Station.Application.Users.DTOs.Response;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EV_Station.Api.Endpoints
{
    public class AuthEndpoints : IEndpointDefinition
    {
        private const string Tag = "Auth";
        private const string BasePath = "/api/v{version:apiVersion}/auth";

        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup(BasePath).WithTags(Tag).WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapPost("/register", RegisterUser).WithName("RegisterUser");

        }

        private Results<Ok<UserResponseDto>, NotFound> RegisterUser(RegisterUserDto command, ISender sender)
        {
            var result = new UserResponseDto(Guid.NewGuid(), command.Email, "John Doe", "https://example.com/avatar.jpg", command.RoleName);
            return TypedResults.Ok(result);
        }
    }
}
