using AdSyst.Notifications.Application;
using AdSyst.Notifications.BusinessLayer;
using AdSyst.Notifications.DAL.MongoDb;

namespace AdSyst.Notifications.Api.Config.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services,
            ConfigurationHelper configurationHelper
        )
        {
            var mongoSettings = configurationHelper.MongoSettings;

            services.AddNotificationsData(mongoSettings);
            services.AddNotificationBusinessLayer();
            services.AddApplicationLayer(configurationHelper);

            return services;
        }

        private static IServiceCollection AddApplicationLayer(
            this IServiceCollection services,
            ConfigurationHelper configurationHelper
        )
        {
            var emailTemplateOptions = configurationHelper.EmailTemplateOptions;

            var rabbitMqSettings = configurationHelper.RabbitMqSettings;

            services.AddNotificationsApplication(rabbitMqSettings, emailTemplateOptions);
            return services;
        }
    }
}
