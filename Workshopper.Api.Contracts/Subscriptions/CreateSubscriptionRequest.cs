namespace Workshopper.Api.Contracts.Subscriptions;

public record CreateSubscriptionRequest(string Name, SubscriptionType SubscriptionType);