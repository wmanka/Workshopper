using Workshopper.Domain.Common;
using Workshopper.Domain.Sessions.Events;

namespace Workshopper.Domain.Sessions;

public abstract class Session : DomainEntity
{
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
        : base(id ?? Guid.NewGuid())
    {
        Title = title;
        Description = description;
        SessionType = sessionType;

        if (startDateTime <= DateTimeOffset.Now)
        {
            throw new DomainException(SessionErrors.StartTimeMustBeGreaterThanNow);
        }

        StartDateTime = startDateTime;

        if (endDateTime <= startDateTime)
        {
            throw new DomainException(SessionErrors.EndTimeMustBeGreaterThanStartTime);
        }

        EndDateTime = endDateTime;

        if (places <= 0)
        {
            throw new DomainException(SessionErrors.NumberOfPlacesMustBeGreaterThanZero);
        }

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

        _domainEvents.Add(new SessionCanceledEvent(this));
    }

    private Session()
    {
    }
}