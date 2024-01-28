using FastEndpoints.Swagger;
using Microsoft.Extensions.DependencyInjection;
using Workshopper.Api.Sessions.Common;
using Workshopper.Application.Common.Abstractions;

namespace Workshopper.Api.Sessions;

public static class SessionsModule
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
            .AddAuthorization()
            .SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Title = "Workshopper API - Sessions";
                    s.Version = "v1";
                };
            })
            .AddHealthChecks();

        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        return services;
    }
}