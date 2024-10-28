namespace AdSyst.Notifications.Application.NotificationTypes.Quiries.GetUserNotificationSettings
{
    public record GetUserNotificationSettingsQueryResponse(
        UserNotificationSettingsViewModel UserSettings
    );
}
