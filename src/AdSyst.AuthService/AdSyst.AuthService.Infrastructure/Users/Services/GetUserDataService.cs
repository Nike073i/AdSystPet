using Microsoft.EntityFrameworkCore;
using AdSyst.AuthService.Application.Users.Queries.GetUserData;
using AdSyst.AuthService.EfContext.UserData.Contexts;

namespace AdSyst.AuthService.EfContext.UserData.Users.Services
{
    public class GetUserDataService : IGetUserDataService
    {
        private readonly UserDataReadDbContext _dbContext;

        public GetUserDataService(UserDataReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserDataDto?> GetAsync(string userId, CancellationToken cancellationToken) =>
            _dbContext
                .Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new UserDataDto(u.FirstName, u.LastName, u.Email!))
                .FirstOrDefaultAsync(cancellationToken);
    }
}
