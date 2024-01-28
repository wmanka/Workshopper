using Microsoft.Extensions.Logging;
using Workshopper.Domain.Users.Events;

namespace Workshopper.Application.Users.Commands.Register;

public class UserRegisteredEventHandler : IEventHandler<UserRegisteredDomainEvent>
{
    private readonly ILogger<UserRegisteredEventHandler> _logger;

    public UserRegisteredEventHandler(ILogger<UserRegisteredEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(UserRegisteredDomainEvent domainEventModel, CancellationToken ct)
    {
        _logger.LogInformation(
            "User with email '{email}' was registered successfully",
            domainEventModel.User.Email);

        return Task.CompletedTask;
    }
}