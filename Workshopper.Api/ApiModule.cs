using FastEndpoints.Swagger;

namespace Workshopper.Api;

public static class ApiModule
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddFastEndpoints(o =>
            {
                o.IncludeAbstractValidators = true;
                o.SourceGeneratorDiscoveredTypes.AddRange(Workshopper.Api.DiscoveredTypes.All);
                o.SourceGeneratorDiscoveredTypes.AddRange(Workshopper.Application.DiscoveredTypes.All);
            })
            .AddAuthorization()
            .SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Title = "Workshopper API";
                    s.Version = "v1";
                };
            })
            .AddHealthChecks();

        return services;
    }
}