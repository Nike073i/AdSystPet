using System.Net;
using ErrorOr;
using FastEndpoints;
using FluentValidation;
using MediatR;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Moderation.Application.Advertisments.Commands.CreateAdvertisment;

namespace AdSyst.Moderation.Api.Features.Advertisments.Moderators
{
    public static class CreateAdvertisment
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
                Group<AdvertismentModeratorEndpointGroup>();
                Description(b => b.ProducesProblem(HttpStatusCode.Conflict));
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(
                        r => new CreateAdvertismentCommand(r.AdvertismentId, r.AdvertismentAuthorId)
                    )
                    .ThenAsync(c => _sender.Send(c, ct))
                    .SwitchFirstAsync(_ => SendNoContentAsync(), this.HandleFailure);
        }

        public class Request
        {
            public Guid AdvertismentId { get; set; }
            public Guid AdvertismentAuthorId { get; set; }
        }

        public class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(r => r.AdvertismentId).NotEmpty();
                RuleFor(r => r.AdvertismentAuthorId).NotEmpty();
            }
        }
    }
}
