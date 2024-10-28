using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.Common.Application;

namespace AdSyst.WebFiles.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebFilesApplication(this IServiceCollection services) =>
            services.AddApplicationServices(Assembly.GetExecutingAssembly());
    }
}
