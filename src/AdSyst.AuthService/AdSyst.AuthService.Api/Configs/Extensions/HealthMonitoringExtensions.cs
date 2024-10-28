using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.FeatureManagement;
using AdSyst.AuthService.Api.Configs.Consts;
using AdSyst.AuthService.EfContext.UserData.Contexts;

namespace AdSyst.AuthService.Api.Configs.Extensions
{
    public static class HealthMonitoringExtensions
    {
        public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
        {
            // Проверку состояния RabbitMq выполняет MassTransit автоматически

            var builder = services.AddHealthChecks();

            builder.AddDbContextCheck<UserDataDbContext>("UserDataDatabaseConnectionHealthCheck");

            using var provider = services.BuildServiceProvider();
            var featureManager = provider.GetRequiredService<IFeatureManager>();
            if (
                !featureManager
                    .IsEnabledAsync(FeatureFlagKeys.IsMemoryConfigForIs4Key)
                    .GetAwaiter()
                    .GetResult()
            )
            {
                builder.AddDbContextCheck<PersistedGrantDbContext>(
                    "PersistedGrantDatabaseConnectionHealthCheck"
                );
                builder.AddDbContextCheck<ConfigurationDbContext>(
                    "ConfigurationDatabaseConnectionHealthCheck"
                );
            }

            return services;
        }
    }
}
