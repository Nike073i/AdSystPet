namespace AdSyst.Orders.Application.Orders.Queries.GetOrderList
{
    public record GetOrderListQueryResponse(int PageNumber, IEnumerable<OrderViewModel> Orders);
}
