using MongoDB.Bson.Serialization;
using AdSyst.Common.DAL.MongoDb.Mappers;
using AdSyst.Orders.DAL.MongoDb.Models;
using AdSyst.Orders.DAL.MongoDb.Models.OrderStates;

namespace AdSyst.Orders.DAL.MongoDb.Mappers
{
    public class OrderClassMapper : IClassMapper<OrderClassMapper>
    {
        public void Register()
        {
            BsonClassMap.RegisterClassMap<Order>(classMap =>
            {
                classMap.AutoMap();
                classMap.MapField("_state").SetSerializer(new OrderStateConverter());
                classMap.MapProperty(o => o.Price);
                classMap.MapProperty(o => o.Status);
                classMap.MapProperty(o => o.Address);
                classMap.MapProperty(o => o.BuyerId);
                classMap.MapProperty(o => o.SellerId);
                classMap.MapProperty(o => o.Advertisment);
                classMap.MapProperty(o => o.AdvertismentId);
                classMap.MapCreator(
                    o => new Order(o.Price, o.SellerId, o.BuyerId, o.Advertisment, o.Address)
                );
            });
        }
    }
}
