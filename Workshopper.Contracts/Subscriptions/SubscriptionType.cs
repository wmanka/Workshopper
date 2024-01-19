using System.Text.Json.Serialization;

namespace Workshopper.Contracts.Subscriptions;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubscriptionType
{
    Starter,
    Pro,
    Enterprise
}