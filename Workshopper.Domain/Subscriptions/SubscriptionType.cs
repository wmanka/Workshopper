namespace Workshopper.Domain.Subscriptions;

public class SubscriptionType(string name, int value) : SmartEnum<SubscriptionType>(name, value)
{
    public readonly static SubscriptionType Starter = new(nameof(Starter), 0);
    public readonly static SubscriptionType Standard = new(nameof(Starter), 1);
    public readonly static SubscriptionType Pro = new(nameof(Pro), 2);
    public readonly static SubscriptionType Enterprise = new(nameof(Enterprise), 3);
}