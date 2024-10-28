using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.Categories.Queries.GetAllCategoriesQuery
{
    internal class GetCategoryListQueryHandler
        : IRequestHandler<GetCategoryListQuery, ErrorOr<IReadOnlyList<CategoryViewModel>>>
    {
        private readonly IGetCategoryListService _service;

        public GetCategoryListQueryHandler(IGetCategoryListService service)
        {
            _service = service;
        }

        public async Task<ErrorOr<IReadOnlyList<CategoryViewModel>>> Handle(
            GetCategoryListQuery request,
            CancellationToken cancellationToken
        )
        {
            var data = await _service.GetAsync(cancellationToken);
            return data.ToErrorOr();
        }
    }
}
