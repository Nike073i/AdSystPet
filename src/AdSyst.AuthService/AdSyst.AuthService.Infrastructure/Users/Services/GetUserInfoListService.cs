using Microsoft.EntityFrameworkCore;
using AdSyst.AuthService.Application.Users.Queries.GetUserInfoList;
using AdSyst.AuthService.EfContext.UserData.Contexts;
using AdSyst.AuthService.EfContext.UserData.Extensions.Sort;
using AdSyst.Common.BusinessLayer.Extensions;
using AdSyst.Common.BusinessLayer.Options;

namespace AdSyst.AuthService.EfContext.UserData.Users.Services
{
    public class GetUserInfoListService : IGetUserInfoListService
    {
        private readonly UserDataReadDbContext _dbContext;

        public GetUserInfoListService(UserDataReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<UserInfoDto>> GetAsync(
            UserSortOptions sortOptions,
            PaginationOptions paginationOptions,
            CancellationToken cancellationToken
        ) =>
            await _dbContext
                .Users
                .Sort(sortOptions)
                .Page(paginationOptions)
                .Select(
                    user =>
                        new UserInfoDto(
                            user.Id,
                            user.Email!,
                            user.FirstName,
                            user.LastName,
                            user.IsActive
                        )
                )
                .ToListAsync(cancellationToken);
    }
}
