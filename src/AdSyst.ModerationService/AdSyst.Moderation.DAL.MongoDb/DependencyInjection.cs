using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using AdSyst.Common.DAL.MongoDb.Mappers;
using AdSyst.Common.DAL.MongoDb.Options;
using AdSyst.Moderation.DAL.MongoDb.Consts;
using AdSyst.Moderation.DAL.MongoDb.Interfaces;
using AdSyst.Moderation.DAL.MongoDb.Models;
using AdSyst.Moderation.DAL.MongoDb.Repositories;

namespace AdSyst.Moderation.DAL.MongoDb
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModerationData(
            this IServiceCollection services,
            MongoSettings mongoSettings
        )
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            MappersRegistrar.RegisterMappersFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(
                sp =>
                    database.GetCollection<Advertisment>(CollectionKeys.AdvertismentCollectionName)
            );
            services.AddTransient<IAdvertismentRepository, AdvertismentRepository>();
            return services;
        }
    }
}
