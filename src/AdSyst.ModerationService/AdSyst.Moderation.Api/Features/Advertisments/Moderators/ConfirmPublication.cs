using System.Net;
using ErrorOr;
using FastEndpoints;
using FluentValidation;
using MediatR;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Moderation.Application.Advertisments.Commands.ConfirmPublication;

namespace AdSyst.Moderation.Api.Features.Advertisments.Moderators
{
    public static class ConfirmPublication
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
                Put("{advertismentId}");
                Group<AdvertismentModeratorEndpointGroup>();
                Description(b => b.ProducesProblem(HttpStatusCode.UnprocessableEntity));
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(req => new ConfirmPublicationCommand(req.AdvertismentId))
                    .ThenAsync(c => _sender.Send(c, ct))
                    .SwitchFirstAsync(_ => SendOkAsync(), this.HandleFailure);
        }

        public class Request
        {
            public Guid AdvertismentId { get; set; }
        }

        public class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(r => r.AdvertismentId).NotEmpty();
            }
        }
    }
}
