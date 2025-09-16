using EV_Station.Api.Abstractions;
using EV_Station.Api.Extensions;
using EV_Station.Api.Middlewares;
using EV_Station.Infrastructure.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.AddDependencyInjection();
builder.RegisterEndpointDefinitions(typeof(Program).Assembly);

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
    await scope.ServiceProvider.GetRequiredService<DatabaseSeeder>().SeedData();

    var endpointDefinitions = scope.ServiceProvider.GetServices<IEndpointDefinition>();
    foreach (var endpoint in endpointDefinitions)
    {
        endpoint.RegisterEndpoints(app);
    }
}

app.Run();
