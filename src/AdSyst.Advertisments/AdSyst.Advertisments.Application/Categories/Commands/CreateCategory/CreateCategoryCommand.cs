using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name, Guid? ParentCategoryId)
        : IRequest<ErrorOr<Guid>>;
}
