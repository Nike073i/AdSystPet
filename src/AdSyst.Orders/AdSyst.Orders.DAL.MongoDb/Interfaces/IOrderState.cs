using AdSyst.Orders.DAL.MongoDb.Enums;

namespace AdSyst.Orders.DAL.MongoDb.Interfaces
{
    public interface IOrderState
    {
        IOrderState Cancel();
        IOrderState GoNext();
        OrderStatus Status { get; }
    }
}
