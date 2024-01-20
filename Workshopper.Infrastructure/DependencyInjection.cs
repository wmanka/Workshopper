using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Workshopper.Application;
using Workshopper.Application.Common.Interfaces;
using Workshopper.Infrastructure.Common.Persistence;
using Workshopper.Infrastructure.Sessions.Persistence;
using Workshopper.Infrastructure.Subscriptions.Persistence;

namespace Workshopper.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddApplication();

        services
            .AddOptionsWithValidateOnStart<DatabaseOptions>()
            .BindConfiguration(DatabaseOptions.SectionName);

        services.AddSingleton<IValidateOptions<DatabaseOptions>, DatabaseOptionsValidator>();

        services.AddDbContext<WorkshopperDbContext>();

        services.AddScoped<IUnitOfWork>(x =>
            x.GetRequiredService<WorkshopperDbContext>());

        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<IOnlineSessionsRepository, OnlineSessionsRepository>();
        services.AddScoped<IStationarySessionsRepository, StationarySessionsRepository>();

        return services;
    }
}