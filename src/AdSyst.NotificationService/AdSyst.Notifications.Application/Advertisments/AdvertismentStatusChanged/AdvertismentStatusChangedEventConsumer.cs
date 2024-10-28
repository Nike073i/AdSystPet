using Microsoft.Extensions.Logging;
using AdSyst.Common.Contracts.Advertisments;
using AdSyst.Notifications.Application.Enums;
using AdSyst.Notifications.Application.Services.Templates;
using AdSyst.Notifications.Application.Shared;
using AdSyst.Notifications.BusinessLayer.Interfaces;

namespace AdSyst.Notifications.Application.Advertisments.AdvertismentStatusChanged
{
    public class AdvertismentStatusChangedEventConsumer : UserNotifierConsumer<AdvertismentStatusChangedEvent>
    {
        public AdvertismentStatusChangedEventConsumer(
            ILogger<AdvertismentStatusChangedEventConsumer> logger,
            INotifyManager notifyManager,
            INotifyMessageManager messageManager
        ) : base(logger, notifyManager, messageManager) { }

        protected override NotificationEvent NotificationEvent => NotificationEvent.AdvertismentStatusChanged;

        protected override object GetMessageData(AdvertismentStatusChangedEvent eventModel) =>
            new { eventModel.AdvertismentId, eventModel.AdvertismentStatus };
        protected override Guid GetReceiverId(AdvertismentStatusChangedEvent eventModel) => eventModel.UserId;
    }
}
