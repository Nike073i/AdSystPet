using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList;
using AdSyst.Common.BusinessLayer.Enums;
using AdSyst.Common.Presentation.Endpoints.Shared.Pagination;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.GetByFilter
{
    public class GetAdvertismentsByFilterRequest : PageRequest
    {
        public string? Search { get; set; }

        public Guid? CategoryId { get; set; }

        public DateTimeOffset? PeriodStart { get; set; }

        public DateTimeOffset? PeriodEnd { get; set; }

        public SortDirection? SortDirection { get; set; }

        public SortAdvertismentField? SortField { get; set; }
    }
}
