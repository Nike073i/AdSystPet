using System.Reflection;
using MassTransit;
using AdSyst.Common.Contracts;
using AdSyst.Common.Contracts.EmailMessages;
using AdSyst.Common.Contracts.Settings;
using AdSyst.EmailWorker.Consumers;
using AdSyst.EmailWorker.Interfaces;
using AdSyst.EmailWorker.Jobs;
using AdSyst.EmailWorker.Services;
using AdSyst.EmailWorker.Settings;
using Serilog;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(
        (context, services) =>
        {
            var config = context.Configuration;

            var rabbitMqSettings = config
                .GetRequiredSection(nameof(RabbitMqSettings))
                .Get<RabbitMqSettings>()!;

            var emailSettings = config
                .GetRequiredSection(nameof(EmailSettings))
                .Get<EmailSettings>()!;

            services.AddSingleton<IMessageManager, InMemoryQueueMessages>();

            services.AddHostedService(sp =>
            {
                var queueMessages = sp.GetRequiredService<IMessageManager>();
                var logger = sp.GetRequiredService<ILogger<EmailSendJob>>();
                return new EmailSendJob(logger, queueMessages, emailSettings);
            });

            services.ConfigureMassTransit(
                rabbitMqSettings,
                configurator => configurator.AddConsumers(Assembly.GetExecutingAssembly()),
                (context, rabbitConfigurator) =>
                {
                    string serviceName = rabbitMqSettings.ServiceName;
                    rabbitConfigurator.ReceiveEndpoint(
                        $"EmailMessageSent-{serviceName}",
                        e =>
                        {
                            e.Consumer<EmailMessageSentEventConsumer>(context);
                            e.Bind<EmailMessageSentEvent>();
                        }
                    );
                }
            );
        }
    )
    .UseSerilog(
        (builder, loggerConfig) => loggerConfig.ReadFrom.Configuration(builder.Configuration)
    )
    .Build();

host.Run();
