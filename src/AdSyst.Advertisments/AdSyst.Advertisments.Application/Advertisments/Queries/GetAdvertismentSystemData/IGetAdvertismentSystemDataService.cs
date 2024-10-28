namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentSystemData
{
    public interface IGetAdvertismentSystemDataService
    {
        Task<AdvertismentSystemViewModel?> GetAsync(Guid id, CancellationToken cancellationToken);
    }
}
