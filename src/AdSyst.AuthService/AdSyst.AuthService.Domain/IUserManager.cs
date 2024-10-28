using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace AdSyst.AuthService.Domain
{
    public interface IUserManager
    {
        Task<bool> IsUserExists(
            string userName,
            string email,
            CancellationToken cancellationToken = default
        );
        Task<ErrorOr<AppUser>> CreateUserAsync(
            Func<AppUser> userProvider,
            string password,
            CancellationToken cancellationToken = default
        );

        Task<ErrorOr<AppUser>> GetByIdAsync(
            string userId,
            CancellationToken cancellationToken = default
        );

        Task<IList<string>> GetRolesAsync(
            AppUser user,
            CancellationToken cancellationToken = default
        );

        Task<IdentityResult> AddToRolesAsync(
            AppUser user,
            IEnumerable<string> roles,
            CancellationToken cancellationToken = default
        );

        Task<IdentityResult> RemoveFromRolesAsync(
            AppUser user,
            IEnumerable<string> roles,
            CancellationToken cancellationToken = default
        );

        Task<IdentityResult> ConfirmEmailAsync(
            AppUser user,
            string token,
            CancellationToken cancellationToken = default
        );

        Task<IdentityResult> AddToRoleAsync(
            AppUser user,
            string role,
            CancellationToken cancellationToken = default
        );

        Task<string> GenerateEmailConfirmationTokenAsync(
            AppUser user,
            CancellationToken cancellationToken = default
        );
    }
}
