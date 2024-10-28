using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AdSyst.Common.Presentation.Data
{
    public static class WebApplicationExtensions
    {
        public static bool InitializeData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var provider = scope.ServiceProvider;

            var dbInitializer = provider.GetService<IDbInitializer>();

            return dbInitializer?.Initialize() ?? true;
        }
    }
}
