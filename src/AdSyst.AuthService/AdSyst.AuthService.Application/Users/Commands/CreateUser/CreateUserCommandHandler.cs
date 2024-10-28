using ErrorOr;
using MediatR;
using AdSyst.AuthService.Domain;
using AdSyst.AuthService.Domain.Enums;
using AdSyst.AuthService.Domain.Events;

namespace AdSyst.AuthService.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<string>>
    {
        private readonly IUserManager _userManager;
        private readonly IPublisher _publisher;

        public CreateUserCommandHandler(IUserManager userManager, IPublisher publisher)
        {
            _userManager = userManager;
            _publisher = publisher;
        }

        public async Task<ErrorOr<string>> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken
        )
        {
            AppUser UserProvider()
            {
                return new AppUser(request.FirstName, request.LastName, request.Birthday)
                {
                    UserName = request.Username,
                    Email = request.Email,
                };
            }
            var roles = SelectValidRoles(request.Roles);

            if (!roles.Any())
                return UserErrors.UserWithoutRole;

            var createUserResult = await _userManager.CreateUserAsync(
                UserProvider,
                request.Password,
                cancellationToken
            );
            if (createUserResult.IsError)
                return createUserResult.Errors;

            var newUser = createUserResult.Value;
            await _userManager.AddToRolesAsync(newUser, roles, cancellationToken);

            await _publisher.Publish(
                new UserCreatedDomainEvent(newUser.Id, newUser.Email!),
                cancellationToken
            );

            return newUser.Id;
        }

        private static List<string> SelectValidRoles(Role[] roles) =>
            Enum.GetNames<Role>().Intersect(roles.Select(r => r.ToString())).ToList();
    }
}
