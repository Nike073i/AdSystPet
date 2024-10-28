using AdSyst.Orders.DAL.MongoDb.Enums;
using AdSyst.Orders.DAL.MongoDb.Exceptions;
using AdSyst.Orders.DAL.MongoDb.Interfaces;

namespace AdSyst.Orders.DAL.MongoDb.Models.OrderStates
{
    public class CanceledState : IOrderState
    {
        public OrderStatus Status => OrderStatus.Canceled;

        public IOrderState Cancel() =>
            throw new UnmodifiableOrderStatusException("Заказ уже отменен");

        public IOrderState GoNext() => throw new UnmodifiableOrderStatusException("Заказ отменен");
    }
}
