using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.Api.Models
{
    public record CreateOrderRequest(Guid AdvertismentId, Address AddressTo);
}
