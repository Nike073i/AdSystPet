using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using AdSyst.Orders.DAL.MongoDb.Interfaces;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.DAL.MongoDb.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IMongoCollection<Order> collection)
        {
            _orders = collection;
        }

        public async Task<ObjectId> CreateAsync(Order order, CancellationToken cancellationToken)
        {
            await _orders.InsertOneAsync(order, null, cancellationToken);
            return order.Id;
        }

        public async Task<Order?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken)
        {
            var cursor = await _orders.FindAsync(
                order => order.Id == id,
                cancellationToken: cancellationToken
            );
            return await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<ObjectId> UpdateAsync(
            Order changedOrder,
            CancellationToken cancellationToken
        )
        {
            await _orders.ReplaceOneAsync(
                order => order.Id == changedOrder.Id,
                changedOrder,
                new ReplaceOptions { IsUpsert = false },
                cancellationToken: cancellationToken
            );

            return changedOrder.Id;
        }
    }
}
