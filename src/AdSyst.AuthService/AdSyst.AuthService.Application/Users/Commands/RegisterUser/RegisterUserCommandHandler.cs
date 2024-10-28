using ErrorOr;
using MediatR;
using AdSyst.AuthService.Domain;
using AdSyst.AuthService.Domain.Enums;
using AdSyst.AuthService.Domain.Events;

namespace AdSyst.AuthService.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<string>>
    {
        private readonly IUserManager _userManager;
        private readonly IPublisher _publisher;

        public RegisterUserCommandHandler(IUserManager userManager, IPublisher publisher)
        {
            _userManager = userManager;
            _publisher = publisher;
        }

        public async Task<ErrorOr<string>> Handle(
            RegisterUserCommand request,
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

            var createUserResult = await _userManager.CreateUserAsync(
                UserProvider,
                request.Password,
                cancellationToken
            );
            if (createUserResult.IsError)
                return createUserResult.Errors;
            var newUser = createUserResult.Value;

            await _userManager.AddToRoleAsync(newUser, nameof(Role.Client), cancellationToken);

            await _publisher.Publish(
                new UserCreatedDomainEvent(newUser.Id, newUser.Email!),
                cancellationToken
            );

            return newUser.Id;
        }
    }
}
