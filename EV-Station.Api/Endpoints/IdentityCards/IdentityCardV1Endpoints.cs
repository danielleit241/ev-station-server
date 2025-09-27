namespace EV_Station.Api.Endpoints.IdentityCards
{
    public class IdentityCardV1Endpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup("api/v{version:apiVersion}/identity-cards").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapGet("", GetAllIdentityCard)
                .WithName("GetAllIdentityCard")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Staff, Admin" });

            //v1.MapGet("/{cardNumber}", GetIdentityCardByCardNumber)
            //    .WithName("GetIdentityCardByCardNumber")
            //    .RequireAuthorization();

            v1.MapPost("", CreateIdentityCard)
               .WithName("CreateIdentityCard")
               .RequireAuthorization();

            v1.MapDelete("/{id:Guid}", DeleteIdentityCard)
                .WithName("DeleteIdentityCard")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Staff, Admin, Renter" });

            v1.MapPut("/{id:Guid}", UpdateIdentityCard)
                .WithName("UpdateIdentityCard")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Staff, Admin, Renter" });

            v1.MapGet("users/{id:Guid}", GetIdentityCardByUserId)
                .WithName("GetIdentityCardByUserId")
                .RequireAuthorization();

            v1.MapPost("/scan-url", ScanIdentityCardUrl)
                .WithName("ScanIdentityCardUrl")
                .RequireAuthorization();

            v1.MapPost("/scan-file", ScanIdentityCardFile)
                .Accepts<IFormFile>("multipart/form-data")
                .WithName("ScanIdentityCardFile")
                .RequireAuthorization()
                .DisableAntiforgery();

            v1.MapPut("{cardNumber}/verify", VerifyIdentityCard)
                .WithName("ConfirmIdentityCard")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Staff, Admin" });
        }

        //private async Task<Results<Ok<GenericApiResponse<IdentityCardResponse>>, NotFound>> GetIdentityCardByCardNumber(string cardNumber, IMediator mediator)
        //{
        //    var getIdentityCardByCardNumberQuery = new GetIdentityCardByCardNumber(cardNumber);
        //    var result = await mediator.Send(getIdentityCardByCardNumberQuery);
        //    return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        //}

        private async Task<Results<Ok<GenericApiResponse<IdentityCardResponse>>, NotFound>> VerifyIdentityCard(string cardNumber, [FromBody] string status, IMediator mediator)
        {
            var verifyIdentityCardCommand = new VerifyIdentityCard(cardNumber, status);
            var result = await mediator.Send(verifyIdentityCardCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<IdentityCardResponse>>, NotFound>> UpdateIdentityCard(Guid id, [FromBody] IdentityCardRequest request, IMediator mediator)
        {
            var updateIdentityCardCommand = new UpdateIdentityCard(id, request);
            var result = await mediator.Send(updateIdentityCardCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<IdentityCardResponse>>, NotFound>> DeleteIdentityCard(Guid id, IMediator mediator)
        {
            var deleteIdentityCardCommand = new DeleteIdentityCard(id);
            var result = await mediator.Send(deleteIdentityCardCommand);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<GenericApiResponse<ICollection<IdentityCardResponse>>>, NotFound>> GetAllIdentityCard(IMediator mediator)
        {
            var getAllIdentityCardQuery = new GetAllIdentityCards();
            var result = await mediator.Send(getAllIdentityCardQuery);
            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();

        }

        private async Task<Results<Ok<GenericApiResponse<IdentityCardResponse>>, NotFound>> GetIdentityCardByUserId(Guid id, IMediator mediator)
        {
            var getMyIdentityCardQuery = new GetIdentityCardByUserId(id);
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
