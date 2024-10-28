using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Categories;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ErrorOr<Guid>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork
        )
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken
        )
        {
            var category = await _categoryRepository.GetByIdAsync(
                request.Id,
                cancellationToken: cancellationToken
            );
            if (category is null)
                return CategoryErrors.NotFound;

            category.Name = request.Name;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return category.Id;
        }
    }
}
