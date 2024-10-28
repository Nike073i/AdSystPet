using ErrorOr;
using MediatR;
using AdSyst.Common.Application.Pagination;

namespace AdSyst.AuthService.Application.Users.Queries.GetUserInfoList
{
    public class GetUserInfoListQueryHandler
        : IRequestHandler<GetUserInfoListQuery, ErrorOr<PageResult<UserInfoDto>>>
    {
        private readonly IGetUserInfoListService _service;

        public GetUserInfoListQueryHandler(IGetUserInfoListService service)
        {
            _service = service;
        }

        public async Task<ErrorOr<PageResult<UserInfoDto>>> Handle(
            GetUserInfoListQuery request,
            CancellationToken cancellationToken
        )
        {
            (var sortOptions, var paginationOptions) = request;
            var data = await _service.GetAsync(sortOptions, paginationOptions, cancellationToken);
            return new PageResult<UserInfoDto>(paginationOptions.PageNumber, data);
        }
    }
}
