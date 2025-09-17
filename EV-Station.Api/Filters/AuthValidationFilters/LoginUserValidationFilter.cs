
namespace EV_Station.Api.Filters.AuthValidationFilters
{
    public class LoginUserValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var loginUser = context.GetArgument<LoginUserDto>(0);
            if (loginUser is null)
            {
                return ValueTask.FromResult<object?>(Results.BadRequest("Invalid user data."));
            }
            if (string.IsNullOrWhiteSpace(loginUser.Email) || !loginUser.Email.Contains("@"))
            {
                return ValueTask.FromResult<object?>(Results.BadRequest("Invalid email address."));
            }
            if (string.IsNullOrWhiteSpace(loginUser.Password) || loginUser.Password.Length < 5)
            {
                return ValueTask.FromResult<object?>(Results.BadRequest("Password must be at least 5 characters long."));
            }
            return await next(context);
        }
    }
}
