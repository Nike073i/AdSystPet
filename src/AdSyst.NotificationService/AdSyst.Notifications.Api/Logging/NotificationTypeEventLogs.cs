namespace AdSyst.Notifications.Api.Logging
{
    public static partial class NotificationTypeEventLogs
    {
        [LoggerMessage(
            LogLevel.Information,
            "User {UserId} has set up an email notification",
            EventId = 3010101,
            EventName = "EmailNotificationAdded"
        )]
        public static partial void EmailNotificationAddedEventLog(this ILogger logger, Guid userId);

        [LoggerMessage(
            LogLevel.Warning,
            "The user {UserId} was unable to set up an email notification because they do not have email information",
            EventId = 3010102,
            EventName = "EmailNotificationWasNotAddedDueToEmailIsEmpty"
        )]
        public static partial void EmailNotificationWasNotAddedDueToEmailIsEmptyEventLog(
            this ILogger logger,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Information,
            "User {UserId} turned off email notifications",
            EventId = 3010201,
            EventName = "EmailNotificationRemoved"
        )]
        public static partial void EmailNotificationRemovedEventLog(
            this ILogger logger,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Information,
            "User {UserId} received data about his notification settings",
            EventId = 4010301,
            EventName = "UserNotificationSettingsReceived"
        )]
        public static partial void UserNotificationSettingsReceivedEventLog(
            this ILogger logger,
            Guid userId
        );

        [LoggerMessage(
            LogLevel.Warning,
            "User {UserId} was not received notification settings becouse settings was not found",
            EventId = 4010302,
            EventName = "UserNotificationSettingsWasNotFound"
        )]
        public static partial void UserNotificationSettingsWasNotFoundEventLog(
            this ILogger logger,
            Guid userId
        );
    }
}
