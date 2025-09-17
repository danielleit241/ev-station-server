namespace EV_Station.Api.Filters.AuthValidationFilters
{
    public class RegisterUserValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var registerUser = context.GetArgument<RegisterUserDto>(0);
            if (registerUser is null)
            {
                return ValueTask.FromResult<object?>(Results.BadRequest("Invalid user data."));
            }
            if (string.IsNullOrWhiteSpace(registerUser.Email) || !registerUser.Email.Contains("@"))
            {
                return ValueTask.FromResult<object?>(Results.BadRequest("Invalid email address."));
            }
            if (string.IsNullOrWhiteSpace(registerUser.Password) || registerUser.Password.Length < 5)
            {
                return ValueTask.FromResult<object?>(Results.BadRequest("Password must be at least 5 characters long."));
            }
            return await next(context);
        }
    }
}
