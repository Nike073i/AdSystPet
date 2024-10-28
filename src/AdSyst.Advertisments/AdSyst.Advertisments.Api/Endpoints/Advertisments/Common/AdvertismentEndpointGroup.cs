using FastEndpoints;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common
{
    public class AdvertismentCommonEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public AdvertismentCommonEndpointGroup()
        {
            Configure("advertisments", ep => { });
        }
    }
}
