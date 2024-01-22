using Workshopper.Domain.Common;

namespace Workshopper.Domain.Subscriptions;

public class Subscription : DomainEntity
{
    public string Name { get; private set; }

    public SubscriptionType SubscriptionType { get; private set; }

    public Subscription(
        string name,
        SubscriptionType subscriptionType,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        SubscriptionType = subscriptionType;
    }

    private Subscription()
    {
    }
}