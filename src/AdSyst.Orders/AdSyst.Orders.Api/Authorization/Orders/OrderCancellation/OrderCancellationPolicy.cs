using Microsoft.AspNetCore.Authorization;

namespace AdSyst.Orders.Api.Authorization.Orders.OrderCancellation
{
    public static class OrderCancellationPolicy
    {
        public const string Name = "OrderCancellationPolicy";

        public static IServiceCollection AddOrderCancellationRequirementHandlers(
            this IServiceCollection services
        )
        {
            services.AddTransient<IAuthorizationHandler, AccessToOrderCancellationByBuyerHandler>();
            return services.AddTransient<
                IAuthorizationHandler,
                AccessToOrderCancellationBySellerHandler
            >();
        }

        public static void AddOrderCancellationPolicy(
            this AuthorizationOptions authorizationOptions
        )
        {
            authorizationOptions.AddPolicy(
                Name,
                builder => builder.AddRequirements(new HasAccessToOrderCancellationRequirement())
            );
        }
    }
}
