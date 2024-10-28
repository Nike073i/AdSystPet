using FastEndpoints;

namespace AdSyst.Common.Presentation.Endpoints.Shared.Pagination
{
    public class PageRequest
    {
        [QueryParam]
        public int? PageSize { get; set; }

        [QueryParam]
        public int? PageNumber { get; set; }
    }
}
