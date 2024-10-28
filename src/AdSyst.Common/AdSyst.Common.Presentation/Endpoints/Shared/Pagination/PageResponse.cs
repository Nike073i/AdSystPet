namespace AdSyst.Common.Presentation.Endpoints.Shared.Pagination
{
    public record PageResponse<T>(int PageNumber, IEnumerable<T> Items)
    {
        public int PageSize => Items.Count();
    }
}
