using MassTransit;
using Microsoft.Extensions.Logging;
using AdSyst.Common.BusinessLayer.Exceptions;
using AdSyst.Notifications.Application.Enums;
using AdSyst.Notifications.Application.Services.Templates;
using AdSyst.Notifications.BusinessLayer.Interfaces;

namespace AdSyst.Notifications.Application.Shared
{
    public abstract class UserNotifierConsumer<TEvent> : IConsumer<TEvent> where TEvent : class
    {
        private readonly ILogger _logger;
        private readonly INotifyManager _notifyManager;
        private readonly INotifyMessageManager _messageManager;

        public UserNotifierConsumer(
            ILogger logger,
            INotifyManager notifyManager,
            INotifyMessageManager messageManager
        )
        {
            _logger = logger;
            _notifyManager = notifyManager;
            _messageManager = messageManager;
        }

        protected abstract NotificationEvent NotificationEvent { get; }
        protected abstract object GetMessageData(TEvent eventModel);
        protected abstract Guid GetReceiverId(TEvent eventModel);

        public async Task Consume(ConsumeContext<TEvent> context)
        {
            var eventModel = context.Message;
            var receiverId = GetReceiverId(eventModel);

            _logger.UserNotificationEventConsumedEventLog(context.Message.GetType().Name);

            try
            {
                var message = await _messageManager.CreateMessageAsync(
                    NotificationEvent,
                    GetMessageData(eventModel),
                    context.CancellationToken
                );
                await _notifyManager.NotifyUserAsync(
                    receiverId,
                    message,
                    context.CancellationToken
                );
            }
            catch (NotFoundException)
            {
                _logger.NotificationSettingsNotFoundEventLog(receiverId);
            }
        }
    }
}
