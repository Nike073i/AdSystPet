using Microsoft.AspNetCore.Authorization;

namespace AdSyst.Orders.Api.Authorization.Orders.OrderMoving
{
    public static class CanMoveOrderPolicy
    {
        public const string Name = "OrderMovingPolicy";

        public static IServiceCollection AddOrderMovingRequirementHandlers(
            this IServiceCollection services
        ) => services.AddTransient<IAuthorizationHandler, AccessToOrderMovingBySellerHandler>();

        public static void AddOrderMovingPolicy(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy(
                Name,
                builder => builder.AddRequirements(new HasAccessToOrderMovingRequirement())
            );
        }
    }
}
