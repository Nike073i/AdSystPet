namespace AdSyst.Advertisments.Application.Categories.Queries.GetCategoryById
{
    public interface IGetCategoryService
    {
        Task<CategoryViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
