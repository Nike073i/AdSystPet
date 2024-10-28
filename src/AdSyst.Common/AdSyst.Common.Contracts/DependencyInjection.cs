using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.Common.Contracts.Settings;

namespace AdSyst.Common.Contracts
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureMassTransit(
            this IServiceCollection services,
            RabbitMqSettings rabbitMqSettings,
            Action<IBusRegistrationConfigurator>? busConfigurator = null,
            Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? rabbitConfigurator =
                null
        )
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq(
                    (context, cfg) =>
                    {
                        cfg.Host(
                            new Uri(rabbitMqSettings.ConnectionString),
                            h =>
                            {
                                h.Username(rabbitMqSettings.User);
                                h.Password(rabbitMqSettings.Password);
                            }
                        );

                        rabbitConfigurator?.Invoke(context, cfg);
                    }
                );
                busConfigurator?.Invoke(x);
            });

            return services;
        }
    }
}
