using MassTransit;
using AdSyst.Common.Application.Abstractions.EventBus;

namespace AdSyst.Common.Infrastructure.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IBus _bus;

        public EventBus(IBus bus)
        {
            _bus = bus;
        }

        Task IEventBus.PublishAsync<TIntegrationEvent>(
            TIntegrationEvent integrationEvent,
            CancellationToken cancellationToken
        ) => _bus.Publish(integrationEvent, cancellationToken);
    }
}
