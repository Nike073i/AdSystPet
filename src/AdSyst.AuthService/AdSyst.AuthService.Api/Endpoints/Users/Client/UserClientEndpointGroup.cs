using FastEndpoints;
using AdSyst.AuthService.Domain.Enums;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;
using static IdentityServer4.IdentityServerConstants;

namespace AdSyst.AuthService.Api.Endpoints.Users.Client
{
    public class UserClientEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public UserClientEndpointGroup()
        {
            Configure(
                "users",
                ep =>
                {
                    ep.Policies(LocalApi.PolicyName);
                    ep.Roles(nameof(Role.Client));
                }
            );
        }
    }
}
