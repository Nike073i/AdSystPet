using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.Common.Application;

namespace AdSyst.Advertisments.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) =>
            services.AddApplicationServices(Assembly.GetExecutingAssembly());
    }
}
