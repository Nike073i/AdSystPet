using AdSyst.Orders.Api.Authorization.Orders.OrderCancellation;
using AdSyst.Orders.Api.Authorization.Orders.OrderCreation;
using AdSyst.Orders.Api.Authorization.Orders.OrderDetails;
using AdSyst.Orders.Api.Authorization.Orders.OrderMoving;

namespace AdSyst.Orders.Api.Config.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
        {
            services.AddOrderCancellationRequirementHandlers();
            services.AddOrderCreationRequirementHandlers();
            services.AddOrderMovingRequirementHandlers();
            services.AddReadOrderRequirementHandlers();
            services.AddAuthorization(configure =>
            {
                configure.AddOrderCancellationPolicy();
                configure.AddOrderCreationPolicy();
                configure.AddOrderMovingPolicy();
                configure.AddReadOrderPolicy();
            });
            return services;
        }
    }
}
