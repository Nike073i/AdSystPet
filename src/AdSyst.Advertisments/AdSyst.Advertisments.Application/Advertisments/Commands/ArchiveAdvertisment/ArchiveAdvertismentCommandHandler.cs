using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.Advertisments.Commands.ArchiveAdvertisment
{
    public class ArchiveAdvertismentCommandHandler
        : IRequestHandler<ArchiveAdvertismentCommand, ErrorOr<AdvertismentStatus>>
    {
        private readonly IAdvertismentRepository _advertismentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public ArchiveAdvertismentCommandHandler(
            IAdvertismentRepository advertismentRepository,
            IUnitOfWork unitOfWork,
            IUserContext userContext
        )
        {
            _advertismentRepository = advertismentRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<ErrorOr<AdvertismentStatus>> Handle(
            ArchiveAdvertismentCommand request,
            CancellationToken cancellationToken
        )
        {
            var userId = _userContext.UserId;
            if (userId is null)
                return AdvertismentErrors.Unauthorized;

            var advertismentId = request.AdvertismentId;

            var advertisment = await _advertismentRepository.GetByIdAsync(
                advertismentId,
                cancellationToken: cancellationToken
            );
            if (advertisment is null)
                return AdvertismentErrors.NotFound;

            if (advertisment.UserId != userId)
                return AdvertismentErrors.Forbidden;

            var archiveResult = advertisment.Archive();
            if (archiveResult.IsError)
                return archiveResult.Errors;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return advertisment.Status;
        }
    }
}
