using MongoDB.Bson;
using AdSyst.Orders.DAL.MongoDb.Enums;
using AdSyst.Orders.DAL.MongoDb.Interfaces;
using AdSyst.Orders.DAL.MongoDb.Models.OrderStates;

namespace AdSyst.Orders.DAL.MongoDb.Models
{
    public class Order
    {
        private IOrderState _state;

        public ObjectId Id { get; init; }

        public decimal Price { get; }

        public DateTimeOffset CreatedAt { get; init; }

        public OrderStatus Status => _state.Status;

        public Address Address { get; }

        public string? TrackNumber { get; set; }

        public Guid SellerId { get; }

        public Guid BuyerId { get; }

        [Obsolete($"Данные по объявлению указаны в свойстве {nameof(Advertisment)}")]
        public int? AdvertismentId { get; }

        public Advertisment? Advertisment { get; }

        public Order(
            decimal price,
            Guid sellerId,
            Guid buyerId,
            Advertisment advertisment,
            Address address
        )
        {
            _state = new CreatedState();
            Price = price;
            CreatedAt = DateTimeOffset.Now;
            SellerId = sellerId;
            BuyerId = buyerId;
            Advertisment = advertisment;
            Address = address;
        }

        public void GoNext() => _state = _state.GoNext();

        public void Cancel() => _state = _state.Cancel();
    }
}
