
﻿using Microsoft.AspNetCore.Http.Features;

namespace EV_Station.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            SwaggerExtensions.AddSwagger(builder);

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600;
            });

            AddDbContext(builder);
            ApiVersioningExtensions.AddApiVersioning(builder);
            CorsExtensions.AddCors(builder);
            AuthenticationExtensions.AddAuthentication(builder);

            builder.AddSerivces();
            builder.AddRepositories();
            builder.RegisterEndpointDefinitions(typeof(Program).Assembly);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthorization();
            builder.Services.AddMediatR(typeof(RegisterUser).Assembly);
            builder.Services.AddMediatR(typeof(IdentityCardScanUrlHandler).Assembly);

            builder.Services.AddAutoMapper(typeof(UserProfile));
            builder.Services.AddAutoMapper(typeof(IdentityCardProfile));


            return builder;
        }

        private static void AddDbContext(IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<EVStationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        }


        public static void RegisterEndpointDefinitions(this IHostApplicationBuilder builder, Assembly assembly)
        {
            var types = assembly.ExportedTypes
                .Where(t => typeof(IEndpointDefinition).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in types)
            {
                builder.Services.AddScoped(typeof(IEndpointDefinition), type);
            }
        }
    }
}