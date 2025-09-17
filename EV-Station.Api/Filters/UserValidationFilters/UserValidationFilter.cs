namespace EV_Station.Api.Filters.UserValidationFilters
{
    public class UserValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var user = context.GetArgument<User>(0);
            if (user.Email is null || string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
            {
                return ValueTask.FromResult<object?>(Results.BadRequest("Invalid email address."));
            }
            return await next(context);
        }
    }
}
