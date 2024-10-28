using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.Categories.Queries.GetAllCategoriesQuery
{
    public record GetCategoryListQuery : IRequest<ErrorOr<IReadOnlyList<CategoryViewModel>>>;
}
