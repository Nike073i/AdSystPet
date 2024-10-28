using AdSyst.Notifications.Application.Enums;
using AdSyst.Notifications.BusinessLayer.Models;

namespace AdSyst.Notifications.Application.Services.Templates
{
    public interface INotifyMessageManager
    {
        Task<NotifyMessage> CreateMessageAsync(
            NotificationEvent notificationEvent,
            object data,
            CancellationToken cancellationToken = default
        );
    }
}
