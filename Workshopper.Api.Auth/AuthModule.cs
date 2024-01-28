using FastEndpoints.Swagger;
using Workshopper.Api.Common;

namespace Workshopper.Api.Auth;

public static class AuthModule
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddFastEndpoints(o =>
            {
                o.IncludeAbstractValidators = true;
                o.SourceGeneratorDiscoveredTypes.AddRange(Workshopper.Api.Auth.DiscoveredTypes.All);
                o.SourceGeneratorDiscoveredTypes.AddRange(Workshopper.Application.DiscoveredTypes.All);
            })
            .AddAuthorization()
            .SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.DocumentName = "Workshopper API - Auth";
                    s.Title = "Workshopper API - Auth";
                    s.Version = "v1";
                };
            })
            .AddPresentationCommon();

        return services;
    }
}