using Microsoft.EntityFrameworkCore;
using AdSyst.AuthService.Application.Users.Queries.GetUserInfoDetails;
using AdSyst.AuthService.EfContext.UserData.Contexts;

namespace AdSyst.AuthService.EfContext.UserData.Users.Services
{
    public class GetUserInfoDetailsService : IGetUserInfoDetailsService
    {
        private readonly UserDataReadDbContext _dbContext;

        public GetUserInfoDetailsService(UserDataReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserInfoDetailDto?> GetAsync(
            string userId,
            CancellationToken cancellationToken
        ) =>
            _dbContext
                .Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(
                    u =>
                        new UserInfoDetailDto(
                            u.Id,
                            u.Email!,
                            u.FirstName,
                            u.LastName,
                            u.UserName!,
                            u.IsActive,
                            u.UserRoles!.Select(r => r.Role.Name).ToArray(),
                            u.EmailConfirmed,
                            u.Birthday
                        )
                )
                .FirstOrDefaultAsync(cancellationToken);
    }
}
