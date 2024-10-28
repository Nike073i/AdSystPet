using ErrorOr;
using MediatR;
using AdSyst.Moderation.BusinessLayer.Interfaces;

namespace AdSyst.Moderation.Application.Advertisments.Commands.CreateAdvertisment
{
    public class CreateAdvertismentCommandHandler
        : IRequestHandler<CreateAdvertismentCommand, ErrorOr<Created>>
    {
        private readonly ICorrectionService _correctionService;

        public CreateAdvertismentCommandHandler(ICorrectionService correctionService)
        {
            _correctionService = correctionService;
        }

        public Task<ErrorOr<Created>> Handle(
            CreateAdvertismentCommand request,
            CancellationToken cancellationToken
        ) =>
            _correctionService.CreateAdvertismentAsync(
                request.AdvertismentId,
                request.AdvertismentAuthorId,
                cancellationToken
            );
    }
}
