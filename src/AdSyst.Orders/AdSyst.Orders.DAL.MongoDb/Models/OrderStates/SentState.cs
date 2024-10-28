using AdSyst.Orders.DAL.MongoDb.Enums;
using AdSyst.Orders.DAL.MongoDb.Interfaces;

namespace AdSyst.Orders.DAL.MongoDb.Models.OrderStates
{
    public class SentState : IOrderState
    {
        public OrderStatus Status => OrderStatus.Sended;

        public IOrderState Cancel() => new CanceledState();

        public IOrderState GoNext() => new ReceivedState();
    }
}
