namespace AdSyst.Advertisments.Domain.Advertisments
{
    /// <summary>
    /// Модель изображения объявления
    /// </summary>
    public class AdvertismentImage
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        /// <value>
        /// Устанавливается автоматически с помощью EntityFrameworkCore.
        /// Доступно для установки в тестах через инициализатор объекта
        /// </value>
        public Guid Id { get; init; }

        /// <summary>
        /// Идентификатор объявления
        /// </summary>
        public Guid AdvertismentId { get; private set; }

        /// <summary>
        /// Идентификатор изображения
        /// </summary>
        public Guid ImageId { get; private set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public byte Order { get; set; }

        private AdvertismentImage() { }

        internal AdvertismentImage(Guid advertismentId, Guid imageId, byte order, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            AdvertismentId = advertismentId;
            ImageId = imageId;
            Order = order;
        }
    }
}
