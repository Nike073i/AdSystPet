using Microsoft.EntityFrameworkCore;
using AdSyst.AuthService.Application.Users.Queries.GetPersonalData;
using AdSyst.AuthService.EfContext.UserData.Contexts;

namespace AdSyst.AuthService.EfContext.UserData.Users.Services
{
    public class GetPersonalDataService : IGetPersonalDataService
    {
        private readonly UserDataReadDbContext _dbContext;

        public GetPersonalDataService(UserDataReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserPersonalData?> GetAsync(
            string userId,
            CancellationToken cancellationToken
        ) =>
            _dbContext
                .Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(
                    u =>
                        new UserPersonalData(
                            u.Id,
                            u.FirstName,
                            u.LastName,
                            u.UserName!,
                            u.Email!,
                            u.Birthday
                        )
                )
                .FirstOrDefaultAsync(cancellationToken);
    }
}
