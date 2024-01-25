using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Workshopper.Application.Sessions.Commands.CancelSession;
using Workshopper.Application.Sessions.Commands.CreateSession;
using Workshopper.Domain.Sessions.Events;

namespace Workshopper.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateOnlineSessionCommandValidator>();

        services.TryAddSingleton<IEventHandler<SessionCanceledEvent>, SessionCanceledEventHandler>();

        return services;
    }
}