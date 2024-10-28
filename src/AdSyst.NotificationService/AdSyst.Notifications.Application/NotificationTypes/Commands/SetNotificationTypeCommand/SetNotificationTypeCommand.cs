using MediatR;
using AdSyst.Notifications.DAL.MongoDb.Enums;

namespace AdSyst.Notifications.Application.NotificationTypes.Commands.SetNotificationTypeCommand
{
    public record SetNotificationTypeCommand(
        Guid UserId,
        NotificationType NotificationType,
        string AddressTo
    ) : IRequest<Unit>;
}
