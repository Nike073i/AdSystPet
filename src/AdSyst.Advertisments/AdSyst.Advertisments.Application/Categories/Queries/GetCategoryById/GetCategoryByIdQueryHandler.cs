using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Categories;

namespace AdSyst.Advertisments.Application.Categories.Queries.GetCategoryById
{
    internal class GetCategoryByIdQueryHandler
        : IRequestHandler<GetCategoryByIdQuery, ErrorOr<CategoryViewModel>>
    {
        private readonly IGetCategoryService _service;

        public GetCategoryByIdQueryHandler(IGetCategoryService service)
        {
            _service = service;
        }

        public async Task<ErrorOr<CategoryViewModel>> Handle(
            GetCategoryByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var categoryViewModel = await _service.GetAsync(request.Id, cancellationToken);

            return categoryViewModel is null ? CategoryErrors.NotFound : categoryViewModel;
        }
    }
}
