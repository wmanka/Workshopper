using System.Text.Json.Serialization;

namespace Workshopper.Api.Sessions.Contracts.Sessions;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SessionType
{
    Workshop,
    Lecture,
    Discussion
}