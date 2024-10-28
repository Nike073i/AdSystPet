using ErrorOr;
using MediatR;

namespace AdSyst.AuthService.Application.Users.Queries.IsUsernameFree
{
    public record IsUsernameFreeQuery(string UserName) : IRequest<ErrorOr<bool>>;
}
