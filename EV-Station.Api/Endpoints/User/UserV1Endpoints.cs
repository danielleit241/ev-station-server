using EV_Station.Application.Users.Querries;

namespace EV_Station.Api.Endpoints.User
{
    public class UserEndpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup("api/v{version:apiVersion}/users").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapGet("", GetAllUsersAsync).WithName("GetAllUsers");
        }

        private async Task<Results<Ok<GenericApiResponse<ICollection<UserResponseDto>>>, NotFound>> GetAllUsersAsync(IMediator mediator)
        {
            var getAllUsersQuery = new GetAllUsers();
            var result = await mediator.Send(getAllUsersQuery);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }
    }
}
