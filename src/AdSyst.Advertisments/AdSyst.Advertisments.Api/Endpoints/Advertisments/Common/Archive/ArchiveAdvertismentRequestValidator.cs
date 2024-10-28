using FastEndpoints;
using FluentValidation;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.Archive
{
    public class ArchiveAdvertismentRequestValidator : Validator<ArchiveAdvertismentRequest>
    {
        public ArchiveAdvertismentRequestValidator()
        {
            RuleFor(req => req.Id).NotEmpty();
        }
    }
}
