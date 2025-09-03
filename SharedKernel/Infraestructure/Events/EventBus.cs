using SharedKernel.Domain.Contracts;
using Microsoft.Extensions.Logging;
using MassTransit;

namespace SharedKernel.Infraestructure.Events
{
    public class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<EventBus> _logger;

        public EventBus(IPublishEndpoint publishEndpoint, ILogger<EventBus> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Publish<TEvent>(List<TEvent> events) where TEvent : DomainEvent
        {
            foreach (var @event in events)
            {
                _logger.LogInformation("Publishing event: {Event}", @event.GetType().Name);
                await _publishEndpoint.Publish(@event, @event.GetType(), context =>
                {
                    context.SetRoutingKey(@event.MessageName);
                });
                _logger.LogInformation("Event published: {Event}", @event.GetType().Name);
            }
        }
    }
}
