namespace Workshopper.Api.Contracts.Subscriptions;

public record CreateSubscriptionRequest(SubscriptionType SubscriptionType, Guid AdminId);