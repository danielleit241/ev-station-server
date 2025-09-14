using EV_Station.Api.Abstractions;
using EV_Station.Application.Common.Abstractions.IRepositories;
using EV_Station.Infrastructure.Persistence.Data;
using EV_Station.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EV_Station.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<EVStationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }

        public static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            return builder;
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
