using AdSyst.Orders.DAL.MongoDb.Enums;
using AdSyst.Orders.DAL.MongoDb.Interfaces;

namespace AdSyst.Orders.DAL.MongoDb.Models.OrderStates
{
    public class CreatedState : IOrderState
    {
        public OrderStatus Status => OrderStatus.Created;

        public IOrderState Cancel() => new CanceledState();

        public IOrderState GoNext() => new HandledState();
    }
}
