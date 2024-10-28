namespace AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeById
{
    public interface IGetAdvertismentTypeService
    {
        Task<AdvertismentTypeViewModel?> GetAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );
    }
}
