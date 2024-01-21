using FastEndpoints.Swagger;
using Microsoft.Extensions.DependencyInjection;

namespace Workshopper.Api.Sessions;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddFastEndpoints()
            .SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Title = "Workshopper API - Sessions";
                    s.Version = "v1";
                };
            });

        return services;
    }
}