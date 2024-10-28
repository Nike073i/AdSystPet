namespace AdSyst.Orders.SyncDataServices.Advertisments.Models
{
    public record AdvertismentDetailDto(
        Guid Id,
        string Title,
        string Description,
        Guid AdvertismentTypeId,
        string AdvertismentTypeName,
        Guid CategoryId,
        string CategoryName,
        decimal Price,
        DateTimeOffset CreatedAt,
        AdvertismentStatus Status,
        Guid UserId,
        Guid[] ImageIds
    );
}
