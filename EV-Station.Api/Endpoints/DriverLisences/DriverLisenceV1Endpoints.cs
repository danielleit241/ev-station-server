
using EV_Station.Application.DriverLisences.Commands;
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

            v1.MapPost("", CreateDriverLicense)
                .WithName("Create Driver License")
                .RequireAuthorization();

            v1.MapPost("/scan-url", ScanUrlDriverLisence)
                .WithName("Scan Url");

            v1.MapPost("/scan-file", ScanFileDriverLisence)
                .WithName("Scan File")
                .Accepts<IFormFile>("multipart/form-data")
                .DisableAntiforgery();
        }

        private async Task<Results<Ok<GenericApiResponse<DriverLicenseResponse>>, NotFound>> CreateDriverLicense(ICurrentUserService currentUser, DriverLicenseRequest request, IMediator mediator)
        {
            var createDriverLicenseCommand = new CreateDriverLicense(currentUser.UserId, request);
            var result = await mediator.Send(createDriverLicenseCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<DriverLicenseScanResponse>>, NotFound>> ScanFileDriverLisence([FromForm] DriverLisenceScanFileRequest request, IMediator mediator)
        {
            var driverLisenceScanCommand = new DriverLicenseScanFile(request);
            var result = await mediator.Send(driverLisenceScanCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<DriverLicenseScanResponse>>, NotFound>> ScanUrlDriverLisence(DriverLisenceScanUrlRequest request, IMediator mediator)
        {
            var driverLisenceScanCommand = new DriverLicenseScanUrl(request);
            var result = await mediator.Send(driverLisenceScanCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }
    }
}
