using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.BusinessLayer.Models
{
    public record CreateOrderDto(
        decimal Price,
        Guid SellerId,
        Guid BuyerId,
        Advertisment Advertisment,
        Address Address
    );
}
