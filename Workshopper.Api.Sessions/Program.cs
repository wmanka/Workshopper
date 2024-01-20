using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Workshopper.Application;
using Workshopper.Infrastructure;

var builder = WebApplication.CreateBuilder();
{
    builder.Services
        .AddFastEndpoints()
        .SwaggerDocument(o =>
        {
            o.DocumentSettings = s =>
            {
                s.Title = "Workshopper API - Sessions";
                s.Version = "v1";
            };
        });

    builder.Services
        .AddApplication()
        .AddInfrastructure();
}

var app = builder.Build();
{
    app.UseFastEndpoints(x =>
        {
            x.Errors.UseProblemDetails();
        })
        .UseSwaggerGen();

    app.Run();
}