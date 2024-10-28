using AdSyst.Common.BusinessLayer.Options;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList
{
    public interface IGetAdvertismentListService
    {
        Task<IReadOnlyList<AdvertismentViewModel>> GetAsync(
            AdvertismentFilterDto? filterOptions,
            AdvertismentSortOptions sortOptions,
            PaginationOptions paginationOptions,
            CancellationToken cancellationToken = default
        );
    }
}
