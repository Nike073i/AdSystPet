using Microsoft.Extensions.Logging;

namespace AdSyst.Notifications.Application.Shared
{
    public static partial class UserNotifierConsumerEventLogs
    {
        [LoggerMessage(
            Level = LogLevel.Information,
            Message = "An event {EventName} was received.",
            EventId = 3010301,
            EventName = "UserNotificationEventConsumed"
        )]
        public static partial void UserNotificationEventConsumedEventLog(
            this ILogger logger,
            string eventName
        );

        [LoggerMessage(
            Level = LogLevel.Warning,
            Message = "Notification settings by user {UserId} weren't found",
            EventId = 3010302,
            EventName = "NotificationSettingsNotFound"
        )]
        public static partial void NotificationSettingsNotFoundEventLog(
            this ILogger logger,
            Guid userId
        );
    }
}
