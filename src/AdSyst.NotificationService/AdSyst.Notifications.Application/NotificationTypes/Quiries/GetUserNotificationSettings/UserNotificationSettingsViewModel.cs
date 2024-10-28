using AdSyst.Notifications.DAL.MongoDb.Enums;

namespace AdSyst.Notifications.Application.NotificationTypes.Quiries.GetUserNotificationSettings
{
    public record UserNotificationSettingsViewModel(
        Guid UserId,
        Dictionary<NotificationType, string> Settings
    );
}
