using AdSyst.Common.Contracts.Settings;
using AdSyst.Common.DAL.MongoDb.Options;
using AdSyst.Common.Presentation.Options;
using AdSyst.Notifications.Application.Services.Templates;

namespace AdSyst.Notifications.Api.Config
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

        public MongoSettings MongoSettings =>
            _configuration.GetRequiredSection(nameof(MongoSettings)).Get<MongoSettings>()!;

        public JwtAuthOptions JwtAuthOptions =>
            _configuration.GetRequiredSection(nameof(JwtAuthOptions)).Get<JwtAuthOptions>()!;

        public EmailTemplateOptions EmailTemplateOptions
        {
            get
            {
                var config = _configuration
                    .GetRequiredSection(nameof(EmailTemplateOptions))
                    .Get<EmailTemplateOptions>()!;

                config.ContentDirectory = Path.GetFullPath(
                    Path.Combine(Environment.CurrentDirectory, config.ContentDirectory)
                );

                return config;
            }
        }
    }
}
