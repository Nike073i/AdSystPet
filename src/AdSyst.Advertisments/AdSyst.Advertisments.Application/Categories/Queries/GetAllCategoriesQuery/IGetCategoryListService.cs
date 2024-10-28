namespace AdSyst.Advertisments.Application.Categories.Queries.GetAllCategoriesQuery
{
    public interface IGetCategoryListService
    {
        Task<IReadOnlyList<CategoryViewModel>> GetAsync(
            CancellationToken cancellationToken = default
        );
    }
}
