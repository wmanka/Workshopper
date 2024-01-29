using Workshopper.Domain.Sessions.Events;

namespace Workshopper.Domain.Sessions;

public sealed class OnlineSession : Session
{
    private OnlineSession(
        string title,
        string? description,
        SessionType sessionType,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        int places,
        Guid hostProfileId,
        string link,
        Guid? id = null)
        : base(title,
            description,
            sessionType,
            startDateTime,
            endDateTime,
            places,
            hostProfileId,
            id)
    {
        DeliveryType = DeliveryType.Online;
        Link = link;
    }

    public string Link { get; private set; }

    public static OnlineSession Create(
        string title,
        string? description,
        SessionType sessionType,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        int places,
        Guid hostProfileId,
        string link,
        Guid? id = null)
    {
        var onlineSession = new OnlineSession(title,
            description,
            sessionType,
            startDateTime,
            endDateTime,
            places,
            hostProfileId,
            link,
            id);

        onlineSession._domainEvents.Add(new SessionCreatedDomainEvent(onlineSession.Id));

        return onlineSession;
    }

    private OnlineSession()
        : this(default!, default!, default!, default!, default!, default!, default!, default!)
    {
    }
}