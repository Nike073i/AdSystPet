using AdSyst.Common.DAL.MongoDb.Options;
using AdSyst.Common.Presentation.Options;
using AdSyst.WebFiles.BusinessLayer.Settings;

namespace AdSyst.WebFiles.Api.Config
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _configuration;

        public ConfigurationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MongoSettings MongoSettings =>
            _configuration.GetRequiredSection(nameof(MongoSettings)).Get<MongoSettings>()!;

        public JwtAuthOptions JwtAuthOptions =>
            _configuration.GetRequiredSection(nameof(JwtAuthOptions)).Get<JwtAuthOptions>()!;

        public FileStorageSettings FileStorageSettings =>
            _configuration.GetSection(nameof(FileStorageSettings)).Get<FileStorageSettings>()
            ?? new();

        public ImageCompressSettings ImageCompressSettings =>
            _configuration.GetSection(nameof(ImageCompressSettings)).Get<ImageCompressSettings>()
            ?? new();
    }
}
