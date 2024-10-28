using Microsoft.AspNetCore.Authorization;

namespace AdSyst.Orders.Api.Authorization.Orders.OrderDetails
{
    public static class CanReadOrderDetailsPolicy
    {
        public const string Name = "CanReadOrderDetails";

        public static IServiceCollection AddReadOrderRequirementHandlers(
            this IServiceCollection services
        )
        {
            services.AddTransient<IAuthorizationHandler, AccessToOrderDetailsByBuyerHandler>();
            return services.AddTransient<
                IAuthorizationHandler,
                AccessToOrderDetailsBySellerHandler
            >();
        }

        public static void AddReadOrderPolicy(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy(
                Name,
                builder => builder.AddRequirements(new HasAccessToOrderDetailsRequirement())
            );
        }
    }
}
