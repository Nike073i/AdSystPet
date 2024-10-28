using ErrorOr;

namespace AdSyst.Advertisments.Domain.Advertisments.AdvertismentStates
{
    /// <summary>
    /// Состояние объявления
    /// </summary>
    public interface IAdvertismentState
    {
        /// <summary>
        /// Статус объяления
        /// </summary>
        AdvertismentStatus Status { get; }

        /// <summary>
        /// Архивировать объявление
        /// </summary>
        /// <returns>Состояние после архивации</returns>
        ErrorOr<IAdvertismentState> Archive();

        /// <summary>
        /// Восстановить объявление из архива
        /// </summary>
        /// <returns>Состояние после восстановления</returns>
        ErrorOr<IAdvertismentState> Restore();

        /// <summary>
        /// Отклонить публикацию объявления
        /// </summary>
        /// <returns>Состояние после отклонения</returns>
        ErrorOr<IAdvertismentState> Reject();

        /// <summary>
        /// Одобрить публикацию объявления
        /// </summary>
        /// <returns>Состояние после одобрения</returns>
        ErrorOr<IAdvertismentState> Approve();

        /// <summary>
        /// Обновить данные объявления
        /// </summary>
        /// <returns>Состояние после обновления</returns>
        ErrorOr<IAdvertismentState> Update();
    }
}
