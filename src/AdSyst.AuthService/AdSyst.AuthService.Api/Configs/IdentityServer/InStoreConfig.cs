using AdSyst.AuthService.SqlServerMigrations;

namespace AdSyst.AuthService.Api.Configs.IdentityServer
{
    public static class InStoreConfig
    {
        public static IIdentityServerBuilder AddConfigInStore(
            this IIdentityServerBuilder builder,
            string connectionString
        )
        {
            builder
                .AddIdentityConfigurationContext(connectionString)
                .AddIdentityPersistedGrantContext(connectionString)
                .AddDeveloperSigningCredential();

            return builder;
        }
    }
}
