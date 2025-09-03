namespace SharedKernel.Domain.Contracts;

public interface IEventBus
{
    Task Publish<TEvent>(List<TEvent> events) where TEvent : DomainEvent;
}
