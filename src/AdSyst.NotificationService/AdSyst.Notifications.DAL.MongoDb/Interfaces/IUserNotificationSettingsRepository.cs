using AdSyst.Notifications.DAL.MongoDb.Models;

namespace AdSyst.Notifications.DAL.MongoDb.Interfaces
{
    public interface IUserNotificationSettingsRepository
    {
        Task CreateAsync(
            UserNotificationSettings settings,
            CancellationToken cancellationToken = default
        );

        Task<UserNotificationSettings?> GetUserSettingsAsync(
            Guid userId,
            CancellationToken cancellationToken = default
        );

        Task UpdateUserSettingsAsync(
            Guid userId,
            UserNotificationSettings userNotificationSettings,
            CancellationToken cancellationToken = default
        );
    }
}
