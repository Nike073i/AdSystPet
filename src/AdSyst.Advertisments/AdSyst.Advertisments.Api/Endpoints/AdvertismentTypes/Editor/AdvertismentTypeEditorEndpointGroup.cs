using FastEndpoints;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;

namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Editor
{
    public class AdvertismentTypeEditorEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public AdvertismentTypeEditorEndpointGroup()
        {
            Configure(
                "editor/advertismentTypes",
                ep =>
                {
                    ep.DontAutoTag();
                    ep.Options(b => b.WithTags("Editor/Types"));
                    ep.Roles(RoleNames.Editor);
                }
            );
        }
    }
}
