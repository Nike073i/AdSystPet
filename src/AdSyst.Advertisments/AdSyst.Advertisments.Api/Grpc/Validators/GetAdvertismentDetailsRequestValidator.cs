using FluentValidation;

namespace AdSyst.Advertisments.Api.Grpc.Validators
{
    public class GetAdvertismentDetailsRequestValidator
        : AbstractValidator<GetAdvertismentDetailsRequest>
    {
        public GetAdvertismentDetailsRequestValidator()
        {
            RuleFor(r => r.AdvertismentId).NotEmpty();
        }
    }
}
