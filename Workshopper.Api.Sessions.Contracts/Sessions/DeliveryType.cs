using System.Text.Json.Serialization;

namespace Workshopper.Api.Sessions.Contracts.Sessions;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DeliveryType
{
    Online,
    Stationary
}