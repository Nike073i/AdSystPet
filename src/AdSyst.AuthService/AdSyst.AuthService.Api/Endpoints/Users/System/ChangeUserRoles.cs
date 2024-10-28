using System.Net;
using ErrorOr;
using FastEndpoints;
using FluentValidation;
using MediatR;
using AdSyst.AuthService.ApplicationLayer.Users.Commands.ChangeRoles;
using AdSyst.AuthService.Domain.Enums;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.AuthService.Api.Endpoints.Users.System
{
    public static class ChangeUserRoles
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
                Put("/roles");
                Group<UserSystemEndpointGroup>();
                Description(d => d.ProducesProblem(HttpStatusCode.NotFound));
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(r => new ChangeRolesCommand(r.UserId, r.Roles))
                    .ThenAsync(c => _sender.Send(c, ct))
                    .SwitchFirstAsync(_ => SendNoContentAsync(), this.HandleFailure);
        }

        public class Request
        {
            public required string UserId { get; set; }
            public required Role[] Roles { get; set; }
        }

        public class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(dto => dto.Roles).NotEmpty();
                RuleFor(dto => dto.Roles)
                    .Must(BeDistinctRoles)
                    .WithMessage("Роли не должны повторяться");
            }

            private bool BeDistinctRoles(Role[] roles) =>
                roles?.Length == roles?.Distinct().Count();
        }
    }
}
