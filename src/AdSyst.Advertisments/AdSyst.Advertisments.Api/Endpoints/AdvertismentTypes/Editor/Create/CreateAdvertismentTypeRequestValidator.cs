using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Editor.Create
{
    public class CreateAdvertismentTypeRequestValidator : Validator<CreateAdvertismentTypeRequest>
    {
        public CreateAdvertismentTypeRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
        }
    }
}
