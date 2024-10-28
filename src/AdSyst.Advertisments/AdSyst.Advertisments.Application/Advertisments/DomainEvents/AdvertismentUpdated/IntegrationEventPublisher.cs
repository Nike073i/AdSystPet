using MediatR;
using AdSyst.Advertisments.Domain.Advertisments.Events;
using AdSyst.Common.Application.Abstractions.EventBus;
using AdSyst.Common.Contracts.Advertisments;

namespace AdSyst.Advertisments.Application.Advertisments.DomainEvents.AdvertismentUpdated
{
    public class IntegrationEventPublisher : INotificationHandler<AdvertismentUpdatedDomainEvent>
    {
        private readonly IEventBus _bus;

        public IntegrationEventPublisher(IEventBus bus)
        {
            _bus = bus;
        }

        public Task Handle(
            AdvertismentUpdatedDomainEvent notification,
            CancellationToken cancellationToken
        )
        {
            var updatedEvent = new AdvertismentUpdatedEvent
            {
                UserId = notification.UserId,
                AdvertismentId = notification.AdvertismentId,
                AdvertismentStatus = notification.AdvertismentStatus.ToString(),
            };
            return _bus.PublishAsync(updatedEvent, cancellationToken);
        }
    }
}
