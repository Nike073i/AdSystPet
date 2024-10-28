using Microsoft.AspNetCore.Authorization;
using AdSyst.Common.Presentation.Extensions;
using AdSyst.Orders.Application.Orders.Queries.GetOrderSystemData;

namespace AdSyst.Orders.Api.Authorization.Orders.OrderDetails
{
    public class AccessToOrderDetailsByBuyerHandler
        : AuthorizationHandler<HasAccessToOrderDetailsRequirement, OrderSystemViewModel>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasAccessToOrderDetailsRequirement requirement,
            OrderSystemViewModel resource
        )
        {
            if (context.User.GetUserId() == resource.BuyerId)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
