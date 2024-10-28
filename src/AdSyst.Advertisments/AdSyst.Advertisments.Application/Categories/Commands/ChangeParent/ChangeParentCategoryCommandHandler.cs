using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Categories;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.Categories.Commands.ChangeParent
{
    public class ChangeParentCategoryCommandHandler
        : IRequestHandler<ChangeParentCategoryCommand, ErrorOr<Guid>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeParentCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork
        )
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(
            ChangeParentCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            (var categoryId, var parentId) = request;
            if (parentId.HasValue)
            {
                bool isParentCategoryExists = await _categoryRepository.IsExistsAsync(
                    parentId.Value,
                    cancellationToken
                );
                if (!isParentCategoryExists)
                    return CategoryErrors.NotFound;
            }
            var category = await _categoryRepository.GetByIdAsync(
                categoryId,
                cancellationToken: cancellationToken
            );
            if (category is null)
                return CategoryErrors.NotFound;

            category.ParentCategoryId = parentId;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return category.Id;
        }
    }
}
