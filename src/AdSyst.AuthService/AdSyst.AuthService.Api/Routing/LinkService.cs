using AdSyst.AuthService.Api.Endpoints.Users.Common;
using AdSyst.AuthService.Application.Routing;

namespace AdSyst.AuthService.Api.Routing
{
    public class LinkService : ILinkService
    {
        public LinkService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string CreateConfirmLink(string token, string userId)
        {
            var httpContext = _httpContextAccessor.HttpContext!;
            string confirmAddress = _linkGenerator.GetUriByName(
                httpContext,
                ConfirmEmail.EndpointName,
                new RouteValueDictionary { { nameof(userId), userId }, { nameof(token), token } }
            )!;
            return confirmAddress;
        }
    }
}
