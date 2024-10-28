using ErrorOr;
using MediatR;
using AdSyst.AuthService.Domain.Enums;

namespace AdSyst.AuthService.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        string Username,
        string FirstName,
        string LastName,
        string Password,
        string Email,
        Role[] Roles,
        DateTimeOffset Birthday
    ) : IRequest<ErrorOr<string>>;
}
