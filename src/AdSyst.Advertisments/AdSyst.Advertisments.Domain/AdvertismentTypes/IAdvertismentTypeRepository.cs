namespace AdSyst.Advertisments.Domain.AdvertismentTypes
{
    public interface IAdvertismentTypeRepository
    {
        Task<AdvertismentType?> GetByIdAsync(
            Guid id,
            bool includeRelationships = false,
            CancellationToken cancellationToken = default
        );
        void Add(AdvertismentType type);
        void Remove(AdvertismentType type);
        Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
