using Microsoft.AspNetCore.Authorization;
using AdSyst.Common.Presentation.Extensions;
using AdSyst.Orders.SyncDataServices.Advertisments.Models;

namespace AdSyst.Orders.Api.Authorization.Orders.OrderCreation
{
    public class AccessToOrderCreationBySomeoneElseHandler
        : AuthorizationHandler<HasAccessToOrderCreationRequirement, AdvertismentSystemDto>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasAccessToOrderCreationRequirement requirement,
            AdvertismentSystemDto resource
        )
        {
            if (context.User.GetUserId() != resource.UserId)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
