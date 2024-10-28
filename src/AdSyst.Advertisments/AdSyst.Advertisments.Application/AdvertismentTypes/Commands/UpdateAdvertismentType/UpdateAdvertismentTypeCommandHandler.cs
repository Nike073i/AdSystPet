using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.AdvertismentTypes.Commands.UpdateAdvertismentType
{
    public class UpdateAdvertismentTypeCommandHandler
        : IRequestHandler<UpdateAdvertismentTypeCommand, ErrorOr<Guid>>
    {
        private readonly IAdvertismentTypeRepository _advertismentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAdvertismentTypeCommandHandler(
            IAdvertismentTypeRepository advertismentTypeRepository,
            IUnitOfWork unitOfWork
        )
        {
            _advertismentTypeRepository = advertismentTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(
            UpdateAdvertismentTypeCommand request,
            CancellationToken cancellationToken
        )
        {
            var type = await _advertismentTypeRepository.GetByIdAsync(
                request.Id,
                cancellationToken: cancellationToken
            );
            if (type is null)
                return AdvertismentTypeErrors.NotFound;

            type.Name = request.Name;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return type.Id;
        }
    }
}
