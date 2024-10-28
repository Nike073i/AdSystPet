using FastEndpoints;
using AdSyst.AuthService.Domain.Enums;
using AdSyst.Common.Presentation.Endpoints.Shared.Groups;
using static IdentityServer4.IdentityServerConstants;

namespace AdSyst.AuthService.Api.Endpoints.Users.System
{
    public class UserSystemEndpointGroup : SubGroup<CommonEndpointGroup>
    {
        public UserSystemEndpointGroup()
        {
            Configure(
                "system/users",
                ep =>
                {
                    ep.Policies(LocalApi.PolicyName);
                    ep.Roles(nameof(Role.System));
                }
            );
        }
    }
}
