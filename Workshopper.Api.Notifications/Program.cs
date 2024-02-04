using FastEndpoints.Swagger;
using FluentValidation;
using Workshopper.Api.Notifications;
using Workshopper.Api.Notifications.FeatureFlags;
using Workshopper.Api.Notifications.Notifications;
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
            c.Endpoints.Configurator = x =>
            {
                x.PreProcessor<FeatureFlagsProcessor>(Order.Before);
            };
        })
        .UseSwaggerGen();

    app.MapHealthChecks("/health");

    app.MapHub<NotificationsHub>("notifications-hub");

    app.Run();
}