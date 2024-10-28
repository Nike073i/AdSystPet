using ErrorOr;
using MediatR;

namespace AdSyst.AuthService.Application.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(
        string Username,
        string FirstName,
        string LastName,
        string Password,
        string Email,
        DateTimeOffset Birthday
    ) : IRequest<ErrorOr<string>>;
}
