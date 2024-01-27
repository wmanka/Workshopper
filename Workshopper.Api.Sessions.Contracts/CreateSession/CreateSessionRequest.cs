using FastEndpoints;

namespace Workshopper.Api.Sessions.Contracts.CreateSession;

public record CreateSessionRequest(
    DeliveryType DeliveryType,
    SessionType SessionType,
    string Title,
    string? Description,
    DateTimeOffset StartDateTime,
    DateTimeOffset EndDateTime,
    int Places,
    string? Link,
    Address? Address)
{
    [FromClaim("ProfileId")]
    public string HostProfileId { get; set; }
};