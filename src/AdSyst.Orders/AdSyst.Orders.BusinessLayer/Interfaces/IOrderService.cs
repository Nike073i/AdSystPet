using AdSyst.Orders.BusinessLayer.Models;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.BusinessLayer.Interfaces
{
    public interface IOrderService
    {
        Task<string> CreateAsync(
            CreateOrderDto createDto,
            CancellationToken cancellationToken = default
        );

        Task<Order> MoveStatusAsync(string id, CancellationToken cancellationToken = default);

        Task<Order> CancelAsync(string id, CancellationToken cancellationToken = default);
    }
}
