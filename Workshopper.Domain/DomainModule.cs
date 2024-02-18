using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Workshopper.Domain.Common;

namespace Workshopper.Domain;

public static class DomainModule
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.TryAddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}