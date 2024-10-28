using ErrorOr;
using MediatR;

namespace AdSyst.AuthService.ApplicationLayer.Users.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(string UserId, string Token) : IRequest<ErrorOr<Success>>;
}
