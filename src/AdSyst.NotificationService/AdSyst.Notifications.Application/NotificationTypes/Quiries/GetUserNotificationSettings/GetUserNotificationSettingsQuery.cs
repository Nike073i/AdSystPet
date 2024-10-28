using MediatR;

namespace AdSyst.Notifications.Application.NotificationTypes.Quiries.GetUserNotificationSettings
{
    public record GetUserNotificationSettingsQuery(Guid UserId)
        : IRequest<GetUserNotificationSettingsQueryResponse>;
}
