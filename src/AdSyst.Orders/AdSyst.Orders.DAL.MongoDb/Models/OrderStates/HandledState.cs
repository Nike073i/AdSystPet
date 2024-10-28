using AdSyst.Orders.DAL.MongoDb.Enums;
using AdSyst.Orders.DAL.MongoDb.Interfaces;

namespace AdSyst.Orders.DAL.MongoDb.Models.OrderStates
{
    public class HandledState : IOrderState
    {
        public OrderStatus Status => OrderStatus.Handled;

        public IOrderState Cancel() => new CanceledState();

        public IOrderState GoNext() => new SentState();
    }
}
