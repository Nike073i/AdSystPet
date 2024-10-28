using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.AuthService.Application.Users.Queries.GetUserInfoDetails;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Common.Presentation.Extensions;

namespace AdSyst.AuthService.Api.Endpoints.Users.System
{
    public static class GetById
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
                Get("/{userId}");
                Group<UserSystemEndpointGroup>();
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(r => new GetUserInfoDetailsQuery(r.UserId))
                    .ThenAsync(q => _sender.Send(q, ct))
                    .Then(
                        res =>
                            new Response(
                                res.Id,
                                res.Email,
                                res.FirstName,
                                res.LastName,
                                res.UserName,
                                res.IsActive,
                                res.Roles,
                                res.EmailConfirmed,
                                res.Birthday
                            )
                    )
                    .SwitchFirstAsync(res => SendOkAsync(res), this.HandleFailure);
        }

        public class Request
        {
            public string UserId { get; set; } = null!;
        }

        public record Response(
            string Id,
            string Email,
            string FirstName,
            string LastName,
            string UserName,
            bool IsActive,
            string[] Roles,
            bool EmailConfirmed,
            DateTimeOffset Birthday
        );

        public class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(r => r.UserId).MustBeGuid();
            }
        }
    }
}
