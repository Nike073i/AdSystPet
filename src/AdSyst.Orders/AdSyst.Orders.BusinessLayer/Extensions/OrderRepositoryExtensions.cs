using MongoDB.Bson;
using AdSyst.Common.BusinessLayer.Exceptions;
using AdSyst.Orders.DAL.MongoDb.Interfaces;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.BusinessLayer.Extensions
{
    public static class OrderRepositoryExtensions
    {
        public static async Task<Order> GetByIdOrThrow(
            this IOrderRepository orderRepository,
            ObjectId id,
            CancellationToken cancellationToken
        )
        {
            return await orderRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(nameof(Order), id);
        }
    }
}
