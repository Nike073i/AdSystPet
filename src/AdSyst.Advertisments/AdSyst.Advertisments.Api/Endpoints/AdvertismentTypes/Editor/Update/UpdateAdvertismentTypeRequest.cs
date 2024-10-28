namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Editor.Update
{
    public class UpdateAdvertismentTypeRequest
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
