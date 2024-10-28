using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.Restore
{
    public class RestoreAdvertismentRequestValidator : Validator<RestoreAdvertismentRequest>
    {
        public RestoreAdvertismentRequestValidator()
        {
            RuleFor(req => req.Id).NotEmpty();
        }
    }
}
