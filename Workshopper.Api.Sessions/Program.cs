﻿using FastEndpoints.Swagger;
using FluentValidation;
using Workshopper.Api.Sessions;
using Workshopper.Application;
using Workshopper.Infrastructure;

ValidatorOptions.Global.LanguageManager.Enabled = false;

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
        .UseFastEndpoints(c =>
        {
            c.Errors.UseProblemDetails();
        })
        .UseSwaggerGen();

    app.MapHealthChecks("/health");

    app.Run();
}