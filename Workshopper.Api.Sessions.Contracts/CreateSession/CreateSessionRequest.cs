namespace Workshopper.Api.Sessions.Contracts.CreateSession;

public record CreateSessionRequest(
    DeliveryType DeliveryType,
    SessionType SessionType,
    string Title,
    string? Description,
    DateTimeOffset StartDateTime,
    DateTimeOffset EndDateTime,
    int Places,
    string? Link = null,
    Address? Address = null);