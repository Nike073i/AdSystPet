using System.Net;
using ErrorOr;
using FastEndpoints;
using FluentValidation;
using MediatR;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Moderation.Application.CorrectionNotes.Queries.GetAdvertismentCorrectionNotes;

namespace AdSyst.Moderation.Api.Features.Advertisments.Clients
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
                Group<CorrectionNoteClientEndpointGroup>();
                Description(b => b.ProducesProblem(HttpStatusCode.NotFound));
            }

            public override Task HandleAsync(Request req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(r => new GetAdvertismentCorrectionNotesQuery(r.AdvertismentId))
                    .ThenAsync(q => _sender.Send(q, ct))
                    .Then(
                        res =>
                            new Response
                            {
                                AdvertisementId = res.AdvertisementId,
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
            public Guid AdvertisementId { get; set; }
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
