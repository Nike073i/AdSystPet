using Microsoft.Extensions.Configuration;

namespace AdSyst.MigrationsTool
{
    public class BaseDesignDbContextFactory
    {
        protected IConfiguration Configuration { get; }

        public BaseDesignDbContextFactory()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
    }
}
