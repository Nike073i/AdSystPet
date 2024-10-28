using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.AuthService.Application.Users.Queries.GetPersonalData;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.AuthService.Api.Endpoints.Users.Client
{
    public static class GetPersonalData
    {
        public class Endpoint : EndpointWithoutRequest<Response>
        {
            private readonly ISender _sender;

            public Endpoint(ISender sender)
            {
                _sender = sender;
            }

            public override void Configure()
            {
                Get("/personal-data");
                Group<UserClientEndpointGroup>();
                Description(b =>
                {
                    b.ProducesProblem(HttpStatusCode.Unauthorized);
                    b.ProducesProblem(HttpStatusCode.NotFound);
                });
            }

            public override Task HandleAsync(CancellationToken ct) =>
                ErrorOrFactory
                    .From(new GetPersonalDataQuery())
                    .ThenAsync(q => _sender.Send(q, ct))
                    .Then(
                        res =>
                            new Response(
                                res.PersonId,
                                res.FirstName,
                                res.LastName,
                                res.Username,
                                res.Email,
                                res.Birthday
                            )
                    )
                    .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
        }

        public record Response(
            string PersonId,
            string FirstName,
            string LastName,
            string Username,
            string Email,
            DateTimeOffset Birthday
        );
    }
}
