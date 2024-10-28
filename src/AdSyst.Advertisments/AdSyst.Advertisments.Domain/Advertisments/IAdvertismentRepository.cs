namespace AdSyst.Advertisments.Domain.Advertisments
{
    public interface IAdvertismentRepository
    {
        Task<Advertisment?> GetByIdAsync(
            Guid id,
            bool includeRelationships = false,
            CancellationToken cancellationToken = default
        );
        void Add(Advertisment advertisment);
        Task<bool> AnyAdvertismentsWithType(Guid id, CancellationToken cancellationToken = default);
        Task<bool> AnyAdvertismentsWithCategory(
            Guid id,
            CancellationToken cancellationToken = default
        );
    }
}
