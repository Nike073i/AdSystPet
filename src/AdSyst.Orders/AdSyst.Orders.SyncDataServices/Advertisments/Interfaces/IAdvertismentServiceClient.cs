using AdSyst.Orders.SyncDataServices.Advertisments.Models;

namespace AdSyst.Orders.SyncDataServices.Advertisments.Interfaces
{
    public interface IAdvertismentServiceClient
    {
        Task<AdvertismentSystemDto> GetAdvertismentSystemDataByIdAsync(
            Guid advertismentId,
            CancellationToken cancellationToken = default
        );

        Task<AdvertismentDetailDto> GetAdvertismentDetailsAsync(
            Guid advertismentId,
            CancellationToken cancellationToken = default
        );
    }
}
