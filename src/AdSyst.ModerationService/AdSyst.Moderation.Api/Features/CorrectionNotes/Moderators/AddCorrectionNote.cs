using System.Net;
using ErrorOr;
using FastEndpoints;
using FluentValidation;
using MediatR;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Moderation.Application.CorrectionNotes.Commands.AddCorrectionNote;

namespace AdSyst.Moderation.Api.Features.CorrectionNotes.Moderators
{
    public static class AddCorrectionNote
    {
        public class Endpoint : Endpoint<Request, Response>
        {
            private readonly ISender _sender;
            private readonly IUserContext _userContext;

            public Endpoint(ISender sender, IUserContext userContext)
            {
                _sender = sender;
                _userContext = userContext;
            }

            public override void Configure()
            {
                Post("/");
                Group<CorrectionNoteModeratorEndpointGroup>();
                Description(b => b.ProducesProblem(HttpStatusCode.NotFound));
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(
                        req =>
                            new AddCorrectionNoteCommand(
                                req.Title,
                                req.Description,
                                _userContext.UserId!.Value,
                                req.AdvertismentId
                            )
                    )
                    .ThenAsync(c => _sender.Send(c, ct))
                    .Then(noteId => new Response { NoteId = noteId })
                    .SwitchFirstAsync(res => SendOkAsync(res), this.HandleFailure);
        }

        public class Request
        {
            public Guid AdvertismentId { get; set; }
            public string Title { get; set; } = null!;
            public string? Description { get; set; }
        }

        public class Response
        {
            public required string NoteId { get; set; }
        }

        public class Validator : Validator<Request>
        {
            public Validator()
            {
                RuleFor(dto => dto.Title).NotEmpty();
                RuleFor(r => r.AdvertismentId).NotEmpty();
            }
        }
    }
}
