using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Common.Application.Abstractions.Data;

namespace AdSyst.Advertisments.Application.Advertisments.Commands.RestoreAdvertisment
{
    public class RestoreAdvertismentCommandHandler
        : IRequestHandler<RestoreAdvertismentCommand, ErrorOr<AdvertismentStatus>>
    {
        private readonly IAdvertismentRepository _advertismentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public RestoreAdvertismentCommandHandler(
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
            RestoreAdvertismentCommand request,
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

            var restoreResult = advertisment.Restore();
            if (restoreResult.IsError)
                return restoreResult.Errors;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return advertisment.Status;
        }
    }
}
