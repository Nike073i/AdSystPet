using FastEndpoints;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Common
{
    public class AdvertismentModeratorEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public AdvertismentModeratorEndpointGroup()
        {
            Configure(
                "moderator/advertisments",
                ep =>
                {
                    ep.DontAutoTag();
                    ep.Options(b => b.WithTags("Moderator/Advertisments"));
                    ep.Roles(RoleNames.Moderator);
                }
            );
        }
    }
}
