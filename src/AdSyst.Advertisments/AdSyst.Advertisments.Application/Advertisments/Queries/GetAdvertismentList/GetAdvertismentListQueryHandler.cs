using ErrorOr;
using MediatR;
using AdSyst.Common.Application.Pagination;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList
{
    internal class GetAdvertismentListQueryHandler
        : IRequestHandler<GetAdvertismentListQuery, ErrorOr<PageResult<AdvertismentViewModel>>>
    {
        private readonly IGetAdvertismentListService _service;

        public GetAdvertismentListQueryHandler(IGetAdvertismentListService service)
        {
            _service = service;
        }

        public async Task<ErrorOr<PageResult<AdvertismentViewModel>>> Handle(
            GetAdvertismentListQuery request,
            CancellationToken cancellationToken
        )
        {
            (var sortOptions, var paginationOptions, var filterOptions) = request;

            var data = await _service.GetAsync(
                filterOptions,
                sortOptions,
                paginationOptions,
                cancellationToken
            );
            return new PageResult<AdvertismentViewModel>(paginationOptions.PageNumber, data);
        }
    }
}
