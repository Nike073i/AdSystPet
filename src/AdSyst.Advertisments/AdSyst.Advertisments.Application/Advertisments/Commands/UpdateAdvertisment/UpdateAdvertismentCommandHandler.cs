using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Advertisments.Domain.Categories;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.Advertisments.Commands.UpdateAdvertisment
{
    public class UpdateAdvertismentCommandHandler
        : IRequestHandler<UpdateAdvertismentCommand, ErrorOr<Guid>>
    {
        private readonly IAdvertismentRepository _advertismentRepository;
        private readonly IAdvertismentTypeRepository _advertismentTypeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UpdateAdvertismentCommandHandler(
            IAdvertismentRepository advertismentRepository,
            IAdvertismentTypeRepository advertismentTypeRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            IUserContext userContext
        )
        {
            _advertismentRepository = advertismentRepository;
            _advertismentTypeRepository = advertismentTypeRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<ErrorOr<Guid>> Handle(
            UpdateAdvertismentCommand request,
            CancellationToken cancellationToken
        )
        {
            var userId = _userContext.UserId;
            if (userId is null)
                return AdvertismentErrors.Unauthorized;

            if (request.AdvertismentTypeId.HasValue)
            {
                bool isTypeExists = await _advertismentTypeRepository.IsExistsAsync(
                    request.AdvertismentTypeId.Value,
                    cancellationToken
                );
                if (!isTypeExists)
                    return AdvertismentTypeErrors.NotFound;
            }

            if (request.CategoryId.HasValue)
            {
                bool isCategoryExists = await _categoryRepository.IsExistsAsync(
                    request.CategoryId.Value,
                    cancellationToken
                );
                if (!isCategoryExists)
                    return CategoryErrors.NotFound;
            }

            var advertisment = await _advertismentRepository.GetByIdAsync(
                request.Id,
                includeRelationships: true,
                cancellationToken
            );

            if (advertisment is null)
                return AdvertismentErrors.NotFound;

            if (advertisment.UserId != userId)
                return AdvertismentErrors.Forbidden;

            var updateResult = advertisment.Update(
                request.Title,
                request.Description,
                request.Price,
                request.AdvertismentTypeId,
                request.CategoryId,
                request.ImageIds
            );

            if (updateResult.IsError)
                return updateResult.Errors;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return advertisment.Id;
        }
    }
}
