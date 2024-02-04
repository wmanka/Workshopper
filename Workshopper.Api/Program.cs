using FastEndpoints.Swagger;
using Workshopper.Api;
using Workshopper.Application;
using Workshopper.Infrastructure;

var builder = WebApplication.CreateBuilder();
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration, builder.Environment);
}

var app = builder.Build();
{
    app
        .UseAuthentication()
        .UseAuthorization()
        .UseDefaultExceptionHandler()
        .UseInfrastructureMiddleware()
        .UseFastEndpoints(x =>
        {
            x.Errors.UseProblemDetails();
        })
        .UseSwaggerGen();

    app.MapHealthChecks("/health");

    app.UseOpenTelemetryPrometheusScrapingEndpoint();

    app.Run();
}