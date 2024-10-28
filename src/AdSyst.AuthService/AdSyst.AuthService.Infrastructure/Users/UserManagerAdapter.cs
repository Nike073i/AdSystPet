using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AdSyst.AuthService.Domain;

namespace AdSyst.AuthService.EfContext.UserData.Users
{
    public class UserManagerAdapter : IUserManager
    {
        private readonly UserManager<AppUser> _adaptee;

        public UserManagerAdapter(UserManager<AppUser> adaptee)
        {
            _adaptee = adaptee;
        }

        public Task<IdentityResult> AddToRoleAsync(
            AppUser user,
            string role,
            CancellationToken cancellationToken
        ) => _adaptee.AddToRoleAsync(user, role);

        public Task<IdentityResult> AddToRolesAsync(
            AppUser user,
            IEnumerable<string> roles,
            CancellationToken cancellationToken
        ) => _adaptee.AddToRolesAsync(user, roles);

        public Task<IdentityResult> ConfirmEmailAsync(
            AppUser user,
            string token,
            CancellationToken cancellationToken
        ) => _adaptee.ConfirmEmailAsync(user, token);

        public async Task<ErrorOr<AppUser>> CreateUserAsync(
            Func<AppUser> userProvider,
            string password,
            CancellationToken cancellationToken
        )
        {
            var user = userProvider();

            bool userExists = await IsUserExists(user.UserName!, user.Email!, cancellationToken);

            if (userExists)
                return UserErrors.UserAlreadyExists;

            var result = await _adaptee.CreateAsync(user, password);

            return !result.Succeeded
                ? UserErrors.UserCannotBeCreated(
                    result.Errors.ToDictionary(x => x.Code, e => (object)e.Description)
                )
                : user;
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(
            AppUser user,
            CancellationToken cancellationToken
        ) => _adaptee.GenerateEmailConfirmationTokenAsync(user);

        public async Task<ErrorOr<AppUser>> GetByIdAsync(
            string userId,
            CancellationToken cancellationToken
        )
        {
            var user = await _adaptee.FindByIdAsync(userId);
            return user is null ? UserErrors.NotFound : user;
        }

        public Task<IList<string>> GetRolesAsync(
            AppUser user,
            CancellationToken cancellationToken
        ) => _adaptee.GetRolesAsync(user);

        public Task<bool> IsUserExists(
            string userName,
            string email,
            CancellationToken cancellationToken
        ) =>
            _adaptee
                .Users
                .AnyAsync(
                    u =>
                        u.NormalizedUserName == userName.Normalize()
                        || u.NormalizedEmail == email.Normalize(),
                    cancellationToken
                );

        public Task<IdentityResult> RemoveFromRolesAsync(
            AppUser user,
            IEnumerable<string> roles,
            CancellationToken cancellationToken
        ) => _adaptee.RemoveFromRolesAsync(user, roles);
    }
}
