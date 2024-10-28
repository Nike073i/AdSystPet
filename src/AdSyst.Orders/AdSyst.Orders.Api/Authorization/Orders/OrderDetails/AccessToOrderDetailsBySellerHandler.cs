using Microsoft.AspNetCore.Authorization;
using AdSyst.Common.Presentation.Extensions;
using AdSyst.Orders.Application.Orders.Queries.GetOrderSystemData;

namespace AdSyst.Orders.Api.Authorization.Orders.OrderDetails
{
    public class AccessToOrderDetailsBySellerHandler
        : AuthorizationHandler<HasAccessToOrderDetailsRequirement, OrderSystemViewModel>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasAccessToOrderDetailsRequirement requirement,
            OrderSystemViewModel resource
        )
        {
            if (context.User.GetUserId() == resource.SellerId)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
