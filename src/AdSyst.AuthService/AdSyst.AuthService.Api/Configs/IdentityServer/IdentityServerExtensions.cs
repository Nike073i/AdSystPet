using Microsoft.AspNetCore.Identity;
using Microsoft.FeatureManagement;
using AdSyst.AuthService.Api.Configs.Consts;
using AdSyst.AuthService.Api.Configs.IdentityServer.Initialization;
using AdSyst.AuthService.Domain;
using AdSyst.AuthService.EfContext.UserData.Contexts;
using AdSyst.AuthService.SqlServerMigrations;
using AdSyst.Common.Presentation.Data;

namespace AdSyst.AuthService.Api.Configs.IdentityServer
{
    public static class IdentityServerExtensions
    {
        public static IServiceCollection AddServicesIdentityServer(
            this IServiceCollection services,
            ConfigurationHelper configurationHelper
        )
        {
            var sqlServerConnectionSettings = configurationHelper.SqlServerConnectionSettings;
            string connectionString = sqlServerConnectionSettings.GetConnectionString();
            services.AddIdentityUserData(connectionString);

            services.AddTransient<IDbInitializer, UserDbInitializer>();

            services
                .AddIdentity<AppUser, IdentityRole>(
                    options => options.SignIn.RequireConfirmedEmail = true
                )
                .AddEntityFrameworkStores<UserDataDbContext>()
                .AddDefaultTokenProviders();

            var identityServerConfigs = configurationHelper.IdentityServerConfigs;

            var identityServerBuilder = services
                .AddIdentityServer(config => config.IssuerUri = identityServerConfigs.IssuerUri)
                .AddAspNetIdentity<AppUser>()
                .AddDeveloperSigningCredential();

            using var provider = services.BuildServiceProvider();
            var featureManager = provider.GetRequiredService<IFeatureManager>();
            if (
                featureManager
                    .IsEnabledAsync(FeatureFlagKeys.IsMemoryConfigForIs4Key)
                    .GetAwaiter()
                    .GetResult()
            )
            {
                identityServerBuilder.AddConfigInMemory();
            }
            else
            {
                identityServerBuilder.AddConfigInStore(connectionString);
            }

            return services;
        }
    }
}
