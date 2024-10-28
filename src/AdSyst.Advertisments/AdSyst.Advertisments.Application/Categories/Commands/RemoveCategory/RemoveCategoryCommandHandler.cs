using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.Categories;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.Categories.Commands.RemoveCategory
{
    public class RemoveCategoryCommandHandler
        : IRequestHandler<RemoveCategoryCommand, ErrorOr<Deleted>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAdvertismentRepository _advertismentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IAdvertismentRepository advertismentRepository,
            IUnitOfWork unitOfWork
        )
        {
            _categoryRepository = categoryRepository;
            _advertismentRepository = advertismentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Deleted>> Handle(
            RemoveCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            var category = await _categoryRepository.GetByIdAsync(
                request.CategoryId,
                includeRelationships: true,
                cancellationToken
            );
            if (category is null)
                return CategoryErrors.NotFound;

            if (category.Children!.Any())
                return CategoryErrors.CannotBeRemovedBecauseThereAreChildCategories;

            bool anyAdvertisments = await _advertismentRepository.AnyAdvertismentsWithCategory(
                request.CategoryId,
                cancellationToken
            );
            if (anyAdvertisments)
                return CategoryErrors.CannotBeRemovedBecauseItIsUsedInAdvertisment;

            _categoryRepository.Remove(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Deleted;
        }
    }
}
