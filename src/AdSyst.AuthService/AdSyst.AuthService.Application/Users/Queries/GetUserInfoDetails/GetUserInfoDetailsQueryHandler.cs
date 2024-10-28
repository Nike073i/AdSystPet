using ErrorOr;
using MediatR;
using AdSyst.AuthService.Domain;

namespace AdSyst.AuthService.Application.Users.Queries.GetUserInfoDetails
{
    public class GetUserInfoDetailsQueryHandler
        : IRequestHandler<GetUserInfoDetailsQuery, ErrorOr<UserInfoDetailDto>>
    {
        private readonly IGetUserInfoDetailsService _service;

        public GetUserInfoDetailsQueryHandler(IGetUserInfoDetailsService service)
        {
            _service = service;
        }

        public async Task<ErrorOr<UserInfoDetailDto>> Handle(
            GetUserInfoDetailsQuery request,
            CancellationToken cancellationToken
        )
        {
            string userId = request.UserId;
            var user = await _service.GetAsync(userId, cancellationToken);
            return user is null ? UserErrors.NotFound : user;
        }
    }
}
