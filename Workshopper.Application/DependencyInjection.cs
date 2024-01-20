using Microsoft.Extensions.DependencyInjection;
using Workshopper.Application.Subscriptions;

namespace Workshopper.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISubscriptionsWriteService, SubscriptionsWriteService>();

        return services;
    }
}