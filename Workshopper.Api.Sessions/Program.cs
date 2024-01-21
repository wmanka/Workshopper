using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Workshopper.Api.Sessions;
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
    app.UseFastEndpoints(c =>
        {
            c.Errors.UseProblemDetails();
        })
        .UseSwaggerGen();

    app.Run();
}