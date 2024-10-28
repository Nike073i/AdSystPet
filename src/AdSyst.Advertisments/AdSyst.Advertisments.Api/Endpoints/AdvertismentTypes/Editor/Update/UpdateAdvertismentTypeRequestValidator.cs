using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Editor.Update
{
    public class UpdateAdvertismentTypeRequestValidator : Validator<UpdateAdvertismentTypeRequest>
    {
        public UpdateAdvertismentTypeRequestValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
