using Microsoft.Extensions.Logging;
using AdSyst.Common.Contracts.Advertisments;
using AdSyst.Notifications.Application.Enums;
using AdSyst.Notifications.Application.Services.Templates;
using AdSyst.Notifications.Application.Shared;
using AdSyst.Notifications.BusinessLayer.Interfaces;

namespace AdSyst.Notifications.Application.Advertisments.AdvertismentCreated
{
    public class AdvertismentCreatedEventConsumer : UserNotifierConsumer<AdvertismentCreatedEvent>
    {
        public AdvertismentCreatedEventConsumer(
            ILogger<AdvertismentCreatedEventConsumer> logger,
            INotifyManager notifyManager,
            INotifyMessageManager messageManager
        ) : base(logger, notifyManager, messageManager) { }

        protected override NotificationEvent NotificationEvent => NotificationEvent.AdvertismentCreated;

        protected override object GetMessageData(AdvertismentCreatedEvent eventModel) => new
        {
            eventModel.AdvertismentId,
            eventModel.Title,
            eventModel.AdvertismentStatus
        };

        protected override Guid GetReceiverId(AdvertismentCreatedEvent eventModel) => eventModel.UserId;
    }
}
