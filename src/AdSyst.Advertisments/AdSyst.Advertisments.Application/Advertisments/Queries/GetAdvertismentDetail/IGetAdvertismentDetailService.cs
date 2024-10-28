namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentDetail
{
    public interface IGetAdvertismentDetailService
    {
        Task<AdvertismentDetailViewModel?> GetAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );
    }
}
