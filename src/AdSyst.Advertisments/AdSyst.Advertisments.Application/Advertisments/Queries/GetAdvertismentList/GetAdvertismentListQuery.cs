using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.Application.Pagination;
using AdSyst.Common.BusinessLayer.Enums;
using AdSyst.Common.BusinessLayer.Options;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList
{
    public record GetAdvertismentListQuery(
        AdvertismentSortOptions SortOptions,
        PaginationOptions PaginationOptions,
        AdvertismentFilterDto? FilterOptions = null
    ) : IRequest<ErrorOr<PageResult<AdvertismentViewModel>>>
    {
        public static GetAdvertismentListQuery CreateQuery(
            SortAdvertismentField? sortField = null,
            SortDirection? sortDirection = null,
            int? pageSize = null,
            int? pageNumber = null,
            Guid? userId = null,
            Guid? categoryId = null,
            string? search = null,
            DateTimeOffset? periodEnd = null,
            DateTimeOffset? periodStart = null,
            AdvertismentStatus? advertismentStatus = null
        )
        {
            var sortOptions = new AdvertismentSortOptions(sortField, sortDirection);
            var paginationOptions = new PaginationOptions(pageSize, pageNumber);
            AdvertismentFilterDto? filterDto = null;
            if (
                userId.HasValue
                || categoryId.HasValue
                || !string.IsNullOrEmpty(search)
                || periodEnd.HasValue
                || periodStart.HasValue
                || advertismentStatus.HasValue
            )
            {
                filterDto = new(
                    search,
                    categoryId,
                    periodStart,
                    periodEnd,
                    userId,
                    advertismentStatus
                );
            }

            return new GetAdvertismentListQuery(
                sortOptions,
                paginationOptions,
                FilterOptions: filterDto
            );
        }
    }
}
