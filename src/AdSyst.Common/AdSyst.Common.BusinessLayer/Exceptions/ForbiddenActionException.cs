namespace AdSyst.Common.BusinessLayer.Exceptions
{
    /// <summary>
    /// Представляет исключение, связанное с запросом выполнения запрещенной операции
    /// </summary>
    public class ForbiddenActionException : Exception
    {
        public ForbiddenActionException(string actionName, string reason)
            : base($"Вызов действия \"{actionName}\" запрещен. Причина: {reason}") { }

        public ForbiddenActionException()
            : base($"Вызов функционала запрещен") { }
    }
}
