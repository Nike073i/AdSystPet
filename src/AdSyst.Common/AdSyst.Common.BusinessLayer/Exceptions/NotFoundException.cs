namespace AdSyst.Common.BusinessLayer.Exceptions
{
    /// <summary>
    /// Представляет исключение, связанное с запросом доступа к несуществующей сущности
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Сущность \"{name}\"({key}) - не найдена") { }
    }
}
