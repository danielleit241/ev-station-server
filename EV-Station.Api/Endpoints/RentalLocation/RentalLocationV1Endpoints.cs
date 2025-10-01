

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

            v1.MapGet("/routes/{id:guid}", GetRouteMarker);
            v1.MapGet("/routes/find", GetRouteByRentalAndUserAddress);
        }

        private async Task<Results<Ok<GenericApiResponse<LocationResponse>>, NotFound>> GetRouteMarker(Guid id, IMediator mediator)
        {
            var query = new GetRentalLocationMarker(id);
            var result = await mediator.Send(query);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<RouteLocationResponse>>, NotFound>> GetRouteByRentalAndUserAddress(
            [AsParameters] RouteLocationRequest request, IMediator mediator)
        {
            var query = new GetRoute(request);
            var result = await mediator.Send(query);

            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }
    }
}
