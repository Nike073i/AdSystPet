using FastEndpoints;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;

namespace AdSyst.Moderation.Api.Features.Advertisments.Clients
{
    public class CorrectionNoteClientEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public CorrectionNoteClientEndpointGroup()
        {
            Configure(
                "users/advertisments",
                d =>
                {
                    d.DontAutoTag();
                    d.Roles(RoleNames.Client);
                    d.Options(b => b.WithTags("Client/CorrectionNotes"));
                }
            );
        }
    }
}
