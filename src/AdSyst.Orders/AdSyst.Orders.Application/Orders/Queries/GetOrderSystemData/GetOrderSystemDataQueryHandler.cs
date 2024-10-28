using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using AdSyst.Common.BusinessLayer.Exceptions;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.Application.Orders.Queries.GetOrderSystemData
{
    public class GetOrderSystemDataQueryHandler
        : IRequestHandler<GetOrderSystemDataQuery, GetOrderSystemDataQueryResponse>
    {
        private readonly IMongoCollection<Order> _orders;

        public GetOrderSystemDataQueryHandler(IMongoCollection<Order> orders)
        {
            _orders = orders;
        }

        public async Task<GetOrderSystemDataQueryResponse> Handle(
            GetOrderSystemDataQuery request,
            CancellationToken cancellationToken
        )
        {
            var projection = Builders<Order>
                .Projection
                .Expression(
                    order =>
                        new OrderSystemViewModel(
                            order.Id.ToString(),
                            order.SellerId,
                            order.BuyerId,
                            order.Status,
                            order.CreatedAt
                        )
                );
            var filter = Builders<Order>
                .Filter
                .Eq(order => order.Id, new ObjectId(request.OrderId));
            var order =
                await _orders
                    .Find(filter)
                    .Project(projection)
                    .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(nameof(Order), request.OrderId);
            return new(order);
        }
    }
}
