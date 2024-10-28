using ErrorOr;
using MediatR;
using AdSyst.Common.Application.Pagination;
using AdSyst.Common.BusinessLayer.Enums;
using AdSyst.Common.BusinessLayer.Options;

namespace AdSyst.AuthService.Application.Users.Queries.GetUserInfoList
{
    public record GetUserInfoListQuery(
        UserSortOptions SortOptions,
        PaginationOptions PaginationOptions
    ) : IRequest<ErrorOr<PageResult<UserInfoDto>>>
    {
        public static GetUserInfoListQuery CreateQuery(
            SortUserField? sortField = null,
            SortDirection? sortDirection = null,
            int? pageSize = null,
            int? pageNumber = null
        )
        {
            var sortOptions = new UserSortOptions(sortField, sortDirection);
            var paginationOptions = new PaginationOptions(pageSize, pageNumber);
            return new(sortOptions, paginationOptions);
        }
    }
}
