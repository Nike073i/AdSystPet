using ErrorOr;
using MediatR;

namespace AdSyst.AuthService.Application.Users.Queries.IsEmailFree
{
    public record IsEmailFreeQuery(string Email) : IRequest<ErrorOr<bool>>;
}
