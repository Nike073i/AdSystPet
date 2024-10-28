using FastEndpoints;
using FluentValidation;

namespace AdSyst.Common.Presentation.Endpoints.Shared.Pagination
{
    public class PageRequestValidator : Validator<PageRequest>
    {
        public PageRequestValidator()
        {
            RuleFor(r => r.PageSize).InclusiveBetween(1, 250).When(r => r.PageSize.HasValue);
            RuleFor(r => r.PageNumber).InclusiveBetween(1, 10000).When(r => r.PageNumber.HasValue);
        }
    }
}
