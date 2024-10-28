using Microsoft.Extensions.Configuration;
using AdSyst.Advertisments.Infrastructure.Data.Settings;
using AdSyst.Advertisments.Infrastructure.Messaging.Settings;
using AdSyst.Common.Contracts.Settings;
using AdSyst.Common.Infrastructure.Caching;
using AdSyst.Common.Presentation.Options;

namespace AdSyst.Advertisments.Infrastructure.Configurations
{
    internal class ConfigurationHelper
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

        public JwtAuthOptions JwtAuthOptions =>
            _configuration.GetRequiredSection(nameof(JwtAuthOptions)).Get<JwtAuthOptions>()!;

        public EventProcessOptions EventMessageProcessOptions =>
            _configuration.GetRequiredSection("OutboxOptions").Get<EventProcessOptions>()!;

        public RedisSettings RedisSettings =>
            _configuration.GetRequiredSection(nameof(RedisSettings)).Get<RedisSettings>()!;
    }
}
