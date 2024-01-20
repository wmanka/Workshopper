namespace Workshopper.Domain.Subscriptions;

public class Subscription
{
    private readonly Guid _adminId;

    public Guid Id { get; private set; }

    public SubscriptionType SubscriptionType { get; private set; }

    public Subscription(
        SubscriptionType subscriptionType,
        Guid adminId,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        _adminId = adminId;
        SubscriptionType = subscriptionType;
    }

    private Subscription()
    {
    }
}