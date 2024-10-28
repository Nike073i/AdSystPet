using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentDetail;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentSystemData;
using AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeById;
using AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeList;
using AdSyst.Advertisments.Application.Categories.Queries.GetAllCategoriesQuery;
using AdSyst.Advertisments.Application.Categories.Queries.GetCategoryById;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Advertisments.Domain.Categories;
using AdSyst.Advertisments.Infrastructure.Advertisments.Persistence;
using AdSyst.Advertisments.Infrastructure.Advertisments.Services;
using AdSyst.Advertisments.Infrastructure.AdvertismentTypes.Persistence;
using AdSyst.Advertisments.Infrastructure.AdvertismentTypes.Services;
using AdSyst.Advertisments.Infrastructure.Categories.Persistence;
using AdSyst.Advertisments.Infrastructure.Categories.Services;
using AdSyst.Advertisments.Infrastructure.Configurations;
using AdSyst.Advertisments.Infrastructure.Consumers;
using AdSyst.Advertisments.Infrastructure.Data;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;
using AdSyst.Advertisments.Infrastructure.Messaging.Jobs;
using AdSyst.Advertisments.Infrastructure.Messaging.Persistence;
using AdSyst.Common.Application.Abstractions.Data;
using AdSyst.Common.Application.Abstractions.EventBus;
using AdSyst.Common.Contracts;
using AdSyst.Common.Contracts.Moderation;
using AdSyst.Common.Contracts.Settings;
using AdSyst.Common.Infrastructure;
using AdSyst.Common.Infrastructure.EventBus;
using AdSyst.Common.Presentation.Data;
using Quartz;

namespace AdSyst.Advertisments.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var configurationHelper = new ConfigurationHelper(configuration);

            services.AddCaching(configurationHelper.RedisSettings);
            services.AddAuthentication(configuration);
            services.AddClock();

            AddPersistence(services, configurationHelper);
            AddMassTransit(services, configurationHelper);
            AddHealthChecks(services, configurationHelper);
            AddQueryServices(services);
            AddEventMessages(services, configurationHelper);
            return services;
        }

        private static void AddQueryServices(IServiceCollection services)
        {
            services.AddTransient<IGetAdvertismentDetailService, GetAdvertismentDetailService>();
            services.AddTransient<IGetAdvertismentListService, GetAdvertismentListService>();
            services.AddTransient<
                IGetAdvertismentSystemDataService,
                GetAdvertismentSystemDataService
            >();

            services.AddTransient<IGetAdvertismentTypeService, GetAdvertismentTypeService>();
            services.AddTransient<
                IGetAdvertismentTypeListService,
                GetAdvertismentTypeListService
            >();

            services.AddTransient<IGetCategoryService, GetCategoryService>();
            services.AddTransient<IGetCategoryListService, GetCategoryListService>();
        }

        private static void AddHealthChecks(
            IServiceCollection services,
            ConfigurationHelper configurationHelper
        )
        {
            var identityServerSettings = configurationHelper.JwtAuthOptions;
            var redisSettings = configurationHelper.RedisSettings;
            // Проверку состояния RabbitMq выполняет MassTransit автоматически

            var builder = services.AddHealthChecks();
            builder.AddDbContextCheck<AdvertismentDbContext>("DatabaseConnectionHealthCheck");
            builder.AddIdentityServer(
                new Uri(identityServerSettings.Authority!),
                name: "IdentityServerConnectionHealthCheck"
            );
            builder.AddRedis(redisSettings.ConnectionString);
        }

        private static void AddPersistence(
            IServiceCollection services,
            ConfigurationHelper configurationHelper
        )
        {
            services.AddTransient<IDbInitializer, DbInitializer>();

            var sqlServerConnectionSettings = configurationHelper.SqlServerConnectionSettings;
            services.AddDbContext<AdvertismentDbContext>(
                options =>
                    options.UseSqlServer(
                        sqlServerConnectionSettings.GetConnectionString(),
                        x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                    )
            );
            services.AddDbContext<AdvertismentReadDbContext>(
                options =>
                    options.UseSqlServer(
                        sqlServerConnectionSettings.GetConnectionString(),
                        x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                    )
            );
            services.AddSingleton<ISqlConnectionFactory>(
                sp => new MssqlConnectionFactory(sqlServerConnectionSettings.GetConnectionString())
            );
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AdvertismentDbContext>());
            services.AddScoped<IAdvertismentRepository, AdvertismentRepository>();
            services.AddScoped<IAdvertismentTypeRepository, AdvertismentTypeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        private static void AddEventMessages(
            IServiceCollection services,
            ConfigurationHelper configurationHelper
        )
        {
            services.AddSingleton(sp => configurationHelper.EventMessageProcessOptions);
            services.AddTransient<OutboxMessageRepository>();
            services.AddTransient<InboxMessageRepository>();

            services.AddQuartz();
            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
            services.ConfigureOptions<ProcessOutboxMessageJobSetup>();
            services.ConfigureOptions<ProcessInboxMessageJobSetup>();
        }

        private static void AddMassTransit(
            IServiceCollection services,
            ConfigurationHelper configurationHelper
        )
        {
            var rabbitMqSettings = configurationHelper.RabbitMqSettings;
            services.ConfigureMassTransit(
                rabbitMqSettings,
                configurator =>
                {
                    configurator.AddConsumer<IntegrationEventConsumer<CorrectionNoteAddedEvent>>();
                    configurator.AddConsumer<
                        IntegrationEventConsumer<AdvertismentPublicationConfirmedEvent>
                    >();
                },
                (context, rabbitConfigurator) =>
                    RabbitMqConfigure(rabbitMqSettings, context, rabbitConfigurator)
            );
            services.AddTransient<IEventBus, EventBus>();
        }

        private static void RabbitMqConfigure(
            RabbitMqSettings rabbitMqSettings,
            IBusRegistrationContext context,
            IRabbitMqBusFactoryConfigurator rabbitConfigurator
        )
        {
            string serviceName = rabbitMqSettings.ServiceName;
            rabbitConfigurator.ReceiveEndpoint(
                $"CorrectionNoteAdded-{serviceName}",
                e =>
                {
                    e.Consumer<IntegrationEventConsumer<CorrectionNoteAddedEvent>>(context);
                    e.Bind<CorrectionNoteAddedEvent>();
                }
            );
            rabbitConfigurator.ReceiveEndpoint(
                $"AdvertismentPublicationConfirmed-{serviceName}",
                e =>
                {
                    e.Consumer<IntegrationEventConsumer<AdvertismentPublicationConfirmedEvent>>(
                        context
                    );
                    e.Bind<AdvertismentPublicationConfirmedEvent>();
                }
            );
        }
    }
}
