namespace AdSyst.Common.Contracts.Orders
{
    public class OrderCanceledEvent
    {
        public string OrderId { get; set; } = null!;
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public string NewStatus { get; set; } = null!;
    }
}
