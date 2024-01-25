using FastEndpoints.Swagger;
using Microsoft.Extensions.DependencyInjection;

namespace Workshopper.Api.Sessions;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddFastEndpoints(o =>
            {
                o.IncludeAbstractValidators = true;
                o.SourceGeneratorDiscoveredTypes.AddRange(Workshopper.Api.Sessions.DiscoveredTypes.All);
                o.SourceGeneratorDiscoveredTypes.AddRange(Workshopper.Application.DiscoveredTypes.All);
            })
            .SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Title = "Workshopper API - Sessions";
                    s.Version = "v1";
                };
            });

        services.AddHealthChecks();

        return services;
    }
}