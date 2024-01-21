using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Workshopper.Application.Sessions.Commands.CreateSession;

namespace Workshopper.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateOnlineSessionCommandValidator>();

        return services;
    }
}