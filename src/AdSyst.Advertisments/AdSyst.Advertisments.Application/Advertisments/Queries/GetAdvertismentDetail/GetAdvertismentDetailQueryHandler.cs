using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Common.Application.Abstractions.Caching;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentDetail
{
    internal class GetAdvertismentDetailQueryHandler
        : IRequestHandler<GetAdvertismentDetailQuery, ErrorOr<AdvertismentDetailViewModel>>
    {
        private readonly ICacheService _cacheService;
        private readonly IGetAdvertismentDetailService _advertismentService;
        private readonly IUserContext _userContext;

        public GetAdvertismentDetailQueryHandler(
            IGetAdvertismentDetailService service,
            IUserContext userContext,
            ICacheService cacheService
        )
        {
            _advertismentService = service;
            _userContext = userContext;
            _cacheService = cacheService;
        }

        public async Task<ErrorOr<AdvertismentDetailViewModel>> Handle(
            GetAdvertismentDetailQuery request,
            CancellationToken cancellationToken
        )
        {
            var userId = _userContext.UserId;

            var advertismentData = await _cacheService.GetAsync(
                request.CacheKey,
                token => _advertismentService.GetAsync(request.Id, token),
                request.ExpirationTime,
                cancellationToken
            );

            if (advertismentData is null)
                return AdvertismentErrors.NotFound;

            bool isOwner = userId.HasValue && userId == advertismentData.UserId;

            return !isOwner && advertismentData.Status != AdvertismentStatus.Active
                ? AdvertismentErrors.NotFound
                : advertismentData;
        }
    }
}
