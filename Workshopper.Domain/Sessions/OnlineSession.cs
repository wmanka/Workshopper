namespace Workshopper.Domain.Sessions;

public sealed class OnlineSession : Session
{
    public string Link { get; private set; }

    public OnlineSession(
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

    private OnlineSession()
        : this(default!, default!, default!, default!, default!, default!, default!, default!)
    {
    }
}