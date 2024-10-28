using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using AdSyst.Common.Application.Abstractions.Authentication;

namespace AdSyst.Common.Infrastructure.Authentication
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext is null)
                    throw new InvalidOperationException();

                var principal = _httpContextAccessor.HttpContext.User;
                if (principal.Identity?.IsAuthenticated != true)
                    return null;

                _ = Guid.TryParse(
                    principal.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? principal.FindFirstValue(JwtRegisteredClaimNames.Sub),
                    out var userId
                );
                return userId;
            }
        }
    }
}
