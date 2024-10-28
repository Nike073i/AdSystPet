using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.AdvertismentTypes.Commands.RemoveAdvertismentType
{
    public record RemoveAdvertismentTypeCommand(Guid Id) : IRequest<ErrorOr<Deleted>>;
}
