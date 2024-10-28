using MediatR;
using AdSyst.Advertisments.Domain.Advertisments.Events;
using AdSyst.Common.Application.Abstractions.EventBus;
using AdSyst.Common.Contracts.Advertisments;

namespace AdSyst.Advertisments.Application.Advertisments.DomainEvents.AdvertismentCreated
{
    public class IntegrationEventPublisher : INotificationHandler<AdvertismentCreatedDomainEvent>
    {
        private readonly IEventBus _bus;

        public IntegrationEventPublisher(IEventBus bus)
        {
            _bus = bus;
        }

        public Task Handle(
            AdvertismentCreatedDomainEvent notification,
            CancellationToken cancellationToken
        )
        {
            var createdEvent = new AdvertismentCreatedEvent
            {
                UserId = notification.UserId,
                AdvertismentId = notification.AdvertismentId,
                Title = notification.Title,
                AdvertismentStatus = notification.Status.ToString()
            };
            return _bus.PublishAsync(createdEvent, cancellationToken);
        }
    }
}
