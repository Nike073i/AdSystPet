namespace AdSyst.Common.BusinessLayer.Models
{
    /// <summary>
    /// Модель отображения данных в страничном виде
    /// </summary>
    /// <param name="PageNumber">Текущий номер страницы</param>
    /// <param name="Data">Данные на текущей странице</param>
    /// <typeparam name="T">Тип элементов страницы</typeparam>
    public record PageDto<T>(int PageNumber, IReadOnlyCollection<T> Data);
}
