namespace EV_Station.Api.Endpoints.DriverLisences
{
    public class DriverLicenseV1Endpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup("api/v{version:apiVersion}/driver-licenses").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapPost("", CreateDriverLicense)
                .WithName("Create Driver License")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Renter" });

            v1.MapGet("", GetAllDriverLicense)
                .WithName("Get All Driver License")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Staff, Admin" });

            v1.MapGet("/{id:Guid}", GetListDriverLicenseByUserId)
                .WithName("Get List Driver License By User Id")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Staff, Admin, Renter" });

            v1.MapGet("/{licenseNumber}", GetDriverLicenseByLicenseNumber)
                .WithName("Get Driver License By Number")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Staff, Admin, Renter" });

            v1.MapPut("/{id:Guid}", UpdateDriverLicense)
                .WithName("Update Driver License")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Staff, Admin, Renter" });

            v1.MapDelete("/{id:Guid}", DeleteDriverLicense)
                .WithName("Delete Driver License")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Staff, Admin, Renter" });

            v1.MapPost("/scan-url", ScanUrlDriverLisence)
                .WithName("Scan Url")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Renter" });

            v1.MapPost("/scan-file", ScanFileDriverLisence)
                .WithName("Scan File")
                .Accepts<IFormFile>("multipart/form-data")
                .DisableAntiforgery()
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Renter" });
        }

        private async Task<Results<Ok<GenericApiResponse<DriverLicenseResponse>>, NotFound>> DeleteDriverLicense(Guid id, [FromBody] string licenseNumber, IMediator mediator)
        {
            var deleteDriverLicenseCommand = new DeleteDriverLicense(id, licenseNumber);
            var result = await mediator.Send(deleteDriverLicenseCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<DriverLicenseResponse>>, NotFound>> UpdateDriverLicense(Guid id, [FromBody] DriverLicenseRequest request, IMediator mediator)
        {
            var updateDriverLicenseCommand = new UpdateDriverLicense(id, request);
            var result = await mediator.Send(updateDriverLicenseCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<ICollection<DriverLicenseResponse>>>, NotFound>> GetListDriverLicenseByUserId(Guid id, IMediator mediator)
        {
            var getListDriverLicenseByUserIdQuery = new GetListDriverLicenseByUserId(id);
            var result = await mediator.Send(getListDriverLicenseByUserIdQuery);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<DriverLicenseResponse>>, NotFound>> GetDriverLicenseByLicenseNumber(string licenseNumber, IMediator mediator)
        {
            var getDriverLicenseByLicenseNumberQuery = new GetDriverLicenseByLicenseNumber(licenseNumber);
            var result = await mediator.Send(getDriverLicenseByLicenseNumberQuery);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<ICollection<DriverLicenseResponse>>>, NotFound>> GetAllDriverLicense(IMediator mediator)
        {
            var getAllDriverLicenseQuery = new GetAllDriverLicenses();
            var result = await mediator.Send(getAllDriverLicenseQuery);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
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
