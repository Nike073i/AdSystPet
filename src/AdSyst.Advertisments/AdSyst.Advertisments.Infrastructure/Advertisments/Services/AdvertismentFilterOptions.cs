using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Infrastructure.Advertisments.Services
{
    /// <summary>
    /// Опции фильтрации объявлений
    /// </summary>
    /// <param name="Search">Поисковая фраза</param>
    /// <param name="CategoryIds">Фильтр по категориям</param>
    /// <param name="PeriodStart">Дата начала периода</param>
    /// <param name="PeriodEnd">Дата окончания периода</param>
    /// <param name="UserId">Идентификатор создателя</param>
    /// <param name="Status">Статус объявления</param>
    internal record AdvertismentFilterOptions(
        string? Search = null,
        IEnumerable<Guid>? CategoryIds = null,
        DateTimeOffset? PeriodStart = null,
        DateTimeOffset? PeriodEnd = null,
        Guid? UserId = null,
        AdvertismentStatus? Status = null
    );
}
