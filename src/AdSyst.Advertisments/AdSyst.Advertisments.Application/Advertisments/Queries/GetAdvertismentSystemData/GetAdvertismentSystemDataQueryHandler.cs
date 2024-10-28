using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.Advertisments;

namespace AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentSystemData
{
    internal class GetAdvertismentSystemDataQueryHandler
        : IRequestHandler<GetAdvertismentSystemDataQuery, ErrorOr<AdvertismentSystemViewModel>>
    {
        private readonly IGetAdvertismentSystemDataService _service;

        public GetAdvertismentSystemDataQueryHandler(IGetAdvertismentSystemDataService service)
        {
            _service = service;
        }

        public async Task<ErrorOr<AdvertismentSystemViewModel>> Handle(
            GetAdvertismentSystemDataQuery request,
            CancellationToken cancellationToken
        )
        {
            var data = await _service.GetAsync(request.AdvertismentId, cancellationToken);
            return data is null ? AdvertismentErrors.NotFound : data;
        }
    }
}
