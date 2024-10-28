using ErrorOr;
using FastEndpoints;
using FluentValidation;
using MediatR;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Moderation.Application.CorrectionNotes.Queries.GetAdvertismentCorrectionNotesForModeration;

namespace AdSyst.Moderation.Api.Features.Advertisments.Moderators
{
    public static class GetAdvertismentCorrectionNotes
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
                Get("{advertismentId}");
                Group<AdvertismentModeratorEndpointGroup>();
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(r => new GetAdvertismentCorrectionNotesQuery(r.AdvertismentId))
                    .ThenAsync(q => _sender.Send(q, ct))
                    .Then(
                        res =>
                            new Response
                            {
                                AdvertismentId = res.AdvertisementId,
                                CorrectionNotes = res.CorrectionNotes
                            }
                    )
                    .SwitchFirstAsync(res => SendOkAsync(res), this.HandleFailure);
        }

        public class Request
        {
            public Guid AdvertismentId { get; set; }
        }

        public class Response
        {
            public required Guid AdvertismentId { get; set; }
            public required IEnumerable<CorrectionNoteDto> CorrectionNotes { get; set; }
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
