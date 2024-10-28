using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.Advertisments.Commands.CreateAdvertisment
{
    public record CreateAdvertismentCommand(
        string Title,
        string Description,
        Guid CategoryId,
        Guid AdvertismentTypeId,
        decimal Price,
        Guid[]? ImageIds = null
    ) : IRequest<ErrorOr<Guid>>;
}
