using MassTransit;
using MediatR;
using AdSyst.Common.Contracts.Moderation;

namespace AdSyst.Moderation.Application.Advertisments.Notifications.AdvertismentPublicationConfirmed
{
    public class AdvertismentPublicationConfirmedPublisher
        : INotificationHandler<AdvertismentPublicationConfirmedNotification>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public AdvertismentPublicationConfirmedPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Handle(
            AdvertismentPublicationConfirmedNotification notification,
            CancellationToken cancellationToken
        )
        {
            var publicationConfirmedEvent = new AdvertismentPublicationConfirmedEvent
            {
                UserId = notification.UserId,
                AdvertismentId = notification.AdvertismentId,
            };
            return _publishEndpoint.Publish(publicationConfirmedEvent, cancellationToken);
        }
    }
}
