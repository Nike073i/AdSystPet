namespace AdSyst.Advertisments.Domain.Categories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(
            Guid id,
            bool includeRelationships = false,
            CancellationToken cancellationToken = default
        );
        void Add(Category category);
        void Remove(Category category);
        Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
