using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Infrastructure.Data.Models
{
    public class AdvertismentReadModel
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required Guid AdvertismentTypeId { get; set; }
        public required Guid CategoryId { get; set; }
        public required decimal Price { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public required AdvertismentStatus Status { get; set; }
        public required Guid UserId { get; set; }

        public AdvertismentTypeReadModel? AdvertismentType { get; set; }
        public CategoryReadModel? Category { get; set; }
        public List<AdvertismentImageReadModel>? Images { get; set; }
    }
}
