namespace Ordering.Domain.Abstraction;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId> where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public IDomainEvent[] ClearDomainEvents()
    {
        IDomainEvent[] dequeuedEvents = [.. _domainEvents];
        _domainEvents.Clear();
        return dequeuedEvents;
    }
}