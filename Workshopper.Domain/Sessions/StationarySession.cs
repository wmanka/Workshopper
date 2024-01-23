using Workshopper.Domain.Common;

namespace Workshopper.Domain.Sessions;

public sealed class StationarySession : Session
{
    public Address Address { get; private set; }

    public StationarySession(
        string title,
        string? description,
        SessionType sessionType,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        int places,
        Address address,
        Guid? id = null)
        : base(title,
            description,
            sessionType,
            startDateTime,
            endDateTime,
            places,
            id)
    {
        DeliveryType = DeliveryType.Stationary;
        Address = address;
    }

    private StationarySession()
        : this(default!, default!, default!, default!, default!, default!, default!)
    {
    }
}