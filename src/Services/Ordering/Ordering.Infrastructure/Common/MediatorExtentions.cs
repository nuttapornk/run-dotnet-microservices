using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;

namespace Ordering.Infrastructure;

public static class MediatorExtentions
{
    public static async Task DispatchDomainEvent(this IMediator mediator ,DbContext context)
    {
        var entities = context.ChangeTracker
           .Entries<BaseEntity>()
           .Where(e => e.Entity.DomainEvents.Any())
           .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
