using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList
{
    public record AdvertismentFilterDto(
        string? Search = null,
        Guid? CategoryId = null,
        DateTimeOffset? PeriodStart = null,
        DateTimeOffset? PeriodEnd = null,
        Guid? UserId = null,
        AdvertismentStatus? Status = null
    );
}
