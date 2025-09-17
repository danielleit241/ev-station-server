using EV_Station.Application.Common.Responses;
using EV_Station.Application.Users.Querries;
using Microsoft.AspNetCore.Mvc;

namespace EV_Station.Api.Endpoints.User
{
    public class UserEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup("api/v{version:apiVersion}/users").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapGet("", GetAllUsersAsync).WithName("GetAllUsers");
            v1.MapGet("/{id:guid}", GetUserByIdAsync).WithName("GetUserById");
            v1.MapPost("", CreateUserAsync).WithName("CreateUser");
            v1.MapDelete("/{id:guid}", DeleteUserAsync).WithName("DeleteUser");
            v1.MapPut("/{id:guid}", UpdateUserAsync).WithName("UpdateUser");
        }

        private async Task<Results<Ok<GenericApiResponse<UserResponseDto>>, NotFound>> UpdateUserAsync(Guid id, [FromBody] UpdateUserDto dto, IMediator mediator)
        {
            var updateUserCommand = new UpdateUser(id, dto);
            var result = await mediator.Send(updateUserCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<UserResponseDto>>, NotFound>> DeleteUserAsync(Guid id, IMediator mediator)
        {
            var deleteUserCommand = new DeleteUserById(id);
            var result = await mediator.Send(deleteUserCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
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
