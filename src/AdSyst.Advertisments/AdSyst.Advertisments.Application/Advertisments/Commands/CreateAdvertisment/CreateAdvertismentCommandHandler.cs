using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Advertisments.Domain.Categories;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Common.Application.Abstractions.Clock;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.Advertisments.Commands.CreateAdvertisment
{
    public class CreateAdvertismentCommandHandler
        : IRequestHandler<CreateAdvertismentCommand, ErrorOr<Guid>>
    {
        private readonly IAdvertismentRepository _advertismentRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAdvertismentTypeRepository _advertismentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateAdvertismentCommandHandler(
            IAdvertismentRepository advertismentRepository,
            ICategoryRepository categoryRepository,
            IAdvertismentTypeRepository advertismentTypeRepository,
            IUnitOfWork unitOfWork,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider
        )
        {
            _advertismentRepository = advertismentRepository;
            _categoryRepository = categoryRepository;
            _advertismentTypeRepository = advertismentTypeRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<Guid>> Handle(
            CreateAdvertismentCommand request,
            CancellationToken cancellationToken
        )
        {
            var userId = _userContext.UserId;
            if (userId is null)
                return AdvertismentErrors.Unauthorized;

            bool isCategoryExists = await _categoryRepository.IsExistsAsync(
                request.CategoryId,
                cancellationToken
            );
            if (!isCategoryExists)
                return CategoryErrors.NotFound;

            bool isTypeExists = await _advertismentTypeRepository.IsExistsAsync(
                request.AdvertismentTypeId,
                cancellationToken
            );
            if (!isTypeExists)
                return AdvertismentTypeErrors.NotFound;

            var advertisment = Advertisment.Create(
                request.Title,
                request.Description,
                request.AdvertismentTypeId,
                request.CategoryId,
                request.Price,
                userId.Value,
                request.ImageIds,
                () => _dateTimeProvider.UtcNow
            );

            _advertismentRepository.Add(advertisment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return advertisment.Id;
        }
    }
}
