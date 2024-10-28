using FastEndpoints;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Common
{
    public class CategoryCommonEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public CategoryCommonEndpointGroup()
        {
            Configure("categories", ep => ep.AllowAnonymous());
        }
    }
}
