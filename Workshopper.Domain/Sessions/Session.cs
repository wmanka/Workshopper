using Workshopper.Domain.Common;

namespace Workshopper.Domain.Sessions;

public abstract class Session
{
    public Guid Id { get; private set; }

    public DeliveryType DeliveryType { get; protected init; } = null!;

    public SessionType SessionType { get; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public DateTimeOffset StartDateTime { get; private set; }

    public DateTimeOffset EndDateTime { get; private set; }

    public int Places { get; }

    public bool IsCanceled { get; private set; }

    protected Session(
        string title,
        string? description,
        SessionType sessionType,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        int places,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Title = title;
        Description = description;
        SessionType = sessionType;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Places = places;
    }

    public void Cancel()
    {
        if (IsCanceled)
        {
            throw new DomainException(SessionErrors.SessionAlreadyCanceled);
        }

        if (DateTimeOffset.Now >= StartDateTime)
        {
            throw new DomainException(SessionErrors.SessionAlreadyStarted);
        }

        IsCanceled = true;
    }

    private Session()
    {
    }
}