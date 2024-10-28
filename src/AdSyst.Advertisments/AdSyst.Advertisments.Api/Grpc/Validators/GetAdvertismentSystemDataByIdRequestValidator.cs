using FluentValidation;

namespace AdSyst.Advertisments.Api.Grpc.Validators
{
    public class GetAdvertismentSystemDataByIdRequestValidator
        : AbstractValidator<GetAdvertismentSystemDataByIdRequest>
    {
        public GetAdvertismentSystemDataByIdRequestValidator()
        {
            RuleFor(r => r.AdvertismentId).NotEmpty();
        }
    }
}
