using ErrorOr;
using MediatR;
using AdSyst.AuthService.Domain;

namespace AdSyst.AuthService.Application.Users.Queries.GetUserData
{
    public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, ErrorOr<UserDataDto>>
    {
        private readonly IGetUserDataService _service;

        public GetUserDataQueryHandler(IGetUserDataService service)
        {
            _service = service;
        }

        public async Task<ErrorOr<UserDataDto>> Handle(
            GetUserDataQuery request,
            CancellationToken cancellationToken
        )
        {
            string userId = request.UserId;
            var user = await _service.GetAsync(userId, cancellationToken);
            return user is null ? UserErrors.NotFound : user;
        }
    }
}
