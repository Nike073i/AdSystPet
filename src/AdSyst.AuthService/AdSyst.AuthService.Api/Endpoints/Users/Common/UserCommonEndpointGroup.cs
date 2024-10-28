using FastEndpoints;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;

namespace AdSyst.AuthService.Api.Endpoints.Users.Common
{
    public class UserCommonEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public UserCommonEndpointGroup()
        {
            Configure("users", ep => ep.AllowAnonymous());
        }
    }
}
