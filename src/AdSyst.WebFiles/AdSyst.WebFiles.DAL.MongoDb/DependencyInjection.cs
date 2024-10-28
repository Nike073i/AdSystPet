using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using AdSyst.Common.DAL.MongoDb.Mappers;
using AdSyst.Common.DAL.MongoDb.Options;
using AdSyst.WebFiles.DAL.MongoDb.Interfaces;
using AdSyst.WebFiles.DAL.MongoDb.Models;
using AdSyst.WebFiles.DAL.MongoDb.Repositories;

namespace AdSyst.WebFiles.DAL.MongoDb
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebFilesData(
            this IServiceCollection services,
            MongoSettings mongoSettings
        )
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            MappersRegistrar.RegisterMappersFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient<IImageRepository, ImageRepository>(
                sp => new(database.GetCollection<Image>(CollectionKeys.ImagesCollectionName))
            );
            return services;
        }
    }
}
