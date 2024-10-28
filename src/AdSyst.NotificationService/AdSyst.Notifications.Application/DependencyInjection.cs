using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.Common.Application;
using AdSyst.Common.Contracts;
using AdSyst.Common.Contracts.Advertisments;
using AdSyst.Common.Contracts.Moderation;
using AdSyst.Common.Contracts.Orders;
using AdSyst.Common.Contracts.Settings;
using AdSyst.Notifications.Application.Advertisments.AdvertismentCreated;
using AdSyst.Notifications.Application.Advertisments.AdvertismentStatusChanged;
using AdSyst.Notifications.Application.Advertisments.AdvertismentUpdated;
using AdSyst.Notifications.Application.Moderation.CorrectionNoteAdded;
using AdSyst.Notifications.Application.Orders.OrderCanceled;
using AdSyst.Notifications.Application.Orders.OrderCreated;
using AdSyst.Notifications.Application.Orders.OrderStatusUpdated;
using AdSyst.Notifications.Application.Services;
using AdSyst.Notifications.Application.Services.Templates;
using AdSyst.Notifications.Application.Services.ViewEngine;
using AdSyst.Notifications.BusinessLayer.Interfaces;

namespace AdSyst.Notifications.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNotificationsApplication(
            this IServiceCollection services,
            RabbitMqSettings rabbitMqSettings,
            EmailTemplateOptions options
        )
        {
            AddEmailTemplates(services, options);

            services.AddTransient<IEmailSender, EmailSender>();
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
            rabbitConfigurator.ReceiveEndpoint(
                $"AdvertismentStatusChanged-{serviceName}",
                e =>
                {
                    e.Consumer<AdvertismentStatusChangedEventConsumer>(context);
                    e.Bind<AdvertismentStatusChangedEvent>();
                }
            );
            rabbitConfigurator.ReceiveEndpoint(
                $"AdvertismentUpdated-{serviceName}",
                e =>
                {
                    e.Consumer<AdvertismentUpdatedEventConsumer>(context);
                    e.Bind<AdvertismentUpdatedEvent>();
                }
            );
            rabbitConfigurator.ReceiveEndpoint(
                $"CorrectionNoteAdded-{serviceName}",
                e =>
                {
                    e.Consumer<CorrectionNoteAddedEventConsumer>(context);
                    e.Bind<CorrectionNoteAddedEvent>();
                }
            );
            rabbitConfigurator.ReceiveEndpoint(
                $"OrderCanceled-{serviceName}",
                e =>
                {
                    e.Consumer<OrderCanceledEventNotifyBuyerConsumer>(context);
                    e.Consumer<OrderCanceledEventNotifySellerConsumer>(context);
                    e.Bind<OrderCanceledEvent>();
                }
            );
            rabbitConfigurator.ReceiveEndpoint(
                $"OrderCreated-{serviceName}",
                e =>
                {
                    e.Consumer<OrderCreatedEventNotifyBuyerConsumer>(context);
                    e.Consumer<OrderCreatedEventNotifySellerConsumer>(context);
                    e.Bind<OrderCreatedEvent>();
                }
            );
            rabbitConfigurator.ReceiveEndpoint(
                $"OrderStatusUpdated-{serviceName}",
                e =>
                {
                    e.Consumer<OrderStatusUpdatedEventNotifyBuyerConsumer>(context);
                    e.Consumer<OrderStatusUpdatedEventNotifySellerConsumer>(context);
                    e.Bind<OrderStatusUpdatedEvent>();
                }
            );
        }

        private static void AddEmailTemplates(
            IServiceCollection services,
            EmailTemplateOptions options
        )
        {
            services.AddTransient<ITemplateProvider>(sp => new TemplateProvider(options));
            services.AddSingleton<IViewEngine, ViewEngine>();
            services.AddTransient<INotifyMessageManager, HtmlNotifyMessageManager>();
        }
    }
}
