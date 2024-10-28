namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.Create
{
    public class CreateAdvertismentRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required Guid CategoryId { get; set; }
        public required Guid AdvertismentTypeId { get; set; }
        public required decimal Price { get; set; }
        public Guid[]? ImageIds { get; set; }
    }
}
