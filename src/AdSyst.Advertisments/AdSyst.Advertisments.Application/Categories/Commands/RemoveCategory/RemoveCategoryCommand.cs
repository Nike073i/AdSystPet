using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.Categories.Commands.RemoveCategory
{
    public record RemoveCategoryCommand(Guid CategoryId) : IRequest<ErrorOr<Deleted>>;
}
