using AdSyst.Notifications.BusinessLayer.Extensions;
using AdSyst.Notifications.BusinessLayer.Interfaces;
using AdSyst.Notifications.DAL.MongoDb.Enums;
using AdSyst.Notifications.DAL.MongoDb.Interfaces;
using AdSyst.Notifications.DAL.MongoDb.Models;

namespace AdSyst.Notifications.BusinessLayer.Services
{
    public class NotificationSettingsService : INotificationSettingsService
    {
        private readonly IUserNotificationSettingsRepository _userSettingsRepository;

        public NotificationSettingsService(
            IUserNotificationSettingsRepository userSettingsRepository
        )
        {
            _userSettingsRepository = userSettingsRepository;
        }

        public async Task AddNotificationTypeAsync(
            Guid userId,
            NotificationType type,
            string addressTo,
            CancellationToken cancellationToken
        )
        {
            var userSettings = await _userSettingsRepository.GetUserSettingsAsync(
                userId,
                cancellationToken
            );
            if (userSettings == null)
            {
                userSettings = new UserNotificationSettings(userId);
                userSettings.NotificationsSettings.Add(type, addressTo);
                await _userSettingsRepository.CreateAsync(userSettings, cancellationToken);
            }
            else
            {
                userSettings.NotificationsSettings[type] = addressTo;
                await _userSettingsRepository.UpdateUserSettingsAsync(
                    userId,
                    userSettings,
                    cancellationToken
                );
            }
        }

        public async Task RemoveNotificationTypeAsync(
            Guid userId,
            NotificationType type,
            CancellationToken cancellationToken
        )
        {
            var userSettings = await _userSettingsRepository.GetUserSettingsOrThrowAsync(
                userId,
                cancellationToken
            );
            userSettings.NotificationsSettings.Remove(type);
            await _userSettingsRepository.UpdateUserSettingsAsync(
                userId,
                userSettings,
                cancellationToken
            );
        }
    }
}
