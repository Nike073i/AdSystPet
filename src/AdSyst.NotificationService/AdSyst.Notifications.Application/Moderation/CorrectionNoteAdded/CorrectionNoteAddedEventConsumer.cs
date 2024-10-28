using Microsoft.Extensions.Logging;
using AdSyst.Common.Contracts.Moderation;
using AdSyst.Notifications.Application.Enums;
using AdSyst.Notifications.Application.Services.Templates;
using AdSyst.Notifications.Application.Shared;
using AdSyst.Notifications.BusinessLayer.Interfaces;

namespace AdSyst.Notifications.Application.Moderation.CorrectionNoteAdded
{
    public class CorrectionNoteAddedEventConsumer : UserNotifierConsumer<CorrectionNoteAddedEvent>
    {
        public CorrectionNoteAddedEventConsumer(
            ILogger<CorrectionNoteAddedEventConsumer> logger,
            INotifyManager notifyManager,
            INotifyMessageManager messageManager
        ) : base(logger, notifyManager, messageManager) { }

        protected override NotificationEvent NotificationEvent => NotificationEvent.CorrectionNoteAdded;

        protected override object GetMessageData(CorrectionNoteAddedEvent eventModel) =>
            new { eventModel.AdvertismentId, eventModel.NoteTitle };
        protected override Guid GetReceiverId(CorrectionNoteAddedEvent eventModel) => eventModel.AdvertismentAuthorId;
    }
}
