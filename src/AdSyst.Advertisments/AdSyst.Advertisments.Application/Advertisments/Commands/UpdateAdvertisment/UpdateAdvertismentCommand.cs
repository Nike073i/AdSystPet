using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.Advertisments.Commands.UpdateAdvertisment
{
    public record UpdateAdvertismentCommand(
        Guid Id,
        string? Title = null,
        string? Description = null,
        decimal? Price = null,
        Guid? CategoryId = null,
        Guid? AdvertismentTypeId = null,
        Guid[]? ImageIds = null
    ) : IRequest<ErrorOr<Guid>>;
}
