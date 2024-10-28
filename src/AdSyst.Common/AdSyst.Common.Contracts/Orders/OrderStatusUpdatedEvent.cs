namespace AdSyst.Common.Contracts.Orders
{
    public class OrderStatusUpdatedEvent
    {
        public string OrderId { get; set; } = null!;
        public string NewStatus { get; set; } = null!;
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
    }
}
