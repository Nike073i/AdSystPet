using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.AdvertismentTypes.Commands.RemoveAdvertismentType;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Editor.Remove
{
    public class RemoveAdvertismentTypeEndpoint : Endpoint<RemoveAdvertismentTypeRequest>
    {
        private readonly ISender _sender;

        public RemoveAdvertismentTypeEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Delete("{id}");
            Group<AdvertismentTypeEditorEndpointGroup>();
            Description(
                b =>
                    b.ProducesProblem(HttpStatusCode.NotFound)
                        .ProducesProblem(HttpStatusCode.UnprocessableEntity)
            );
        }

        public override Task HandleAsync(RemoveAdvertismentTypeRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new RemoveAdvertismentTypeCommand(r.Id))
                .ThenAsync(command => _sender.Send(command, ct))
                .MatchFirst(_ => SendNoContentAsync(), this.HandleFailure);
    }
}
