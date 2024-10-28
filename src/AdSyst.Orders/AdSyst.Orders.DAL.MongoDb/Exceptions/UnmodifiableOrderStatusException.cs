namespace AdSyst.Orders.DAL.MongoDb.Exceptions
{
    public class UnmodifiableOrderStatusException : Exception
    {
        public UnmodifiableOrderStatusException(string reasone)
            : base($"Невозможно изменить состояние заказа. Причина: {reasone}") { }
    }
}
