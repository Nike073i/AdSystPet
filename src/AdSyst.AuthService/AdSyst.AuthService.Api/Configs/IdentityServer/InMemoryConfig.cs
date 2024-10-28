using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace AdSyst.AuthService.Api.Configs.IdentityServer
{
    public static class InMemoryConfig
    {
        private static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> { new("webApiScope", "Web API Scope"), new("IdentityServerApi") };

        private static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new(
                    "webApiResource",
                    "Web API Resource",
                    new[] { JwtClaimTypes.Email, JwtClaimTypes.Role }
                )
                {
                    Scopes = { "webApiScope", "IdentityServerApi" }
                }
            };

        private static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        private static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new()
                {
                    ClientId = "web-api-client",
                    ClientName = "Web API Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "webApiScope",
                        "IdentityServerApi"
                    }
                }
            };

        public static IIdentityServerBuilder AddConfigInMemory(this IIdentityServerBuilder builder)
        {
            builder
                .AddInMemoryApiScopes(ApiScopes)
                .AddInMemoryApiResources(ApiResources)
                .AddInMemoryIdentityResources(IdentityResources)
                .AddInMemoryClients(Clients);

            return builder;
        }
    }
}
