using Microsoft.EntityFrameworkCore;
using AdSyst.AuthService.Application.Users.Queries.IsEmailFree;
using AdSyst.AuthService.EfContext.UserData.Contexts;

namespace AdSyst.AuthService.EfContext.UserData.Users.Services
{
    public class IsEmailFreeService : IIsEmailFreeService
    {
        private readonly UserDataReadDbContext _userDataDbContext;

        public IsEmailFreeService(UserDataReadDbContext userDataDbContext)
        {
            _userDataDbContext = userDataDbContext;
        }

        public Task<bool> CheckAsync(string email, CancellationToken cancellationToken)
        {
            string normalizedEmail = email.Normalize();
            return _userDataDbContext
                .Users
                .AnyAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken);
        }
    }
}
