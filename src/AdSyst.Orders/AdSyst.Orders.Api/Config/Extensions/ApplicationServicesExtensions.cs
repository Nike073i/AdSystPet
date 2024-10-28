using System.Reflection;
using FluentValidation;
using AdSyst.Common.Contracts;
using AdSyst.Orders.Application;
using AdSyst.Orders.BusinessLayer;
using AdSyst.Orders.DAL.MongoDb;
using AdSyst.Orders.SyncDataServices;

namespace AdSyst.Orders.Api.Config.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            ConfigurationHelper configurationHelper
        )
        {
            var rabbitMqSettings = configurationHelper.RabbitMqSettings;

            services.ConfigureMassTransit(rabbitMqSettings);

            var advertismentsGrpcClientSettings =
                configurationHelper.AdvertismentsGrpcClientSettings;

            var mongoSettings = configurationHelper.MongoSettings;

            services.AddOrdersData(mongoSettings);
            services.AddOrdersBusinessLayer();
            services.AddOrderApplication();

            services.AddSyncDataServices(advertismentsGrpcClientSettings);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
