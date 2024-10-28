using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<ErrorOr<Guid>>;
}
