using MediatR;
using MongoDB.Driver;
using AdSyst.Orders.BusinessLayer.Extensions.Filter;
using AdSyst.Orders.BusinessLayer.Extensions.Sorting;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.Application.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler
        : IRequestHandler<GetOrderListQuery, GetOrderListQueryResponse>
    {
        private readonly IMongoCollection<Order> _orders;

        public GetOrderListQueryHandler(IMongoCollection<Order> orders)
        {
            _orders = orders;
        }

        public async Task<GetOrderListQueryResponse> Handle(
            GetOrderListQuery request,
            CancellationToken cancellationToken
        )
        {
            (var sortOptions, var paginationOptions, var filterOptions) = request;

            var filter = Builders<Order>.Filter.FilterByOptions(filterOptions);
            var sort = Builders<Order>.Sort.SortByOptions(sortOptions);
            var projection = Builders<Order>
                .Projection
                .Expression(
                    o =>
                        new OrderViewModel(
                            o.Id.ToString(),
                            o.Price,
                            o.CreatedAt,
                            o.Status,
                            o.AdvertismentId,
                            o.Advertisment
                        )
                );

            var data = await _orders
                .Find(filter)
                .Sort(sort)
                .Project(projection)
                .Skip(paginationOptions.PageSize * (paginationOptions.PageNumber - 1))
                .Limit(paginationOptions.PageSize)
                .ToListAsync(cancellationToken);

            return new GetOrderListQueryResponse(paginationOptions.PageNumber, data);
        }
    }
}
