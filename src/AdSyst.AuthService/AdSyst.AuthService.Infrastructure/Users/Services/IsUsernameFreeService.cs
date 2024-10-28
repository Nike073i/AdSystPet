using Microsoft.EntityFrameworkCore;
using AdSyst.AuthService.Application.Users.Queries.IsUsernameFree;
using AdSyst.AuthService.EfContext.UserData.Contexts;

namespace AdSyst.AuthService.EfContext.UserData.Users.Services
{
    public class IsUsernameFreeService : IIsUsernameFreeService
    {
        private readonly UserDataReadDbContext _userDataDbContext;

        public IsUsernameFreeService(UserDataReadDbContext userDataDbContext)
        {
            _userDataDbContext = userDataDbContext;
        }

        public Task<bool> CheckAsync(string userName, CancellationToken cancellationToken)
        {
            string normalizedUserName = userName.Normalize();
            return _userDataDbContext
                .Users
                .AnyAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
        }
    }
}
