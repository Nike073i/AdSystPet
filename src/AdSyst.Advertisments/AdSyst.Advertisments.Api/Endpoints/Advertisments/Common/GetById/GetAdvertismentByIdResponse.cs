using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.GetById
{
    public record GetAdvertismentByIdResponse(
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
