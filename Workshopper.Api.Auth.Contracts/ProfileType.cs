using System.Text.Json.Serialization;

namespace Workshopper.Api.Auth.Contracts;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProfileType
{
    Attendee,
    Host
}