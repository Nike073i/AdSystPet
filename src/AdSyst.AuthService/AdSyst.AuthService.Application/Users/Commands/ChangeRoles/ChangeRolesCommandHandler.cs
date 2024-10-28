using ErrorOr;
using MediatR;
using AdSyst.AuthService.Domain;
using AdSyst.AuthService.Domain.Enums;

namespace AdSyst.AuthService.ApplicationLayer.Users.Commands.ChangeRoles
{
    public class ChangeRolesCommandHandler : IRequestHandler<ChangeRolesCommand, ErrorOr<Success>>
    {
        private readonly IUserManager _userManager;

        public ChangeRolesCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(
            ChangeRolesCommand request,
            CancellationToken cancellationToken
        )
        {
            var getResult = await _userManager.GetByIdAsync(request.UserId, cancellationToken);
            if (getResult.IsError)
                return getResult.Errors;
            var user = getResult.Value;

            var currentRoles = await _userManager.GetRolesAsync(user, cancellationToken);
            var newRoles = SelectValidRoles(request.Roles);

            if (!newRoles.Any())
                return UserErrors.UserWithoutRole;

            var rolesForRemove = currentRoles.Except(newRoles);
            var rolesForAdd = newRoles.Except(currentRoles);

            if (rolesForAdd.Any())
                await _userManager.AddToRolesAsync(user, rolesForAdd, cancellationToken);

            if (rolesForRemove.Any())
                await _userManager.RemoveFromRolesAsync(user, rolesForRemove, cancellationToken);

            return Result.Success;
        }

        private static List<string> SelectValidRoles(Role[] roles) =>
            Enum.GetNames<Role>().Intersect(roles.Select(r => r.ToString())).ToList();
    }
}
