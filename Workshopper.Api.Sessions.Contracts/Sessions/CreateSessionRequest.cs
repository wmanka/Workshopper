namespace Workshopper.Api.Sessions.Contracts.Sessions;

public record CreateSessionRequest(
    DeliveryType DeliveryType,
    SessionType SessionType,
    string Title,
    string? Description,
    DateTimeOffset StartDateTime,
    DateTimeOffset EndDateTime,
    int Places,
    string? Link,
    string? Address);