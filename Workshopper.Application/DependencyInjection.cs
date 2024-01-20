using Microsoft.Extensions.DependencyInjection;

namespace Workshopper.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}