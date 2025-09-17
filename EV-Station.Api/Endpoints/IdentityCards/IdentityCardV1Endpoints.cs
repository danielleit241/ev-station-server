
namespace EV_Station.Api.Endpoints.IdentityCards
{
    public class IdentityCardV1Endpoints : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication application)
        {
            var group = application.MapGroup("/api/v1/identitycards");

            group.MapGet("/", () =>
            {
                // Ví dụ trả về danh sách IdentityCards
                return Results.Ok(new { message = "Get all IdentityCards" });
            });
        }
    }
}
