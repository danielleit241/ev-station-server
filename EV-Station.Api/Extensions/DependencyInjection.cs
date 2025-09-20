using EV_Station.Infrastructure.Persistence.SqlServer.Data;

namespace EV_Station.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IHostApplicationBuilder AddSerivces(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();
            builder.Services.AddScoped<ITesseractOcrService, TesseractOcrService>();
            builder.Services.AddScoped<IGeminiAiService, GeminiAiService>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<DatabaseSeeder>();
            builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();

            return builder;
        }

        public static IHostApplicationBuilder AddRepositories(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            //builder.Services.AddScoped<IIdentityCardRepository, IdentityCardRepository>();

            return builder;
        }
    }
}
