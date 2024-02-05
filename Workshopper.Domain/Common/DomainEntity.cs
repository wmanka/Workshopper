namespace Workshopper.Domain.Common;

public class DomainEntity
{
    protected readonly List<IDomainEvent> _domainEvents = [];

    public Guid Id { get; protected set; }

    protected DomainEntity(Guid id)
    {
        Id = id;
    }

    protected DomainEntity()
    {
    }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents => _domainEvents.ToList();

    public List<IDomainEvent> ApplyDomainEvents()
    {
        var domainEvents = _domainEvents.ToList();

        _domainEvents.Clear();

        return domainEvents;
    }
}