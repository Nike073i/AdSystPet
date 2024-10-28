using System.Net;
using ErrorOr;
using FastEndpoints;
using FluentValidation;
using MediatR;
using AdSyst.AuthService.Application.Users.Commands.CreateUser;
using AdSyst.AuthService.Domain.Enums;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.AuthService.Api.Endpoints.Users.System
{
    public static class CreateUser
    {
        public class Endpoint : Endpoint<Request>
        {
            private readonly ISender _sender;

            public Endpoint(ISender sender)
            {
                _sender = sender;
            }

            public override void Configure()
            {
                Post("/");
                Group<UserSystemEndpointGroup>();
                Description(d =>
                {
                    d.ProducesProblem(HttpStatusCode.Forbidden);
                    d.ProducesProblem(HttpStatusCode.Unauthorized);
                });
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(
                        r =>
                            new CreateUserCommand(
                                r.Username,
                                r.FirstName,
                                r.LastName,
                                r.Password,
                                r.Email,
                                r.Roles,
                                r.Birthday
                            )
                    )
                    .ThenAsync(c => _sender.Send(c, ct))
                    .SwitchFirstAsync(_ => SendNoContentAsync(), this.HandleFailure);
        }

        public class Request
        {
            public required string Username { get; set; }
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
            public required string Password { get; set; }
            public required string ConfirmPassword { get; set; }
            public required string Email { get; set; }
            public required Role[] Roles { get; set; }
            public required DateTimeOffset Birthday { get; set; }
        }

        public class Validator : Validator<Request>
        {
            public const int MinimumAge = 14;

            public Validator()
            {
                RuleFor(dto => dto.Username).NotEmpty().MinimumLength(4).MaximumLength(75);
                RuleFor(dto => dto.FirstName).NotEmpty().MaximumLength(50);
                RuleFor(dto => dto.LastName).NotEmpty().MaximumLength(50);
                RuleFor(dto => dto.Password)
                    .NotEmpty()
                    .MinimumLength(8)
                    .MaximumLength(75)
                    .Must(ContainsBigLetterAndDigit)
                    .WithMessage("Пароль должен содержать хотя бы одну заглавную букву и цифру");
                RuleFor(dto => dto.ConfirmPassword)
                    .NotEmpty()
                    .Equal(dto => dto.Password)
                    .WithMessage("Пароль и подтверждение пароля должны совпадать");
                RuleFor(dto => dto.Email).NotEmpty().MaximumLength(75).EmailAddress();
                RuleFor(dto => dto.Birthday)
                    .NotEmpty()
                    .Must(BeOlderThanMinimumAge)
                    .WithMessage($"Пользователь должен быть старше {MinimumAge} лет");
                RuleFor(dto => dto.Roles).NotEmpty();
                RuleFor(dto => dto.Roles)
                    .Must(BeDistinctRoles)
                    .WithMessage("Роли не должны повторяться");
            }

            private bool ContainsBigLetterAndDigit(string password) =>
                password.Any(char.IsUpper) && password.Any(char.IsDigit);

            private bool BeDistinctRoles(Role[] roles) =>
                roles?.Length == roles?.Distinct().Count();

            private bool BeOlderThanMinimumAge(DateTimeOffset birthDate)
            {
                var today = DateTimeOffset.Now;
                int userAge = today.Year - birthDate.Year;

                // Если ДР в этом году еще не наступил
                if (birthDate.DayOfYear > today.AddYears(-userAge).DayOfYear)
                    userAge--;

                return userAge >= MinimumAge;
            }
        }
    }
}