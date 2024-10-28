namespace AdSyst.Common.Application.Pagination
{
    public record PageResult<T>(int PageNumber, IEnumerable<T> Items);
}
