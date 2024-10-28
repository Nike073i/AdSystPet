using ErrorOr;
using MediatR;
using AdSyst.AuthService.Domain;
using AdSyst.Common.Application.Abstractions.Authentication;

namespace AdSyst.AuthService.Application.Users.Queries.GetPersonalData
{
    public class GetPersonalDataQueryHandler
        : IRequestHandler<GetPersonalDataQuery, ErrorOr<UserPersonalData>>
    {
        private readonly IUserContext _userContext;
        private readonly IGetPersonalDataService _service;

        public GetPersonalDataQueryHandler(
            IUserContext userContext,
            IGetPersonalDataService service
        )
        {
            _userContext = userContext;
            _service = service;
        }

        public async Task<ErrorOr<UserPersonalData>> Handle(
            GetPersonalDataQuery request,
            CancellationToken cancellationToken
        )
        {
            var userId = _userContext.UserId;
            if (userId is null)
                return UserErrors.Unauthorized;

            var user = await _service.GetAsync(userId.ToString()!, cancellationToken);
            return user is null ? UserErrors.NotFound : user;
        }
    }
}
