using ConfigCat.Client;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTelemetry.Metrics;
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
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        return services
            .AddApplication()
            .AddAuthentication(configuration)
            .AddAuthorization()
            .AddHttpContextAccessor()
            .AddPersistence()
            .AddFeatureFlags(configuration, environment)
            .AddTelemetry();
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

    private static IServiceCollection AddFeatureFlags(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        if (!environment.IsDevelopment() && !environment.IsEnvironment("Development.Container"))
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
        }
        else
        {
            services.AddSingleton<IConfigCatClient>(_ =>
            {
                return ConfigCatClient.Get("development",
                    options =>
                    {
                        options.FlagOverrides = FlagOverrides.LocalDictionary(
                            CustomFlagOverrides.LocalDictionary,
                            OverrideBehaviour.LocalOnly);
                    });
            });
        }

        return services;
    }

    private static IServiceCollection AddTelemetry(this IServiceCollection services)
    {
        services
            .AddOpenTelemetry()
            .WithMetrics(x =>
            {
                x.AddPrometheusExporter();

                x.AddMeter(
                    "Microsoft.AspNetCore.Hosting",
                    "Microsoft.AspNetCore.Server.Kestrel");

                x.AddView("request-duration",
                    new ExplicitBucketHistogramConfiguration
                    {
                        Boundaries =
                        [
                            0, 0.005, 0.01, 0.025, 0.05, 0.1, 0.25, 0.5, 1, 2.5, 5, 10
                        ]
                    });
            });

        return services;
    }

}