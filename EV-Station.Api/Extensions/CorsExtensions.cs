namespace EV_Station.Api.Extensions
{
    public static class CorsExtensions
    {
        public static void AddCors(IHostApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllClients", builder =>
                {
                    builder.SetIsOriginAllowed(_ => true)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
        }
    }
}
