var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600;
});

builder.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
    app.UseHttpsRedirection();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors("AllowAllClients");

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<DatabaseSeeder>().SeedData();

    var endpointDefinitions = scope.ServiceProvider.GetServices<IEndpointDefinition>();
    foreach (var endpoint in endpointDefinitions)
    {
        endpoint.RegisterEndpoints(app);
    }
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
