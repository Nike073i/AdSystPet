using MediatR;
using AdSyst.Notifications.DAL.MongoDb.Enums;

namespace AdSyst.Notifications.Application.NotificationTypes.Commands.RemoveNotificationTypeCommand
{
    public record RemoveNotificationTypeCommand(Guid UserId, NotificationType Type)
        : IRequest<Unit>;
}
