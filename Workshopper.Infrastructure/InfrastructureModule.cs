using ConfigCat.Client;
using FastEndpoints.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Workshopper.Application;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Infrastructure.Authentication;
using Workshopper.Infrastructure.Common.Persistence;
using Workshopper.Infrastructure.FeatureFlags;
using Workshopper.Infrastructure.Sessions.Persistence;
using Workshopper.Infrastructure.Subscriptions.Persistence;
using Workshopper.Infrastructure.Users.Persistence;

namespace Workshopper.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddApplication()
            .AddAuthentication(configuration)
            .AddAuthorization()
            .AddHttpContextAccessor()
            .AddPersistence()
            .AddFeatureFlags(configuration);
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services
            .AddOptionsWithValidateOnStart<DatabaseOptions>()
            .BindConfiguration(DatabaseOptions.SectionName);

        services.AddSingleton<IValidateOptions<DatabaseOptions>, DatabaseOptionsValidator>();

        services.AddDbContext<WorkshopperDbContext>();

        services.AddScoped<IUnitOfWork>(x =>
            x.GetRequiredService<WorkshopperDbContext>());

        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();

        services.AddScoped<ISessionsRepository, SessionsRepository>();
        services.AddScoped<IOnlineSessionsRepository, OnlineSessionsRepository>();
        services.AddScoped<IStationarySessionsRepository, StationarySessionsRepository>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtOptions();
        configuration.Bind(JwtOptions.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IValidateOptions<JwtOptions>, JwtOptionsValidator>();

        services.AddJWTBearerAuth(jwtSettings.SigningKey);

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    private static IServiceCollection AddFeatureFlags(this IServiceCollection services, IConfiguration configuration)
    {
        var featureFlagsSettings = new FeatureFlagsSettings();
        configuration.Bind(FeatureFlagsSettings.SectionName, featureFlagsSettings);
        services.AddSingleton(Options.Create(featureFlagsSettings));
        services.AddSingleton<IValidateOptions<FeatureFlagsSettings>, FeatureFlagsOptionsValidator>();

        services.AddSingleton<IConfigCatClient>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<ConfigCatClient>>();
            var pollingInterval = TimeSpan.FromSeconds(featureFlagsSettings.PollingInterval);

            return ConfigCatClient.Get(featureFlagsSettings.Key,
                options =>
                {
                    options.PollingMode = PollingModes.LazyLoad(pollingInterval);
                    options.Logger = new FeatureFlagsLoggerAdapter(logger);
                });
        });

        return services;
    }
}