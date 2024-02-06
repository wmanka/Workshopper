namespace Workshopper.Application.Bus;

public record SessionCanceledNotificationRequest(
    Guid Id,
    string Title,
    Guid HostId,
    string HostName,
    (DateTimeOffset StartDateTime, DateTimeOffset EndDateTime) DateTime,
    IEnumerable<Guid> AttendeeIds);