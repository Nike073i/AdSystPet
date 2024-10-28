using MediatR;
using MongoDB.Driver;
using AdSyst.Common.BusinessLayer.Exceptions;
using AdSyst.Notifications.DAL.MongoDb.Models;

namespace AdSyst.Notifications.Application.NotificationTypes.Quiries.GetUserNotificationSettings
{
    public class GetUserNotificationSettingsQueryHandler
        : IRequestHandler<
            GetUserNotificationSettingsQuery,
            GetUserNotificationSettingsQueryResponse
        >
    {
        private readonly IMongoCollection<UserNotificationSettings> _userSettings;

        public GetUserNotificationSettingsQueryHandler(
            IMongoCollection<UserNotificationSettings> userSettings
        )
        {
            _userSettings = userSettings;
        }

        public async Task<GetUserNotificationSettingsQueryResponse> Handle(
            GetUserNotificationSettingsQuery request,
            CancellationToken cancellationToken
        )
        {
            var filter = Builders<UserNotificationSettings>
                .Filter
                .Eq(settings => settings.UserId, request.UserId);
            var projection = Builders<UserNotificationSettings>
                .Projection
                .Expression(
                    settings =>
                        new UserNotificationSettingsViewModel(
                            settings.UserId,
                            settings.NotificationsSettings
                        )
                );
            var settings =
                await _userSettings
                    .Find(filter)
                    .Project(projection)
                    .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(nameof(UserNotificationSettings), request.UserId);
            return new(settings);
        }
    }
}
