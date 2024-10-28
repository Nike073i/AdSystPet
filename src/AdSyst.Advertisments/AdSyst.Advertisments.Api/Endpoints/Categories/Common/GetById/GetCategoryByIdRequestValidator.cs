using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Common.GetById
{
    public class GetCategoryByIdRequestValidator : Validator<GetCategoryByIdRequest>
    {
        public GetCategoryByIdRequestValidator()
        {
            RuleFor(r => r.Id).NotEmpty();
        }
    }
}
