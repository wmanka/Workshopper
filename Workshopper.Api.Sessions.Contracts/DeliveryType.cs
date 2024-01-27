using System.Text.Json.Serialization;

namespace Workshopper.Api.Sessions.Contracts;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DeliveryType
{
    Online,
    Stationary
}