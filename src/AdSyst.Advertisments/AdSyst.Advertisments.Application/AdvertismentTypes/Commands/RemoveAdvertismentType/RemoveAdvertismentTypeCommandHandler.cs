using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.AdvertismentTypes.Commands.RemoveAdvertismentType
{
    public class RemoveAdvertismentTypeCommandHandler
        : IRequestHandler<RemoveAdvertismentTypeCommand, ErrorOr<Deleted>>
    {
        private readonly IAdvertismentTypeRepository _advertismentTypeRepository;
        private readonly IAdvertismentRepository _advertismentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveAdvertismentTypeCommandHandler(
            IAdvertismentTypeRepository advertismentTypeRepository,
            IAdvertismentRepository advertismentRepository,
            IUnitOfWork unitOfWork
        )
        {
            _advertismentTypeRepository = advertismentTypeRepository;
            _advertismentRepository = advertismentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Deleted>> Handle(
            RemoveAdvertismentTypeCommand request,
            CancellationToken cancellationToken
        )
        {
            var advertismentType = await _advertismentTypeRepository.GetByIdAsync(
                request.Id,
                cancellationToken: cancellationToken
            );
            if (advertismentType is null)
                return AdvertismentTypeErrors.NotFound;

            bool anyAdvertisments = await _advertismentRepository.AnyAdvertismentsWithType(
                request.Id,
                cancellationToken
            );

            if (anyAdvertisments)
                return AdvertismentTypeErrors.CannotBeRemovedBecauseItIsUsedInAdvertisment;

            _advertismentTypeRepository.Remove(advertismentType);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Deleted;
        }
    }
}
