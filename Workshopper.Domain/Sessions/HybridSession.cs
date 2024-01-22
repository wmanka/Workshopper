namespace Workshopper.Domain.Sessions;

public sealed class HybridSession : Session
{
    public string Link { get; private set; }

    public string Address { get; private set; }

    public HybridSession(
        string title,
        string? description,
        SessionType sessionType,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        int places,
        string link,
        string address,
        Guid? id = null)
        : base(title,
            description,
            sessionType,
            startDateTime,
            endDateTime,
            places,
            id)
    {
        DeliveryType = DeliveryType.Online;
        Link = link;
        Address = address;
    }

    private HybridSession()
        : this(default!, default!, default!, default!, default!, default!, default!, default!)
    {
    }
}