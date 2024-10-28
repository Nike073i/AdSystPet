using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.AdvertismentTypes.Commands.UpdateAdvertismentType;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Editor.Update
{
    public class UpdateAdvertismentTypeEndpoint : Endpoint<UpdateAdvertismentTypeRequest, Guid>
    {
        private readonly ISender _sender;

        public UpdateAdvertismentTypeEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Patch("{id}");
            Group<AdvertismentTypeEditorEndpointGroup>();
            Description(b => b.ProducesProblem(HttpStatusCode.NotFound));
        }

        public override Task HandleAsync(UpdateAdvertismentTypeRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new UpdateAdvertismentTypeCommand(r.Id, r.Name))
                .ThenAsync(command => _sender.Send(command, ct))
                .MatchFirst(id => SendOkAsync(id), this.HandleFailure);
    }
}
