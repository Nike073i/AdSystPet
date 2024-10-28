using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.AuthService.Application.Users.Queries.GetUserData;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Common.Presentation.Extensions;

namespace AdSyst.AuthService.Api.Endpoints.Users.Client
{
    public static class GetUserData
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
                Get("{userId}");
                Group<UserClientEndpointGroup>();
                Description(b =>
                {
                    b.ProducesProblem(HttpStatusCode.Unauthorized);
                    b.ProducesProblem(HttpStatusCode.NotFound);
                });
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(req => new GetUserDataQuery(req.UserId))
                    .ThenAsync(q => _sender.Send(q, ct))
                    .Then(res => new Response(res.FirstName, res.LastName, res.Email))
                    .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
        }

        public class Request
        {
            public required string UserId { get; set; }
        }

        public record Response(string FirstName, string LastName, string Email);

        public class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(r => r.UserId).MustBeGuid();
            }
        }
    }
}
