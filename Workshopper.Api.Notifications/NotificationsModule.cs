using FastEndpoints.Swagger;
using Workshopper.Api.Common;

namespace Workshopper.Api.Notifications;

public static class NotificationsModule
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddFastEndpoints(o =>
            {
                o.IncludeAbstractValidators = true;
                o.SourceGeneratorDiscoveredTypes.AddRange(Workshopper.Api.Notifications.DiscoveredTypes.All);
                o.SourceGeneratorDiscoveredTypes.AddRange(Workshopper.Application.DiscoveredTypes.All);
            })
            .AddAuthorization()
            .SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Title = "Workshopper API - Notifications";
                    s.Version = "v1";
                };
            })
            .AddPresentationCommon()
            .AddSignalR();

        return services;
    }
}