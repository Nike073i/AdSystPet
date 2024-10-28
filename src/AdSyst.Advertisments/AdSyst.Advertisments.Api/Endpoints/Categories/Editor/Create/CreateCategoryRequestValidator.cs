using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Editor.Create
{
    public class CreateCategoryRequestValidator : Validator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.ParentCategoryId).NotEmpty().When(c => c.ParentCategoryId.HasValue);
        }
    }
}
