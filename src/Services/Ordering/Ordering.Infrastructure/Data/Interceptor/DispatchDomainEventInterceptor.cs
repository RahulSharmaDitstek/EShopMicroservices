namespace Ordering.Infrastructure.Data.Interceptor;

public class DispatchDomainEventInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvent(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvent(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private async Task DispatchDomainEvent(DbContext? context)
    {
        if (context is null) return;
        var domainEntities = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = domainEntities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        domainEntities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var entity in domainEntities)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                await mediator.Publish(domainEvent);
            }
            entity.ClearDomainEvents();
        }
    }
}
