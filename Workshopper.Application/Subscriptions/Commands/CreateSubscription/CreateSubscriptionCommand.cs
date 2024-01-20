using Workshopper.Domain.Subscriptions;

namespace Workshopper.Application.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand : ICommand<Guid>
{
    public SubscriptionType SubscriptionType { get; init; } = null!;

    public Guid AdminId { get; init; }
}