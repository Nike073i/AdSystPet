using MongoDB.Bson;
using AdSyst.Orders.BusinessLayer.Extensions;
using AdSyst.Orders.BusinessLayer.Interfaces;
using AdSyst.Orders.BusinessLayer.Models;
using AdSyst.Orders.DAL.MongoDb.Interfaces;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.BusinessLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateAsync(
            CreateOrderDto createDto,
            CancellationToken cancellationToken
        )
        {
            var order = new Order(
                createDto.Price,
                createDto.SellerId,
                createDto.BuyerId,
                createDto.Advertisment,
                createDto.Address
            );
            var id = await _repository.CreateAsync(order, cancellationToken);
            return id.ToString();
        }

        public Task<Order> MoveStatusAsync(string id, CancellationToken cancellationToken) =>
            ChangeOrderStatusAsync(id, currentState => currentState.GoNext(), cancellationToken);

        public Task<Order> CancelAsync(string id, CancellationToken cancellationToken) =>
            ChangeOrderStatusAsync(id, currentState => currentState.Cancel(), cancellationToken);

        private async Task<Order> ChangeOrderStatusAsync(
            string id,
            Action<Order> changeStateAction,
            CancellationToken cancellationToken
        )
        {
            var order = await _repository.GetByIdOrThrow(new ObjectId(id), cancellationToken);

            changeStateAction(order);
            await _repository.UpdateAsync(order, cancellationToken);
            return order;
        }
    }
}
