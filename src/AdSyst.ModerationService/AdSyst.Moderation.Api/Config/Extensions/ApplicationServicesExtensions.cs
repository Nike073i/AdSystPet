using System.Reflection;
using FluentValidation;
using AdSyst.Moderation.Application;
using AdSyst.Moderation.BusinessLayer;
using AdSyst.Moderation.DAL.MongoDb;

namespace AdSyst.Moderation.Api.Config.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            ConfigurationHelper configurationHelper
        )
        {
            var rabbitMqSettings = configurationHelper.RabbitMqSettings;

            var mongoSettings = configurationHelper.MongoSettings;

            services.AddModerationData(mongoSettings);
            services.AddModerationBusinessLayer();
            services.AddModerationApplication(rabbitMqSettings);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
