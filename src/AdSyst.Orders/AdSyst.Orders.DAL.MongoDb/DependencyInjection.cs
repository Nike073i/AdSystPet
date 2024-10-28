using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using AdSyst.Common.DAL.MongoDb.Mappers;
using AdSyst.Common.DAL.MongoDb.Options;
using AdSyst.Orders.DAL.MongoDb.Interfaces;
using AdSyst.Orders.DAL.MongoDb.Models;
using AdSyst.Orders.DAL.MongoDb.Repositories;

namespace AdSyst.Orders.DAL.MongoDb
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrdersData(
            this IServiceCollection services,
            MongoSettings mongoSettings
        )
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            MappersRegistrar.RegisterMappersFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(
                sp => database.GetCollection<Order>(CollectionKeys.OrderCollectionName)
            );
            services.AddTransient<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
