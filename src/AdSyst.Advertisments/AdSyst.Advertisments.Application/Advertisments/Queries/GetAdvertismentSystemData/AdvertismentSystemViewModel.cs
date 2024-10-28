using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentSystemData
{
    /// <summary>
    /// Модель системной информации по объявлению
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    /// <param name="UserId">Идентификатор создателя</param>
    /// <param name="CreatedAt">Дата создания объявления</param>
    /// <param name="Status">Текущий статус</param>
    public record AdvertismentSystemViewModel(
        Guid Id,
        Guid UserId,
        DateTimeOffset CreatedAt,
        AdvertismentStatus Status
    );
}
