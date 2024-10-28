using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Editor.Remove
{
    public class RemoveCategoryRequestValidator : Validator<RemoveCategoryRequest>
    {
        public RemoveCategoryRequestValidator()
        {
            RuleFor(req => req.Id).NotEmpty();
        }
    }
}
