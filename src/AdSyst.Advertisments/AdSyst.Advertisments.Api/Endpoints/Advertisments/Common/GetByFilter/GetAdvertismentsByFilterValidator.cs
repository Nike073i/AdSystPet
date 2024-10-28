using FastEndpoints;
using FluentValidation;
using AdSyst.Common.Presentation.Endpoints.Shared.Pagination;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.GetByFilter
{
    public class GetAdvertismentsByFilterValidator : Validator<GetAdvertismentsByFilterRequest>
    {
        private readonly DateTimeOffset _minimumPeriodStartDate =
            new(2020, 1, 1, 0, 0, 0, new(0, 0, 0));

        public GetAdvertismentsByFilterValidator()
        {
            Include(new PageRequestValidator());
            RuleFor(dto => dto.Search)
                .MaximumLength(100)
                .When(dto => !string.IsNullOrEmpty(dto.Search));
            RuleFor(dto => dto.CategoryId).NotEmpty().When(dto => dto.CategoryId.HasValue);
            RuleFor(dto => dto.PeriodStart)
                .GreaterThanOrEqualTo(_minimumPeriodStartDate)
                .When(dto => dto.PeriodStart.HasValue);
            RuleFor(dto => dto.PeriodEnd)
                .LessThanOrEqualTo(DateTimeOffset.UtcNow)
                .When(dto => dto.PeriodEnd.HasValue);
        }
    }
}
