namespace SharedKernel.Domain.Contracts;

public interface IEventBus
{
    Task Publish<TEvent>(TEvent @event) where TEvent : DomainEvent;
    Task Publish<TEvent>(List<TEvent> events) where TEvent : DomainEvent;
}
