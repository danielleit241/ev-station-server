
namespace EV_Station.Api.Filters.AuthValidationFilters
{
    public class GoogleLoginValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var googleLogin = context.GetArgument<GoogleLoginDto>(0);
            if (googleLogin.IdToken is null || string.IsNullOrWhiteSpace(googleLogin.IdToken))
            {
                return ValueTask.FromResult<object?>(Results.BadRequest("Invalid Google ID token."));
            }
            return await next(context);
        }
    }
}
