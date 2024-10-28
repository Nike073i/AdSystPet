using FastEndpoints;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;

namespace AdSyst.Moderation.Api.Features.CorrectionNotes.Moderators
{
    public class CorrectionNoteModeratorEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public CorrectionNoteModeratorEndpointGroup()
        {
            Configure(
                "moderator/notes",
                d =>
                {
                    d.DontAutoTag();
                    d.Roles(RoleNames.Moderator);
                    d.Options(b => b.WithTags("Moderator/CorrectionNotes"));
                }
            );
        }
    }
}
