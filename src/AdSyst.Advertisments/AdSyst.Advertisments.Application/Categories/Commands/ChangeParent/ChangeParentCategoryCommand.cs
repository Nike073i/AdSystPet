using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.Categories.Commands.ChangeParent
{
    public record ChangeParentCategoryCommand(Guid Id, Guid? ParentCategoryId)
        : IRequest<ErrorOr<Guid>>;
}
