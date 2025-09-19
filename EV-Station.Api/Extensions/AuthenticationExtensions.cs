namespace EV_Station.Api.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void AddAuthentication(IHostApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var authHeader = context.Request.Headers["Authorization"].ToString();

                            if (!string.IsNullOrEmpty(authHeader))
                            {
                                if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                                    context.Token = authHeader.Substring("Bearer ".Length).Trim();
                                else
                                    context.Token = authHeader.Trim();
                            }

                            return Task.CompletedTask;
                        }
                    };

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Authentication:Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Authentication:Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Jwt:Key"]!)
                        )
                    };
                });
        }
    }
}
