﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Workshopper.Application.Sessions.Commands.CancelSession;
using Workshopper.Application.Sessions.Commands.CreateSession;
using Workshopper.Application.Sessions.Events;
using Workshopper.Application.Users.Commands.Login;
using Workshopper.Application.Users.Commands.Register;
using Workshopper.Application.Users.Events;
using Workshopper.Domain.Sessions.Events;
using Workshopper.Domain.Users.Events;

namespace Workshopper.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateOnlineSessionCommandValidator>();
        services.AddEventHandlers();

        return services;
    }

    private static void AddEventHandlers(this IServiceCollection services)
    {
        services.TryAddSingleton<IEventHandler<UserRegisteredDomainEvent>, UserRegisteredEventHandler>();
        services.TryAddSingleton<IEventHandler<UserLoggedInDomainEvent>, UserLoggedInEventHandler>();

        services.TryAddSingleton<IEventHandler<SessionCanceledDomainEvent>, SessionCanceledEventHandler>();
    }
}