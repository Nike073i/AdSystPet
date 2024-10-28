using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.Update
{
    public class UpdateAdvertismentRequestValidator : Validator<UpdateAdvertismentRequest>
    {
        private const int _maxImageCount = 5;

        public UpdateAdvertismentRequestValidator()
        {
            RuleFor(d => d.Id).NotEmpty();
            RuleFor(d => d)
                .Must(BeValidUpdateDto)
                .WithMessage("Хотя бы одно из полей для обновления должно иметь значение");
            RuleFor(d => d.Title).NotEmpty().MaximumLength(50).When(dto => dto.Title != null);
            RuleFor(d => d.Description)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(1000)
                .When(dto => dto.Description != null);
            RuleFor(d => d.CategoryId).NotEmpty().When(dto => dto.CategoryId.HasValue);
            RuleFor(d => d.AdvertismentTypeId)
                .NotEmpty()
                .When(dto => dto.AdvertismentTypeId.HasValue);
            RuleFor(d => d.Price).GreaterThanOrEqualTo(0).When(dto => dto.Price.HasValue);
            RuleFor(d => d.ImageIds)
                .Must(ids => BeLimitedArrayWithUniqueImageIds(ids!, _maxImageCount))
                .When(d => d.ImageIds != null)
                .WithMessage(
                    $"Изображения не должны повторяться и их количество не может превышать {_maxImageCount}"
                );
        }

        private static bool BeValidUpdateDto(UpdateAdvertismentRequest req) =>
            !string.IsNullOrEmpty(req.Title)
            || !string.IsNullOrEmpty(req.Description)
            || req.Price.HasValue
            || req.CategoryId.HasValue
            || req.AdvertismentTypeId.HasValue
            || req.ImageIds != null;

        public bool BeLimitedArrayWithUniqueImageIds(Guid[] ids, int maxCount) =>
            ids.Length <= maxCount && ids.Distinct().Count() == ids.Length;
    }
}
