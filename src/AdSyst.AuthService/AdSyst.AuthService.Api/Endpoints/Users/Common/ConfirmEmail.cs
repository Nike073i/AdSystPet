using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.AuthService.ApplicationLayer.Users.Commands.ConfirmEmail;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Common.Presentation.Extensions;

namespace AdSyst.AuthService.Api.Endpoints.Users.Common
{
    public static class ConfirmEmail
    {
        public const string EndpointName = "confirm";

        public class Endpoint : Endpoint<Request>
        {
            private readonly ISender _sender;

            public Endpoint(ISender sender)
            {
                _sender = sender;
            }

            public override void Configure()
            {
                Get("confirm");
                Group<UserCommonEndpointGroup>();
                Description(b =>
                {
                    b.ProducesProblem(HttpStatusCode.NotFound);
                    b.ProducesProblem(HttpStatusCode.UnprocessableEntity);
                });
                Options(b => b.WithName(EndpointName));
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(r => new ConfirmEmailCommand(req.UserId, req.Token))
                    .ThenAsync(c => _sender.Send(c, ct))
                    .SwitchFirstAsync(_ => SendNoContentAsync(), this.HandleFailure);
        }

        public class Request
        {
            public required string UserId { get; set; }
            public required string Token { get; set; }
        }

        public class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(r => r.UserId).MustBeGuid();
            }
        }
    }
}
