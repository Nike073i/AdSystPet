using ErrorOr;
using MediatR;

namespace AdSyst.AuthService.Application.Users.Queries.IsEmailFree
{
    public class IsEmailFreeHandler : IRequestHandler<IsEmailFreeQuery, ErrorOr<bool>>
    {
        private readonly IIsEmailFreeService _service;

        public IsEmailFreeHandler(IIsEmailFreeService service)
        {
            _service = service;
        }

        public async Task<ErrorOr<bool>> Handle(
            IsEmailFreeQuery request,
            CancellationToken cancellationToken
        )
        {
            bool isExists = await _service.CheckAsync(request.Email, cancellationToken);
            return !isExists;
        }
    }
}
