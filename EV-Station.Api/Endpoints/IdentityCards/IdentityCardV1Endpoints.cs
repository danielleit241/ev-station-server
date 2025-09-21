
namespace EV_Station.Api.Endpoints.IdentityCards
{
    public class IdentityCardV1Endpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup("api/v{version:apiVersion}/identity-cards").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapPost("/scan-url", ScanIdentityCardUrl)
                .WithName("ScanIdentityCardUrl")
                .RequireAuthorization();

            v1.MapPost("/scan-file", ScanIdentityCardFile)
                .Accepts<IFormFile>("multipart/form-data")
                .WithName("ScanIdentityCardFile")
                .RequireAuthorization()
                .DisableAntiforgery()
                .WithMetadata(new RequestSizeLimitAttribute(104857600));

            v1.MapPost("/create", CreateIdentityCard)
                .WithName("CreateIdentityCard")
                .RequireAuthorization();

            v1.MapGet("/{id}", GetMyIdentityCard)
                .WithName("GetMyIdentityCard")
                .RequireAuthorization();
        }

        private async Task<Results<Ok<GenericApiResponse<IdentityCardResponse>>, NotFound>> GetMyIdentityCard([FromBody] string cardNumber, IMediator mediator)
        {

            var getMyIdentityCardQuery = new GetIdentityCardById(cardNumber);
            var result = await mediator.Send(getMyIdentityCardQuery);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<IdentityCardResponse>>, NotFound>> CreateIdentityCard(ICurrentUserService currentUserService, IdentityCardRequest request, IMediator mediator)
        {
            var userId = currentUserService.UserId;
            var createIdentityCardCommand = new CreateIdentityCard(userId, request);
            var result = await mediator.Send(createIdentityCardCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<IdentityCardScanResponse>>, NotFound>> ScanIdentityCardFile([FromForm] IdentityCardScanFileRequest request, IMediator mediator)
        {
            var result = await mediator.Send(new IdentityCardScanFile(request));

            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<IdentityCardScanResponse>>, NotFound>> ScanIdentityCardUrl(IdentityCardScanUrlRequest request, IMediator mediator)
        {
            var identityCardScanUrlCommand = new IdentityCardScanUrl(request);
            var result = await mediator.Send(identityCardScanUrlCommand);

            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }
    }
}
