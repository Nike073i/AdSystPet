using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using AdSyst.Common.BusinessLayer.Exceptions;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.Application.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryHandler
        : IRequestHandler<GetOrderDetailsQuery, GetOrderDetailsQueryResponse>
    {
        private readonly IMongoCollection<Order> _orders;

        public GetOrderDetailsQueryHandler(IMongoCollection<Order> orders)
        {
            _orders = orders;
        }

        public async Task<GetOrderDetailsQueryResponse> Handle(
            GetOrderDetailsQuery request,
            CancellationToken cancellationToken
        )
        {
            var projection = Builders<Order>
                .Projection
                .Expression(
                    order =>
                        new OrderDetailsViewModel(
                            order.Id.ToString(),
                            order.Price,
                            order.CreatedAt,
                            order.Status,
                            order.TrackNumber,
                            order.SellerId,
                            order.BuyerId,
                            order.AdvertismentId,
                            order.Advertisment,
                            order.Address
                        )
                );
            var filter = Builders<Order>.Filter.Eq(order => order.Id, new ObjectId(request.Id));
            var order =
                await _orders
                    .Find(filter)
                    .Project(projection)
                    .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(nameof(Order), request.Id);
            return new(order);
        }
    }
}
