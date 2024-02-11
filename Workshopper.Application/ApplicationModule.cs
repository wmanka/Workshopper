using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Workshopper.Application.Bus;
using Workshopper.Application.Sessions.Commands.CreateSession;
using Workshopper.Application.Sessions.Events;
using Workshopper.Application.Users.Events;
using Workshopper.Domain.Sessions.Events;
using Workshopper.Domain.Users.Events;

namespace Workshopper.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddEventHandlers()
            .AddValidators();

        services.TryAddScoped<IPushNotificationSender, PushNotificationSender>();

        return services;
    }

    private static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        services.TryAddSingleton<IEventHandler<UserRegisteredDomainEvent>, UserRegisteredEventHandler>();
        services.TryAddSingleton<IEventHandler<UserLoggedInDomainEvent>, UserLoggedInEventHandler>();

        services.TryAddSingleton<IEventHandler<SessionCreatedDomainEvent>, SessionCreatedEventHandler>();
        services.TryAddSingleton<IEventHandler<SessionCanceledDomainEvent>, SessionCanceledEventHandler>();

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.TryAddScoped<IValidator<CreateOnlineSessionCommand>, CreateOnlineSessionCommandValidator>();

        return services;
    }
}