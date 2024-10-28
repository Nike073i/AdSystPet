using FastEndpoints;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Common
{
    public class CategoryEditorEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public CategoryEditorEndpointGroup()
        {
            Configure(
                "editor/categories",
                ep =>
                {
                    ep.DontAutoTag();
                    ep.Options(b => b.WithTags("Editor/Categories"));
                    ep.Roles(RoleNames.Editor);
                }
            );
        }
    }
}
