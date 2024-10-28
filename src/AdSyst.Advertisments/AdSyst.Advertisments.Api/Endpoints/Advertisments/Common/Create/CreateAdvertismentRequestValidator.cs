using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.Create
{
    public class CreateAdvertismentRequestValidator : Validator<CreateAdvertismentRequest>
    {
        private const int _maxImageCount = 5;

        public CreateAdvertismentRequestValidator()
        {
            RuleFor(d => d.Title).NotEmpty().MaximumLength(50);
            RuleFor(d => d.Description).NotEmpty().MinimumLength(10).MaximumLength(1000);
            RuleFor(d => d.CategoryId).NotEmpty();
            RuleFor(d => d.AdvertismentTypeId).NotEmpty();
            RuleFor(d => d.Price).GreaterThanOrEqualTo(0);
            RuleFor(d => d.ImageIds)
                .Must(ids => BeLimitedArrayWithUniqueImageIds(ids!, _maxImageCount))
                .When(d => d.ImageIds != null)
                .WithMessage(
                    $"Изображения не должны повторяться и их количество не может превышать {_maxImageCount}"
                );
        }

        public bool BeLimitedArrayWithUniqueImageIds(Guid[] ids, int maxCount) =>
            ids.Length <= maxCount && ids.Distinct().Count() == ids.Length;
    }
}
