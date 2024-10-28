using AdSyst.AuthService.Api.Configs.Consts;
using AdSyst.AuthService.Api.Configs.IdentityServer;
using AdSyst.AuthService.SqlServerMigrations.Configuration;
using AdSyst.Common.BusinessLayer.Options;
using AdSyst.Common.Contracts.Settings;

namespace AdSyst.AuthService.Api.Configs
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _configuration;

        public ConfigurationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RabbitMqSettings RabbitMqSettings =>
            _configuration.GetRequiredSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>()!;

        public SqlServerConnectionSettings SqlServerConnectionSettings =>
            _configuration
                .GetRequiredSection(nameof(SqlServerConnectionSettings))
                .Get<SqlServerConnectionSettings>()!;

        public MessageTemplate ConfirmEmailTemplate =>
            _configuration
                .GetRequiredSection(ConfigurationKeys.ConfirmEmailMessageTemplateSectionKey)
                .Get<MessageTemplate>()!;

        public IdentityServerConfigs IdentityServerConfigs =>
            _configuration
                .GetRequiredSection(nameof(IdentityServerConfigs))
                .Get<IdentityServerConfigs>()!;
    }
}
