
using EV_Station.Application.DriverLisences.DTOs.Requests;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.DriverLisences.Queries;

namespace EV_Station.Api.Endpoints.DriverLisences
{
    public class DriverLisenceV1Endpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup("api/v{version:apiVersion}/driver-licenses").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapPost("/scan", ScanDriverLisence)
                .WithName("Scan");
        }

        private async Task<Results<Ok<GenericApiResponse<DriverLisenceScanResponse>>, NotFound>> ScanDriverLisence(DriverLisenceScanRequest request, IMediator mediator)
        {
            var driverLisenceScanCommand = new DriverLisenceScan(request);
            var result = await mediator.Send(driverLisenceScanCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }
    }
}
