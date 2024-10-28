using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Editor.ChangeParent
{
    public class ChangeParentCategoryRequestValidator
        : AbstractValidator<ChangeParentCategoryRequest>
    {
        public ChangeParentCategoryRequestValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.ParentCategoryId).NotEmpty().When(c => c.ParentCategoryId.HasValue);
        }
    }
}
