using ErrorOr;
using MediatR;

namespace AdSyst.AuthService.Application.Users.Queries.IsUsernameFree
{
    public class IsUsernameFreeHandler : IRequestHandler<IsUsernameFreeQuery, ErrorOr<bool>>
    {
        private readonly IIsUsernameFreeService _service;

        public IsUsernameFreeHandler(IIsUsernameFreeService service)
        {
            _service = service;
        }

        public async Task<ErrorOr<bool>> Handle(
            IsUsernameFreeQuery request,
            CancellationToken cancellationToken
        )
        {
            bool isExists = await _service.CheckAsync(request.UserName, cancellationToken);
            return !isExists;
        }
    }
}
