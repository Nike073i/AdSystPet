using Microsoft.Extensions.DependencyInjection;
using AdSyst.Orders.BusinessLayer.Interfaces;
using AdSyst.Orders.BusinessLayer.Services;

namespace AdSyst.Orders.BusinessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrdersBusinessLayer(this IServiceCollection services) =>
            services.AddTransient<IOrderService, OrderService>();
    }
}
