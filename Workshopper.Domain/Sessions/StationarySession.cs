namespace Workshopper.Domain.Sessions;

public sealed class StationarySession : Session
{
    public string Address { get; private set; } // todo: to value object

    public StationarySession(
        string title,
        string? description,
        SessionType sessionType,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        int places,
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
        DeliveryType = DeliveryType.Stationary;
        Address = address;
    }

    private StationarySession()
        : this(default!, default!, default!, default!, default!, default!, default!)
    {
    }
}