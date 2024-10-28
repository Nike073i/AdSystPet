using FastEndpoints;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Common
{
    public class AdvertismentTypeCommonEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public AdvertismentTypeCommonEndpointGroup()
        {
            Configure("types", ep => ep.AllowAnonymous());
        }
    }
}
