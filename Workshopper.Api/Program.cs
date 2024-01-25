using FastEndpoints.Swagger;
using Workshopper.Api;
using Workshopper.Application;
using Workshopper.Infrastructure;

var builder = WebApplication.CreateBuilder();
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure();
}

var app = builder.Build();
{
    app
        .UseDefaultExceptionHandler()
        .UseInfrastructureMiddleware()
        .UseFastEndpoints(x =>
        {
            x.Errors.UseProblemDetails();
        })
        .UseSwaggerGen();

    app.MapHealthChecks("/health");

    app.Run();
}