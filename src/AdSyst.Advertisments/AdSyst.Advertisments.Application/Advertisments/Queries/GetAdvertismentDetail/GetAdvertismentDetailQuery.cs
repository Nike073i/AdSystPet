using ErrorOr;
using MediatR;
using AdSyst.Common.Application.Abstractions.Caching;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentDetail
{
    public record GetAdvertismentDetailQuery(Guid Id)
        : IRequest<ErrorOr<AdvertismentDetailViewModel>>,
            ICachedQuery
    {
        public string CacheKey => $"Advertisments-{Id}";

        public TimeSpan? ExpirationTime => null;
    }
}
