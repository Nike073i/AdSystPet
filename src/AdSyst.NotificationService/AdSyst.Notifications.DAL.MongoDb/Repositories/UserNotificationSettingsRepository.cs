using MongoDB.Driver;
using AdSyst.Notifications.DAL.MongoDb.Interfaces;
using AdSyst.Notifications.DAL.MongoDb.Models;

namespace AdSyst.Notifications.DAL.MongoDb.Repositories
{
    public class UserNotificationSettingsRepository : IUserNotificationSettingsRepository
    {
        private readonly IMongoCollection<UserNotificationSettings> _userSettingsCollection;

        public UserNotificationSettingsRepository(
            IMongoCollection<UserNotificationSettings> collection
        )
        {
            _userSettingsCollection = collection;
        }

        public Task CreateAsync(
            UserNotificationSettings settings,
            CancellationToken cancellationToken
        ) => _userSettingsCollection.InsertOneAsync(settings, cancellationToken: cancellationToken);

        public async Task<UserNotificationSettings?> GetUserSettingsAsync(
            Guid userId,
            CancellationToken cancellationToken
        )
        {
            var cursor = await _userSettingsCollection.FindAsync(
                setting => setting.UserId == userId,
                cancellationToken: cancellationToken
            );
            return await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public Task UpdateUserSettingsAsync(
            Guid userId,
            UserNotificationSettings userNotificationSettings,
            CancellationToken cancellationToken
        ) =>
            _userSettingsCollection.ReplaceOneAsync(
                settings => settings.UserId == userId,
                userNotificationSettings,
                new ReplaceOptions { IsUpsert = false },
                cancellationToken: cancellationToken
            );
    }
}
