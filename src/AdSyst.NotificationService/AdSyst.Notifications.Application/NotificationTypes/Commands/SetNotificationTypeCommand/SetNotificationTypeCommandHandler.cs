using MediatR;
using AdSyst.Notifications.BusinessLayer.Interfaces;

namespace AdSyst.Notifications.Application.NotificationTypes.Commands.SetNotificationTypeCommand
{
    public class SetNotificationTypeCommandHandler
        : IRequestHandler<SetNotificationTypeCommand, Unit>
    {
        private readonly INotificationSettingsService _service;

        public SetNotificationTypeCommandHandler(INotificationSettingsService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(
            SetNotificationTypeCommand request,
            CancellationToken cancellationToken
        )
        {
            await _service.AddNotificationTypeAsync(
                request.UserId,
                request.NotificationType,
                request.AddressTo,
                cancellationToken
            );
            return Unit.Value;
        }
    }
}
