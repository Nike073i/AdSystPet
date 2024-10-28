using AdSyst.Notifications.DAL.MongoDb.Enums;

namespace AdSyst.Notifications.BusinessLayer.Interfaces
{
    public interface INotificationSettingsService
    {
        Task AddNotificationTypeAsync(
            Guid userId,
            NotificationType type,
            string addressTo,
            CancellationToken cancellationToken = default
        );

        Task RemoveNotificationTypeAsync(
            Guid userId,
            NotificationType type,
            CancellationToken cancellationToken = default
        );
    }
}
