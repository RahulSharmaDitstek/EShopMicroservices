namespace Ordering.Domain.Events;

public record OrderUpdatedDomainEvent(Order Order) : IDomainEvent;