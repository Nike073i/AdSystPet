using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Common.GetById
{
    public class GetTypeByIdRequestValidator : Validator<GetTypeByIdRequest>
    {
        public GetTypeByIdRequestValidator()
        {
            RuleFor(r => r.Id).NotEmpty();
        }
    }
}
