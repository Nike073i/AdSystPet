using AdSyst.Common.BusinessLayer.Options;

namespace AdSyst.AuthService.Application.Users.Queries.GetUserInfoList
{
    public interface IGetUserInfoListService
    {
        Task<IReadOnlyCollection<UserInfoDto>> GetAsync(
            UserSortOptions sortOptions,
            PaginationOptions paginationOptions,
            CancellationToken cancellationToken = default
        );
    }
}
