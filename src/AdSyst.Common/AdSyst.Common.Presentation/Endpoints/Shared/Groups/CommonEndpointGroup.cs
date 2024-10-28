using FastEndpoints;

namespace AdSyst.Common.Presentation.Endpoints.Shared.Groups
{
    public class CommonEndpointGroup : Group
    {
        public CommonEndpointGroup()
        {
            Configure("api", ep => { });
        }
    }
}
