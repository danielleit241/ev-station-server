
using EV_Station.Application.IdentityCards.Commands;
using EV_Station.Application.IdentityCards.DTOs.Requests;
using EV_Station.Application.IdentityCards.DTOs.Responses;

namespace EV_Station.Api.Endpoints.IdentityCards
{
    public class IdentityCardV1Endpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication application)
        {
            var v1 = application.MapGroup("api/v{version:apiVersion}/identity-cards").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapPost("/scan", ScanIdentityCard);
        }


        private async Task<Results<Ok<GenericApiResponse<IdentityCardScanResponse>>, NotFound>> ScanIdentityCard(IdentityCardScanRequest request, IMediator mediator)
        {
            var identityCardScanCommand = new IdentityCardScan(request);
            var result = await mediator.Send(identityCardScanCommand);

            return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }
    }
}
