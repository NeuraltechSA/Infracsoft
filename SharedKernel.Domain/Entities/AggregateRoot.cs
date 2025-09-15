using System.Collections.Generic;
using SharedKernel.Domain.Contracts;
namespace SharedKernel.Domain.Entities;

public abstract class AggregateRoot : Entity
{
    private readonly List<DomainEvent> _domainEvents = new();

    protected void RecordDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public List<DomainEvent> PullDomainEvents()
    {
        var events = new List<DomainEvent>(_domainEvents);
        _domainEvents.Clear();
        return events;
    }
}