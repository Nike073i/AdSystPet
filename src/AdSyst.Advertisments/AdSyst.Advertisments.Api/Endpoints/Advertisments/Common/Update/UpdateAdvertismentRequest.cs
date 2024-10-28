namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.Update
{
    public class UpdateAdvertismentRequest
    {
        public Guid Id { get; set; }

        public string? Title { get; init; }

        public string? Description { get; init; }

        public decimal? Price { get; init; }

        public Guid? CategoryId { get; init; }

        public Guid? AdvertismentTypeId { get; init; }

        public Guid[]? ImageIds { get; init; }
    }
}
