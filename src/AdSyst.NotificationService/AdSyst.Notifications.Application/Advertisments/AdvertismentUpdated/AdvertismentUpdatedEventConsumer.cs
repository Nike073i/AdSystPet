using Microsoft.Extensions.Logging;
using AdSyst.Common.Contracts.Advertisments;
using AdSyst.Notifications.Application.Enums;
using AdSyst.Notifications.Application.Services.Templates;
using AdSyst.Notifications.Application.Shared;
using AdSyst.Notifications.BusinessLayer.Interfaces;

namespace AdSyst.Notifications.Application.Advertisments.AdvertismentUpdated
{
    public class AdvertismentUpdatedEventConsumer : UserNotifierConsumer<AdvertismentUpdatedEvent>
    {
        public AdvertismentUpdatedEventConsumer(
            ILogger<AdvertismentUpdatedEventConsumer> logger,
            INotifyManager notifyManager,
            INotifyMessageManager messageManager
        ) : base(logger, notifyManager, messageManager) { }

        protected override NotificationEvent NotificationEvent => NotificationEvent.AdvertismentUpdated;

        protected override object GetMessageData(AdvertismentUpdatedEvent eventModel) =>
            new { eventModel.AdvertismentId, eventModel.AdvertismentStatus };
        protected override Guid GetReceiverId(AdvertismentUpdatedEvent eventModel) => eventModel.UserId;
    }
}
