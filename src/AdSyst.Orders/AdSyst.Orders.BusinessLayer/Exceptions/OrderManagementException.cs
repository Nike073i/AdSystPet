namespace AdSyst.Orders.BusinessLayer.Exceptions
{
    public class OrderManagementException : Exception
    {
        public OrderManagementException(string actionName, string reason)
            : base(
                $"Ошибка выполнения операции с заказом. Действие: \"{actionName}\". Причина: {reason}"
            ) { }
    }
}
