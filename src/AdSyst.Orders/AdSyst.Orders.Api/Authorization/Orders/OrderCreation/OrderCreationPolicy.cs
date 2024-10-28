using Microsoft.AspNetCore.Authorization;

namespace AdSyst.Orders.Api.Authorization.Orders.OrderCreation
{
    public static class OrderCreationPolicy
    {
        public const string Name = "OrderCreationPolicy";

        public static IServiceCollection AddOrderCreationRequirementHandlers(
            this IServiceCollection services
        ) =>
            services.AddTransient<
                IAuthorizationHandler,
                AccessToOrderCreationBySomeoneElseHandler
            >();

        public static void AddOrderCreationPolicy(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy(
                Name,
                builder => builder.AddRequirements(new HasAccessToOrderCreationRequirement())
            );
        }
    }
}
