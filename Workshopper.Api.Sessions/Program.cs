using FastEndpoints.Swagger;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Workshopper.Api.Sessions;
using Workshopper.Application;
using Workshopper.Infrastructure;

ValidatorOptions.Global.LanguageManager.Enabled = false;

var builder = WebApplication.CreateBuilder();
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure();

    builder.Services.AddHealthChecks();
}

var app = builder.Build();
{
    app.UseFastEndpoints(c =>
        {
            c.Errors.UseProblemDetails();
        })
        .UseSwaggerGen();

    app.MapHealthChecks("/health");

    app.Run();
}