namespace Workshopper.Application.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand : ICommand<Guid>
{
    public string SubscriptionType { get; init; } = null!;

    public Guid AdminId { get; init; }
}