namespace AdSyst.Common.Contracts.Orders
{
    public class OrderCreatedEvent
    {
        public string OrderId { get; set; } = null!;
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public string NewStatus { get; set; } = null!;
    }
}
