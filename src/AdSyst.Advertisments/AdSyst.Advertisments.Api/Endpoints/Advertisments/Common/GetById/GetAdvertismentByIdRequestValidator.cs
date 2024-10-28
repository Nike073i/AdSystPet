using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.GetById
{
    public class GetAdvertismentByIdRequestValidator : Validator<GetAdvertismentByIdRequest>
    {
        public GetAdvertismentByIdRequestValidator()
        {
            RuleFor(req => req.Id).NotEmpty();
        }
    }
}
