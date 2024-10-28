using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AdSyst.Common.Application;

namespace AdSyst.Orders.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrderApplication(this IServiceCollection services) =>
            services.AddApplicationServices(Assembly.GetExecutingAssembly());
    }
}
