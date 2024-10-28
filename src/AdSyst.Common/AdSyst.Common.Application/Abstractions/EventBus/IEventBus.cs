using AdSyst.Common.Contracts.Abstractions;

namespace AdSyst.Common.Application.Abstractions.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<TIntegrationEvent>(
            TIntegrationEvent integrationEvent,
            CancellationToken cancellationToken
        )
            where TIntegrationEvent : class, IIntegrationEvent;
    }
}
