using Workshopper.Domain.Common;
using Workshopper.Domain.Sessions.Events;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Domain.Sessions;

public abstract class Session : DomainEntity
{
    private readonly List<AttendeeProfile> _attendees = [];

    protected Session(
        string title,
        string? description,
        SessionType sessionType,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        int places,
        Guid hostProfileId,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Title = title;
        Description = description;
        SessionType = sessionType;
        HostProfileId = hostProfileId;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Places = places;
    }

    public DeliveryType DeliveryType { get; protected init; } = null!;

    public SessionType SessionType { get; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public DateTimeOffset StartDateTime { get; private set; }

    public DateTimeOffset EndDateTime { get; private set; }

    public int Places { get; }

    public bool IsCanceled { get; private set; }

    public Guid HostProfileId { get; set; }

    public HostProfile HostProfile { get; set; }

    public IReadOnlyList<AttendeeProfile> Attendees => _attendees.AsReadOnly();

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

        _domainEvents.Add(new SessionCanceledDomainEvent(this));
    }

    public void AddAttendee(AttendeeProfile attendee)
    {
        if (Attendees.Any(x => x.Id == attendee.Id))
        {
            throw new DomainException(SessionErrors.AttendeeAlreadyAdded);
        }

        if (Attendees.Count >= Places)
        {
            throw new DomainException(SessionErrors.SessionIsFull);
        }

        _attendees.Add(attendee);
    }

    public void RemoveAttendee(AttendeeProfile attendee)
    {
        if (Attendees.All(x => x.Id != attendee.Id))
        {
            throw new DomainException(SessionErrors.AttendeeNotAdded);
        }

        _attendees.Remove(attendee);
    }

    private Session()
        : this(default!, default!, default!, default!, default!, default!, default!)
    {
    }
}