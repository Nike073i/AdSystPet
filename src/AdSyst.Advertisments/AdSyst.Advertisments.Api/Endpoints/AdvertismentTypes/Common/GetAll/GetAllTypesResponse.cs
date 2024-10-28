using AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeList;

namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Common.GetAll
{
    public record GetAllTypesResponse(IEnumerable<AdvertismentTypeViewModel> Types);
}
