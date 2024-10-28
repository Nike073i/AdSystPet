using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentDetail
{
    /// <summary>
    /// Модель с деталями объявления
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    /// <param name="Title">Заголовок</param>
    /// <param name="Description">Описание</param>
    /// <param name="AdvertismentTypeId">Идентификатор типа объявления</param>
    /// <param name="AdvertismentTypeName">Название типа объявления</param>
    /// <param name="CategoryId">Идентификатор категории</param>
    /// <param name="CategoryName">Название категории</param>
    /// <param name="Price">Цена</param>
    /// <param name="CreatedAt">Дата создания</param>
    /// <param name="Status">Текущий статус</param>
    /// <param name="UserId">Идентификатор создателя</param>
    /// <param name="ImageIds">Идентификаторы изображений</param>
    public record AdvertismentDetailViewModel(
        Guid Id,
        string Title,
        string Description,
        Guid AdvertismentTypeId,
        string AdvertismentTypeName,
        Guid CategoryId,
        string CategoryName,
        decimal Price,
        DateTimeOffset CreatedAt,
        AdvertismentStatus Status,
        Guid UserId,
        Guid[] ImageIds
    );
}
