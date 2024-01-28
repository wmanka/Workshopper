using Microsoft.Extensions.Logging;
using Workshopper.Domain.Users.Events;

namespace Workshopper.Application.Users.Commands.Login;

public class UserLoggedInEventHandler : IEventHandler<UserLoggedInDomainEvent>
{
    private readonly ILogger<UserLoggedInEventHandler> _logger;

    public UserLoggedInEventHandler(ILogger<UserLoggedInEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(UserLoggedInDomainEvent domainEventModel, CancellationToken ct)
    {
        _logger.LogInformation(
            "User with email '{email}' was logged in successfully",
            domainEventModel.User.Email);

        return Task.CompletedTask;
    }
}