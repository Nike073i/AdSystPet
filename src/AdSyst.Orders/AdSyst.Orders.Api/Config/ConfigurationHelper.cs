using AdSyst.Common.Contracts.Settings;
using AdSyst.Common.DAL.MongoDb.Options;
using AdSyst.Common.Presentation.Options;
using AdSyst.Orders.Api.Config.Consts;
using AdSyst.Orders.SyncDataServices.Options;

namespace AdSyst.Orders.Api.Config
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

        public GrpcClientSettings AdvertismentsGrpcClientSettings =>
            _configuration
                .GetRequiredSection(ConfigurationKeys.AdvertismentsServiceGrpcClientSectionKey)
                .Get<GrpcClientSettings>()!;
    }
}
