using ErrorOr;
using MediatR;

namespace AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeList
{
    internal class GetAdvertismentTypeListQueryHandler
        : IRequestHandler<
            GetAdvertismentTypeListQuery,
            ErrorOr<IReadOnlyList<AdvertismentTypeViewModel>>
        >
    {
        private readonly IGetAdvertismentTypeListService _service;

        public GetAdvertismentTypeListQueryHandler(IGetAdvertismentTypeListService service)
        {
            _service = service;
        }

        public async Task<ErrorOr<IReadOnlyList<AdvertismentTypeViewModel>>> Handle(
            GetAdvertismentTypeListQuery request,
            CancellationToken cancellationToken
        )
        {
            var types = await _service.GetAsync(cancellationToken);
            return types.ToErrorOr();
        }
    }
}
