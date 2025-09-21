
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

            v1.MapPost("/scan-url", ScanUrlDriverLisence)
                .WithName("Scan Url");

            v1.MapPost("/scan-file", ScanFileDriverLisence)
                .WithName("Scan File")
                .Accepts<IFormFile>("multipart/form-data")
                .DisableAntiforgery()
                .WithMetadata(new RequestSizeLimitAttribute(104857600));
        }

        private async Task<Results<Ok<GenericApiResponse<DriverLisenceScanResponse>>, NotFound>> ScanFileDriverLisence([FromForm] DriverLisenceScanFileRequest request, IMediator mediator)
        {
            var driverLisenceScanCommand = new DriverLisenceScanFile(request);
            var result = await mediator.Send(driverLisenceScanCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<DriverLisenceScanResponse>>, NotFound>> ScanUrlDriverLisence(DriverLisenceScanUrlRequest request, IMediator mediator)
        {
            var driverLisenceScanCommand = new DriverLisenceScanUrl(request);
            var result = await mediator.Send(driverLisenceScanCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }
    }
}
