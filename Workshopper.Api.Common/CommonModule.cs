using Microsoft.Extensions.DependencyInjection;
using Workshopper.Application.Common.Abstractions;

namespace Workshopper.Api.Common;

public static class CommonModule
{
    public static IServiceCollection AddPresentationCommon(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        services.AddHealthChecks();

        return services;
    }
}