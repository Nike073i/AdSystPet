using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.AdvertismentTypes;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.AdvertismentTypes.Commands.CreateAdvertismentType
{
    public class CreateAdvertismentTypeCommandHandler
        : IRequestHandler<CreateAdvertismentTypeCommand, ErrorOr<Guid>>
    {
        private readonly IAdvertismentTypeRepository _advertismentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAdvertismentTypeCommandHandler(
            IAdvertismentTypeRepository advertismentTypeRepository,
            IUnitOfWork unitOfWork
        )
        {
            _advertismentTypeRepository = advertismentTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(
            CreateAdvertismentTypeCommand request,
            CancellationToken cancellationToken
        )
        {
            var type = new AdvertismentType(request.Name);
            _advertismentTypeRepository.Add(type);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return type.Id;
        }
    }
}
