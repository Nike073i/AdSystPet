using ErrorOr;
using MediatR;
using AdSyst.Advertisments.Domain.AdvertismentTypes;

namespace AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeById
{
    internal class GetAdvertismentTypeByIdQueryHandler
        : IRequestHandler<GetAdvertismentTypeByIdQuery, ErrorOr<AdvertismentTypeViewModel>>
    {
        private readonly IGetAdvertismentTypeService _service;

        public GetAdvertismentTypeByIdQueryHandler(IGetAdvertismentTypeService service)
        {
            _service = service;
        }

        public async Task<ErrorOr<AdvertismentTypeViewModel>> Handle(
            GetAdvertismentTypeByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var type = await _service.GetAsync(request.Id, cancellationToken);

            return type is null ? AdvertismentTypeErrors.NotFound : type;
        }
    }
}
