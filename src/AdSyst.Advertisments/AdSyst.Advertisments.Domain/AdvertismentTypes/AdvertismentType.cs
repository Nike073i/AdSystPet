using AdSyst.Advertisments.Domain.Abstractions;

namespace AdSyst.Advertisments.Domain.AdvertismentTypes
{
    /// <summary>
    /// Модель сущности типа объявления
    /// </summary>
    public class AdvertismentType : Entity<Guid>
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        public AdvertismentType(string name)
        {
            Name = name;
        }
    }
}
