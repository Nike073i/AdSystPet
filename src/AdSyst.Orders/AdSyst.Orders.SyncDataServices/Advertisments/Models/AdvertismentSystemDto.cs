namespace AdSyst.Orders.SyncDataServices.Advertisments.Models
{
    public record AdvertismentSystemDto(
        Guid Id,
        Guid UserId,
        DateTimeOffset CreatedAt,
        AdvertismentStatus Status
    );
}
