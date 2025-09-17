using EV_Station.Api.Filters.UserValidationFilters;
using EV_Station.Application.Users.Querries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EV_Station.Api.Endpoints.User
{
    public class UserEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup("api/v{version:apiVersion}/users").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapGet("", GetAllUsersAsync)
                .WithName("GetAllUsers")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Staff, Admin" });

            v1.MapGet("/{id:guid}", GetUserByIdAsync)
                .WithName("GetUserById")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Staff, Admin" });

            v1.MapPost("", CreateUserAsync).WithName("CreateUser")
                .AddEndpointFilter<UserValidationFilter>()
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });
        }

        private async Task<Results<Ok<GenericApiResponse<UserResponseDto>>, NotFound>> CreateUserAsync([FromBody] CreateUserDto request, IMediator mediator)
        {
            var createUserCommand = new CreateUser(request);
            var result = await mediator.Send(createUserCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<UserResponseDto>>, NotFound>> GetUserByIdAsync(Guid id, IMediator mediator)
        {
            var getUserByIdQuery = new GetUserById(id);
            var result = await mediator.Send(getUserByIdQuery);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<ICollection<UserResponseDto>>>, NotFound>> GetAllUsersAsync(IMediator mediator)
        {
            var getAllUsersQuery = new GetAllUsers();
            var result = await mediator.Send(getAllUsersQuery);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }
    }
}
