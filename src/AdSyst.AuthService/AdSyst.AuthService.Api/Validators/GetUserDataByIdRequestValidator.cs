using FluentValidation;
using AdSyst.AuthService.Api.Grpc;

namespace AdSyst.AuthService.Api.Validators
{
    public class GetUserDataByIdRequestValidator : AbstractValidator<GetUserDataByIdRequest>
    {
        public GetUserDataByIdRequestValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty()
                .Must(BeCorrectGuid)
                .WithMessage("Идентификатор пользователя должен быть в виде UUID значения");
        }

        public bool BeCorrectGuid(string userId) => Guid.TryParse(userId, out var _);
    }
}
