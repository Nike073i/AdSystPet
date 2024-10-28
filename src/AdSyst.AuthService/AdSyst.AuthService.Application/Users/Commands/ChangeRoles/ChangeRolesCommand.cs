using ErrorOr;
using MediatR;
using AdSyst.AuthService.Domain.Enums;

namespace AdSyst.AuthService.ApplicationLayer.Users.Commands.ChangeRoles
{
    public record ChangeRolesCommand(string UserId, Role[] Roles) : IRequest<ErrorOr<Success>>;
}
