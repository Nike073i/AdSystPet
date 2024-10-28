using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.AuthService.Application.Users.Queries.IsEmailFree;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.AuthService.Api.Endpoints.Users.Common
{
    public static class IsEmailFree
    {
        public class Endpoint : Endpoint<Request, Response>
        {
            private readonly ISender _sender;

            public Endpoint(ISender sender)
            {
                _sender = sender;
            }

            public override void Configure()
            {
                Post("/check/email");
                Group<UserCommonEndpointGroup>();
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(req => new IsEmailFreeQuery(req.Email))
                    .ThenAsync(q => _sender.Send(q, ct))
                    .Then(response => new Response(response))
                    .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
        }

        public class Request
        {
            public required string Email { get; set; }
        }

        public record Response(bool IsFree);
    }
}
