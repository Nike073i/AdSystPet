using AdSyst.Notifications.DAL.MongoDb.Enums;

namespace AdSyst.Notifications.DAL.MongoDb.Models
{
    public class UserNotificationSettings
    {
        public Guid UserId { get; init; }

        public Dictionary<NotificationType, string> NotificationsSettings { get; init; }

        public UserNotificationSettings(Guid userId)
        {
            UserId = userId;
            NotificationsSettings = new();
        }
    }
}
