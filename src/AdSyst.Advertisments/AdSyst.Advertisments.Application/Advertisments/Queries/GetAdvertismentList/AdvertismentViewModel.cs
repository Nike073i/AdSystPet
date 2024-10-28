using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList
{
    /// <summary>
    /// Модель общей информации по объявлению
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    /// <param name="Title">Заголовок</param>
    /// <param name="Price">Цена</param>
    /// <param name="Status">Статус объявления</param>
    /// <param name="CategoryId">Идентификатор категории</param>
    /// <param name="CategoryName">Название категории</param>
    /// <param name="MainImageId">Идентификатор главного изображения</param>
    /// <param name="CreatedAt">Дата создания объявления</param>
    public record AdvertismentViewModel(
        Guid Id,
        string Title,
        decimal Price,
        AdvertismentStatus Status,
        Guid CategoryId,
        string CategoryName,
        Guid? MainImageId,
        DateTimeOffset CreatedAt
    );
}
