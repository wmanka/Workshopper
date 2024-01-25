namespace Workshopper.Domain.Common;

public class DomainEntity
{
    protected readonly List<IEvent> _domainEvents = [];

    public Guid Id { get; protected set; }

    protected DomainEntity(Guid id)
    {
        Id = id;
    }

    protected DomainEntity()
    {
    }

    public List<IEvent> ApplyDomainEvents()
    {
        var domainEvents = _domainEvents.ToList();

        _domainEvents.Clear();

        return domainEvents;
    }
}