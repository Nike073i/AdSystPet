namespace AdSyst.WebFiles.Api.Config.Extensions
{
    public static class HealthMonitoringExtensions
    {
        public static IServiceCollection AddAppHealthChecks(
            this IServiceCollection services,
            ConfigurationHelper configurationHelper
        )
        {
            var mongoSettings = configurationHelper.MongoSettings;
            var identityServerSettings = configurationHelper.JwtAuthOptions;

            var builder = services.AddHealthChecks();
            builder.AddMongoDb(
                mongoSettings.ConnectionString,
                name: "DatabaseConnectionHealthCheck",
                timeout: TimeSpan.FromSeconds(2)
            );
            builder.AddIdentityServer(
                new Uri(identityServerSettings.Authority!),
                name: "IdentityServerConnectionHealthCheck"
            );

            return services;
        }
    }
}
