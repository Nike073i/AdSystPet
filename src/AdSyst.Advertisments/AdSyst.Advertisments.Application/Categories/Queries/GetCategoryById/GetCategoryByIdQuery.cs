using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<ErrorOr<CategoryViewModel>>;
}
