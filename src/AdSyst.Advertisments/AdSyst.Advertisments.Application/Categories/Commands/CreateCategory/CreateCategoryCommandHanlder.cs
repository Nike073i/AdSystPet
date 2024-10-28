using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Categories;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHanlder : IRequestHandler<CreateCategoryCommand, ErrorOr<Guid>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHanlder(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork
        )
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(
            CreateCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            (string name, var parentCategoryId) = request;

            if (parentCategoryId.HasValue)
            {
                bool isParentCategoryExists = await _categoryRepository.IsExistsAsync(
                    parentCategoryId.Value,
                    cancellationToken
                );
                if (!isParentCategoryExists)
                    return CategoryErrors.NotFound;
            }

            var category = new Category(name) { ParentCategoryId = parentCategoryId };
            _categoryRepository.Add(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return category.Id;
        }
    }
}
