using AdSyst.Common.BusinessLayer.Exceptions;
using AdSyst.Notifications.DAL.MongoDb.Interfaces;
using AdSyst.Notifications.DAL.MongoDb.Models;

namespace AdSyst.Notifications.BusinessLayer.Extensions
{
    public static class UserNotificationSettingsRepositoryExtensions
    {
        public static async Task<UserNotificationSettings> GetUserSettingsOrThrowAsync(
            this IUserNotificationSettingsRepository repository,
            Guid userId,
            CancellationToken cancellationToken
        )
        {
            return await repository.GetUserSettingsAsync(userId, cancellationToken)
                ?? throw new NotFoundException(nameof(UserNotificationSettings), userId);
        }
    }
}
