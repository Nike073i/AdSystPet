using Microsoft.AspNetCore.Authorization;
using AdSyst.Common.Presentation.Extensions;
using AdSyst.Orders.Application.Orders.Queries.GetOrderSystemData;

namespace AdSyst.Orders.Api.Authorization.Orders.OrderCancellation
{
    public class AccessToOrderCancellationBySellerHandler
        : AuthorizationHandler<HasAccessToOrderCancellationRequirement, OrderSystemViewModel>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasAccessToOrderCancellationRequirement requirement,
            OrderSystemViewModel resource
        )
        {
            if (context.User.GetUserId() == resource.SellerId)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
