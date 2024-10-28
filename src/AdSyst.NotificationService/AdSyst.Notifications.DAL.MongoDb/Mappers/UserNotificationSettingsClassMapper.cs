using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using AdSyst.Common.DAL.MongoDb.Mappers;
using AdSyst.Notifications.DAL.MongoDb.Enums;
using AdSyst.Notifications.DAL.MongoDb.Models;

namespace AdSyst.Notifications.DAL.MongoDb.Mappers
{
    public class UserNotificationSettingsClassMapper
        : IClassMapper<UserNotificationSettingsClassMapper>
    {
        public void Register()
        {
            BsonClassMap.RegisterClassMap<UserNotificationSettings>(classMap =>
            {
                classMap.AutoMap();
                classMap.MapIdMember(us => us.UserId);
                classMap
                    .MapMember(us => us.NotificationsSettings)
                    .SetSerializer(
                        new DictionaryInterfaceImplementerSerializer<
                            Dictionary<NotificationType, string>
                        >(DictionaryRepresentation.ArrayOfDocuments)
                    );
                classMap.MapCreator(model => new UserNotificationSettings(model.UserId));
            });
        }
    }
}
