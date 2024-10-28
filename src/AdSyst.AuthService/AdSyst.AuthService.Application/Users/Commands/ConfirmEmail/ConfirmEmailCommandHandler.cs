using ErrorOr;
using MediatR;
using AdSyst.AuthService.Domain;

namespace AdSyst.AuthService.ApplicationLayer.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ErrorOr<Success>>
    {
        private readonly IUserManager _userManager;

        public ConfirmEmailCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(
            ConfirmEmailCommand request,
            CancellationToken cancellationToken
        )
        {
            var getResult = await _userManager.GetByIdAsync(request.UserId, cancellationToken);
            if (getResult.IsError)
                return UserErrors.NotFound;
            var user = getResult.Value;

            var result = await _userManager.ConfirmEmailAsync(
                user,
                request.Token,
                cancellationToken
            );
            return !result.Succeeded
                ? UserErrors.EmailCannotBeConfirmed(
                    result.Errors.ToDictionary(x => x.Code, x => (object)x.Description)
                )
                : Result.Success;
        }
    }
}
