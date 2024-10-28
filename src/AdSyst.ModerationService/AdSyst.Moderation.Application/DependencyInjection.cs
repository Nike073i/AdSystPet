using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.Common.Application;
using AdSyst.Common.Contracts;
using AdSyst.Common.Contracts.Advertisments;
using AdSyst.Common.Contracts.Settings;
using AdSyst.Moderation.Application.Advertisments.Events.AdvertismentCreated;

namespace AdSyst.Moderation.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModerationApplication(
            this IServiceCollection services,
            RabbitMqSettings rabbitMqSettings
        )
        {
            services.ConfigureMassTransit(
                rabbitMqSettings,
                configurator => configurator.AddConsumers(Assembly.GetExecutingAssembly()),
                (context, rabbitConfigurator) =>
                    RabbitMqConfigure(rabbitMqSettings, context, rabbitConfigurator)
            );
            return services.AddApplicationServices(Assembly.GetExecutingAssembly());
        }

        private static void RabbitMqConfigure(
            RabbitMqSettings rabbitMqSettings,
            IBusRegistrationContext context,
            IRabbitMqBusFactoryConfigurator rabbitConfigurator
        )
        {
            string serviceName = rabbitMqSettings.ServiceName;
            rabbitConfigurator.ReceiveEndpoint(
                $"AdvertismentCreated-{serviceName}",
                e =>
                {
                    e.Consumer<AdvertismentCreatedEventConsumer>(context);
                    e.Bind<AdvertismentCreatedEvent>();
                }
            );
        }
    }
}
