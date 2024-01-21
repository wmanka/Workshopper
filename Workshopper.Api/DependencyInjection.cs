using FastEndpoints.Swagger;

namespace Workshopper.Api;

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
                    s.Title = "Workshopper API";
                    s.Version = "v1";
                };
            });

        services.AddHealthChecks();

        return services;
    }
}