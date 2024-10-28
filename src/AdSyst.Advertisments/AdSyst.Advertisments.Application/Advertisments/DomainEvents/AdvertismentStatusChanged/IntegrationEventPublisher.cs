using MediatR;
using AdSyst.Advertisments.Domain.Advertisments.Events;
using AdSyst.Common.Application.Abstractions.EventBus;
using AdSyst.Common.Contracts.Advertisments;

namespace AdSyst.Advertisments.Application.Advertisments.DomainEvents.AdvertismentStatusChanged
{
    public class IntegrationEventPublisher
        : INotificationHandler<AdvertismentStatusChangedDomainEvent>
    {
        private readonly IEventBus _bus;

        public IntegrationEventPublisher(IEventBus bus)
        {
            _bus = bus;
        }

        public Task Handle(
            AdvertismentStatusChangedDomainEvent notification,
            CancellationToken cancellationToken
        )
        {
            var statusChangedEvent = new AdvertismentStatusChangedEvent
            {
                UserId = notification.UserId,
                AdvertismentId = notification.AdvertismentId,
                AdvertismentStatus = notification.AdvertismentStatus.ToString(),
            };
            return _bus.PublishAsync(statusChangedEvent, cancellationToken);
        }
    }
}
