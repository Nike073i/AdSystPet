using System.Net;
using ErrorOr;
using FastEndpoints;
using FluentValidation;
using MediatR;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Common.Presentation.Extensions;
using AdSyst.Moderation.Application.CorrectionNotes.Commands.DisableCorrectionNote;

namespace AdSyst.Moderation.Api.Features.CorrectionNotes.Moderators
{
    public static class DisableCorrectionNote
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
                Delete("{advertismentId}/{noteId}");
                Group<CorrectionNoteModeratorEndpointGroup>();
                Description(b =>
                {
                    b.ProducesProblem(HttpStatusCode.NotFound);
                    b.ProducesProblem(HttpStatusCode.UnprocessableEntity);
                });
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(req => new DisableCorrectionNoteCommand(req.AdvertismentId, req.NoteId))
                    .ThenAsync(c => _sender.Send(c, ct))
                    .SwitchFirstAsync(_ => SendNoContentAsync(), this.HandleFailure);
        }

        public class Request
        {
            public Guid AdvertismentId { get; set; }
            public string NoteId { get; set; } = null!;
        }

        public class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(r => r.AdvertismentId).NotEmpty();
                RuleFor(r => r.NoteId).MustBeMongoId();
            }
        }
    }
}
