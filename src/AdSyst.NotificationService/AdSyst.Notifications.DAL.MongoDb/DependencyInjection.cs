using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using AdSyst.Common.DAL.MongoDb.Mappers;
using AdSyst.Common.DAL.MongoDb.Options;
using AdSyst.Notifications.DAL.MongoDb.Interfaces;
using AdSyst.Notifications.DAL.MongoDb.Models;
using AdSyst.Notifications.DAL.MongoDb.Repositories;

namespace AdSyst.Notifications.DAL.MongoDb
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNotificationsData(
            this IServiceCollection services,
            MongoSettings mongoSettings
        )
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            MappersRegistrar.RegisterMappersFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(
                sp =>
                    database.GetCollection<UserNotificationSettings>(
                        CollectionKeys.UserNotificationSettingsCollectionName
                    )
            );

            services.AddTransient<
                IUserNotificationSettingsRepository,
                UserNotificationSettingsRepository
            >();

            return services;
        }
    }
}
