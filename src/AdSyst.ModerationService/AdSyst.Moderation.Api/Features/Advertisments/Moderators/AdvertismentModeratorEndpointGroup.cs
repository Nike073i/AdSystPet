using FastEndpoints;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;

namespace AdSyst.Moderation.Api.Features.Advertisments.Moderators
{
    public class AdvertismentModeratorEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public AdvertismentModeratorEndpointGroup()
        {
            Configure(
                "moderator/advertisments",
                d =>
                {
                    d.DontAutoTag();
                    d.Roles(RoleNames.Moderator);
                    d.Options(b => b.WithTags("Moderator/Advertisments"));
                }
            );
        }
    }
}
