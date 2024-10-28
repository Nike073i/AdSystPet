using MediatR;
using AdSyst.Orders.SyncDataServices.Advertisments.Interfaces;

namespace AdSyst.Orders.Application.Advertisments.Queries.GetAdvertismentSystemData
{
    public class GetAdvertismentSystemDataQueryHandler
        : IRequestHandler<GetAdvertismentSystemDataQuery, GetAdvertismentSystemDataQueryResponse>
    {
        private readonly IAdvertismentServiceClient _advertismentService;

        public GetAdvertismentSystemDataQueryHandler(IAdvertismentServiceClient advertismentService)
        {
            _advertismentService = advertismentService;
        }

        public async Task<GetAdvertismentSystemDataQueryResponse> Handle(
            GetAdvertismentSystemDataQuery request,
            CancellationToken cancellationToken
        )
        {
            var data = await _advertismentService.GetAdvertismentSystemDataByIdAsync(
                request.AdvertismentId,
                cancellationToken
            );
            return new(data);
        }
    }
}
