namespace AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeById
{
    /// <summary>
    /// Модель типа объявления
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    /// <param name="Name">Название</param>
    public record AdvertismentTypeViewModel(Guid Id, string Name);
}
