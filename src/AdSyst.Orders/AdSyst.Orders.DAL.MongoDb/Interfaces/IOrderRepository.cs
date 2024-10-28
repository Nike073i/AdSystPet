using MongoDB.Bson;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.DAL.MongoDb.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken = default);

        Task<ObjectId> CreateAsync(Order order, CancellationToken cancellationToken = default);

        Task<ObjectId> UpdateAsync(
            Order changedOrder,
            CancellationToken cancellationToken = default
        );
    }
}
