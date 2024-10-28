using ErrorOr;
using AdSyst.Advertisments.Domain.Abstractions;
using AdSyst.Advertisments.Domain.Advertisments.AdvertismentStates;
using AdSyst.Advertisments.Domain.Advertisments.Events;

namespace AdSyst.Advertisments.Domain.Advertisments
{
    /// <summary>
    /// Модель сущности объявления
    /// </summary>
    public class Advertisment : Entity<Guid>
    {
        private IAdvertismentState _state;

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Идентификатор типа объявления
        /// </summary>
        public Guid AdvertismentTypeId { get; private set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Дата создания объявления
        /// </summary>
        /// <value>
        /// При создании указывается время создания объекта.
        /// Доступно для установки в тестах через инициализатор объекта
        /// </value>
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// Текущий статус
        /// </summary>
        /// <value>
        /// При создании указывается статус "На модерации"
        /// </value>
        public AdvertismentStatus Status => _state.Status;

        /// <summary>
        /// Идентификатор пользователя, создавшего объявление
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// Изображения
        /// </summary>
        private List<AdvertismentImage>? _images;

        public IReadOnlyCollection<AdvertismentImage>? Images => _images?.ToList();

        private void SetImages(Guid[] imageIds) =>
            _images = imageIds
                .Select((imageId, index) => new AdvertismentImage(Id, imageId, (byte)index))
                .ToList();

        private Advertisment() { }

        public ErrorOr<Success> Reject() => ChangeState(_state => _state.Reject());

        public ErrorOr<Success> Approve() => ChangeState(_state => _state.Approve());

        public ErrorOr<Success> Archive() => ChangeState(_state => _state.Archive());

        public ErrorOr<Success> Update(
            string? title,
            string? description,
            decimal? price,
            Guid? advertismentTypeId,
            Guid? categoryId,
            Guid[]? imageIds
        )
        {
            Title = title ?? Title;
            Description = description ?? Description;
            Price = price ?? Price;
            AdvertismentTypeId = advertismentTypeId ?? AdvertismentTypeId;
            CategoryId = categoryId ?? CategoryId;

            if (imageIds is not null)
                SetImages(imageIds);

            return ChangeState(
                _state => _state.Update(),
                new AdvertismentUpdatedDomainEvent(UserId, Id, Status)
            );
        }

        public ErrorOr<Success> Restore() => ChangeState(_state => _state.Restore());

        private ErrorOr<Success> ChangeState(
            Func<IAdvertismentState, ErrorOr<IAdvertismentState>> changeStateFunc,
            IDomainEvent? domainEvent = null
        )
        {
            var changeResult = changeStateFunc(_state);
            if (changeResult.IsError)
                return changeResult.Errors;

            _state = changeResult.Value;

            domainEvent ??= new AdvertismentStatusChangedDomainEvent(UserId, Id, Status);
            Raise(domainEvent);

            return Result.Success;
        }

        public static Advertisment Create(
            string title,
            string description,
            Guid advertismentTypeId,
            Guid categoryId,
            decimal price,
            Guid userId,
            Guid[]? imageIds,
            Func<DateTimeOffset> clock,
            Guid? id = null
        )
        {
            var advertisment = new Advertisment
            {
                Id = id ?? Guid.NewGuid(),
                _state = new ModerationState(),
                Title = title,
                Description = description,
                AdvertismentTypeId = advertismentTypeId,
                CategoryId = categoryId,
                Price = price,
                CreatedAt = clock(),
                UserId = userId
            };
            if (imageIds is not null)
                advertisment.SetImages(imageIds);

            advertisment.Raise(
                new AdvertismentCreatedDomainEvent(
                    userId,
                    advertisment.Id,
                    title,
                    advertisment.Status
                )
            );

            return advertisment;
        }
    }
}
