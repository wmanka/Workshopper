using Workshopper.Domain.Common;

namespace Workshopper.Domain.Sessions;

public sealed class HybridSession : Session
{
    public string Link { get; private set; }

    public Address Address { get; private set; }

    public HybridSession(
        string title,
        string? description,
        SessionType sessionType,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        int places,
        Guid hostProfileId,
        string link,
        Address address,
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
        Address = address;
    }

    private HybridSession()
        : this(default!, default!, default!, default!, default!, default!, default!, default!, default!)
    {
    }
}