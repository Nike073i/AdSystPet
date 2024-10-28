using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.AuthService.Application.Users.Queries.IsUsernameFree;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.AuthService.Api.Endpoints.Users.Common
{
    public static class IsUsernameFree
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
                Post("/check/username");
                Group<UserCommonEndpointGroup>();
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(r => new IsUsernameFreeQuery(r.Username))
                    .ThenAsync(q => _sender.Send(q, ct))
                    .Then(result => new Response(result))
                    .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
        }

        public class Request
        {
            public required string Username { get; set; }
        }

        public record Response(bool IsFree);
    }
}
