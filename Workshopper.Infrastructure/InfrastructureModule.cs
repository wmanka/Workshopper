﻿using ConfigCat.Client;
using FastEndpoints.Security;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTelemetry.Metrics;
using Workshopper.Application;
using Workshopper.Application.Bus;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Infrastructure.Authentication;
using Workshopper.Infrastructure.Common.Persistence;
using Workshopper.Infrastructure.FeatureFlags;
using Workshopper.Infrastructure.FilesStore;
using Workshopper.Infrastructure.MessageBus;
using Workshopper.Infrastructure.Notifications;
using Workshopper.Infrastructure.Notifications.Persistence;
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
            .AddTelemetry()
            .AddMessageBus(configuration)
            .AddNotifications()
            .AddFilesStore(configuration, environment);
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

        services.AddScoped<INotificationsRepository, NotificationsRepository>();

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

    private static IServiceCollection AddFeatureFlags(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        if (!environment.IsDevelopment() && !environment.IsEnvironment("Development.Container"))
        {
            var featureFlagsSettings = new FeatureFlagsOptions();
            configuration.Bind(FeatureFlagsOptions.SectionName, featureFlagsSettings);
            services.AddSingleton(Options.Create(featureFlagsSettings));
            services.AddSingleton<IValidateOptions<FeatureFlagsOptions>, FeatureFlagsOptionsValidator>();

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
                            CustomFeatureFlagOverrides.LocalDictionary,
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

    private static IServiceCollection AddMessageBus(
        this IServiceCollection services, IConfiguration configuration)
    {
        var messageBusOptions = new MessageBusOptions();
        configuration.Bind(MessageBusOptions.SectionName, messageBusOptions);
        services.AddSingleton(Options.Create(messageBusOptions));
        services.AddSingleton<IValidateOptions<MessageBusOptions>, MessageBusOptionsValidator>();

        return services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) => // todo: sqs dla staging i prod
            {
                cfg.AutoStart = true;
                cfg.Durable = true;
                cfg.ExchangeType = "fanout";

                cfg.Host(messageBusOptions.Host,
                    messageBusOptions.VirtualHost,
                    h =>
                    {
                        h.Username(messageBusOptions.User);
                        h.Password(messageBusOptions.Password);
                    });

                cfg.ConfigureEndpoints(context);
            });

            x.AddEntityFrameworkOutbox<WorkshopperDbContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
            });

            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumer<SessionCanceledNotificationRequestConsumer>();
        });
    }

    private static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddSingleton<IUserIdProvider, UserIdProvider>();

        return services;
    }
}