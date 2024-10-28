namespace AdSyst.Orders.DAL.MongoDb.Models
{
    public record Address(string City, string Street, string House, string Flat)
    {
        public GeoPosition? GeoPosition { get; init; }
    };
}
