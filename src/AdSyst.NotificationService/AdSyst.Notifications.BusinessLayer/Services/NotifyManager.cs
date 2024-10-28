using AdSyst.Notifications.BusinessLayer.Extensions;
using AdSyst.Notifications.BusinessLayer.Interfaces;
using AdSyst.Notifications.BusinessLayer.Models;
using AdSyst.Notifications.DAL.MongoDb.Enums;
using AdSyst.Notifications.DAL.MongoDb.Interfaces;

namespace AdSyst.Notifications.BusinessLayer.Services
{
    public class NotifyManager : INotifyManager
    {
        private readonly IUserNotificationSettingsRepository _userNotificationSettingsRepository;
        private readonly IEmailSender _emailSender;

        public NotifyManager(
            IUserNotificationSettingsRepository notificationSettingsService,
            IEmailSender emailSender
        )
        {
            _userNotificationSettingsRepository = notificationSettingsService;
            _emailSender = emailSender;
        }

        public async Task NotifyUserAsync(
            Guid userId,
            NotifyMessage notifyMessage,
            CancellationToken cancellationToken
        )
        {
            var userSettings =
                await _userNotificationSettingsRepository.GetUserSettingsOrThrowAsync(
                    userId,
                    cancellationToken
                );

            foreach (var notifyWay in userSettings.NotificationsSettings)
            {
                await SendNotificationAsync(
                    notifyWay.Key,
                    notifyMessage,
                    new[] { notifyWay.Value },
                    cancellationToken
                );
            }
        }

        private Task SendNotificationAsync(
            NotificationType type,
            NotifyMessage message,
            string[] addressesTo,
            CancellationToken cancellationToken
        )
        {
            return type switch
            {
                NotificationType.Email
                    => _emailSender.SendMessageAsync(message, addressesTo, cancellationToken),
                _ => throw new NotSupportedException(),
            };
        }
    }
}
