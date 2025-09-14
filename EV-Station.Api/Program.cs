using EV_Station.Api.Abstractions;
using EV_Station.Api.Extensions;
using EV_Station.Api.Middelwares;
using EV_Station.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.AddDependencyInjection();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EVStationDbContext>();
    context.Database.Migrate();

    var endpointDefinitions = scope.ServiceProvider.GetServices<IEndpointDefinition>();
    foreach (var endpoint in endpointDefinitions)
    {
        endpoint.RegisterEndpoints(app);
    }
}

app.Run();
