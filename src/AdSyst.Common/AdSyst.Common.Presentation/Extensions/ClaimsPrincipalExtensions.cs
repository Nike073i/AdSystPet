using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AdSyst.Common.Presentation.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            if (user.Identity?.IsAuthenticated != true)
                return default;
            _ = Guid.TryParse(
                user.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? user.FindFirstValue(JwtRegisteredClaimNames.Sub),
                out var userId
            );
            return userId;
        }
    }
}
