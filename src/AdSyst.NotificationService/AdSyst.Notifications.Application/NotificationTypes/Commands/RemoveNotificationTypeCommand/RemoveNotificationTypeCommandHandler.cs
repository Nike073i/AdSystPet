using MediatR;
using AdSyst.Notifications.BusinessLayer.Interfaces;

namespace AdSyst.Notifications.Application.NotificationTypes.Commands.RemoveNotificationTypeCommand
{
    public class RemoveNotificationTypeCommandHandler
        : IRequestHandler<RemoveNotificationTypeCommand, Unit>
    {
        private readonly INotificationSettingsService _service;

        public RemoveNotificationTypeCommandHandler(INotificationSettingsService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(
            RemoveNotificationTypeCommand request,
            CancellationToken cancellationToken
        )
        {
            await _service.RemoveNotificationTypeAsync(
                request.UserId,
                request.Type,
                cancellationToken
            );
            return Unit.Value;
        }
    }
}
