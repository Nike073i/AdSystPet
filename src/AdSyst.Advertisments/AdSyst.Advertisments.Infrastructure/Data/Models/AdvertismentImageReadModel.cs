namespace AdSyst.Advertisments.Infrastructure.Data.Models
{
    public class AdvertismentImageReadModel
    {
        public required Guid Id { get; set; }
        public required Guid AdvertismentId { get; set; }
        public required Guid ImageId { get; set; }
        public required byte Order { get; set; }
    }
}
