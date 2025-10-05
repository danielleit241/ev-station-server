

using EV_Station.Application.RentalLocation.Commands;
using EV_Station.Application.RentalLocation.Dtos.Requests;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using EV_Station.Application.RentalLocation.Queries;

namespace EV_Station.Api.Endpoints.RentalLocation
{
    public class RentalLocationV1Endpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup("api/v{version:apiVersion}/rental-locations").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapGet("", GetAllRentalLocationAsync)
                .WithName("Get All Rental Locations");

            v1.MapGet("{id:guid}", GetRentalLocationByIdAsync)
                .WithName("Get Rental Location By Id");

            v1.MapPost("", CreateRentalLocationAsync)
                .WithName("Create Rental Location")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

            v1.MapPut("{id:guid}", UpdateRentalLocationAsync)
                .WithName("Update Rental Location")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

            v1.MapDelete("{id:guid}", DeleteRentalLocationAsync)
                .WithName("Delete Rental Location")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

            v1.MapGet("/routes/{id:guid}", GetRouteMarkerAsync);
            v1.MapGet("/routes/find", GetRouteByRentalAndUserAddressAsync);
        }

        private async Task<Results<Ok<GenericApiResponse<RentalLocationResponse>>, NotFound>> DeleteRentalLocationAsync(Guid id, IMediator mediator)
        {
            var command = new DeleteRentalLocation(id);
            var result = await mediator.Send(command);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<RentalLocationResponse>>, NotFound>> UpdateRentalLocationAsync(Guid id, [FromBody] RentalLocationRequest request, IMediator mediator)
        {
            var command = new UpdateRentalLocation(id, request);
            var result = await mediator.Send(command);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<RentalLocationResponse>>, NotFound>> GetRentalLocationByIdAsync(Guid id, IMediator mediator)
        {
            var query = new GetRentalLocationById(id);
            var result = await mediator.Send(query);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<IEnumerable<RentalLocationResponse>>>, NotFound>> GetAllRentalLocationAsync(IMediator mediator)
        {
            var query = new GetAllRentalLocation();
            var result = await mediator.Send(query);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<RentalLocationResponse>>, NotFound>> CreateRentalLocationAsync([FromBody] RentalLocationRequest dto, IMediator mediator)
        {
            var command = new CreateRentalLocation(dto);
            var result = await mediator.Send(command);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<LocationMarkerResponse>>, NotFound>> GetRouteMarkerAsync(Guid id, IMediator mediator)
        {
            var query = new GetRentalLocationMarker(id);
            var result = await mediator.Send(query);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<RouteResponse>>, NotFound>> GetRouteByRentalAndUserAddressAsync(
            [AsParameters] RouteLocationRequest request, IMediator mediator)
        {
            var query = new GetRoute(request);
            var result = await mediator.Send(query);

            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }
    }
}
