namespace AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeList
{
    public interface IGetAdvertismentTypeListService
    {
        Task<IReadOnlyList<AdvertismentTypeViewModel>> GetAsync(
            CancellationToken cancellationToken = default
        );
    }
}
