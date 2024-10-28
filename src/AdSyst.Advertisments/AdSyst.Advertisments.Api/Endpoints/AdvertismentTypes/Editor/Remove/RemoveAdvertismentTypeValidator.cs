using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Editor.Remove
{
    public class RemoveAdvertismentTypeValidator : Validator<RemoveAdvertismentTypeRequest>
    {
        public RemoveAdvertismentTypeValidator()
        {
            RuleFor(req => req.Id).NotEmpty();
        }
    }
}
