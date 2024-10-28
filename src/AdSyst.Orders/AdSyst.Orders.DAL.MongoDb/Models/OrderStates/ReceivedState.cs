using AdSyst.Orders.DAL.MongoDb.Enums;
using AdSyst.Orders.DAL.MongoDb.Exceptions;
using AdSyst.Orders.DAL.MongoDb.Interfaces;

namespace AdSyst.Orders.DAL.MongoDb.Models.OrderStates
{
    public class ReceivedState : IOrderState
    {
        public OrderStatus Status => OrderStatus.Received;

        public IOrderState Cancel() =>
            throw new UnmodifiableOrderStatusException("Товар уже получен");

        public IOrderState GoNext() =>
            throw new UnmodifiableOrderStatusException("Товар уже получен");
    }
}
