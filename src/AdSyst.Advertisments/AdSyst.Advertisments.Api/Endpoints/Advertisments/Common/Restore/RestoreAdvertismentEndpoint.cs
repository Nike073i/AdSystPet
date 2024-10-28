using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.Advertisments.Commands.RestoreAdvertisment;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.Restore
{
    public class RestoreAdvertismentEndpoint
        : Endpoint<RestoreAdvertismentRequest, AdvertismentStatus>
    {
        private readonly ISender _sender;

        public RestoreAdvertismentEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Put("/{id}/restore");
            Group<AdvertismentCommonEndpointGroup>();
            Roles(RoleNames.Client);
            Description(b =>
            {
                b.ProducesProblem(HttpStatusCode.NotFound);
                b.ProducesProblem(HttpStatusCode.UnprocessableEntity);
            });
        }

        public override Task HandleAsync(RestoreAdvertismentRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new RestoreAdvertismentCommand(r.Id))
                .ThenAsync(command => _sender.Send(command, ct))
                .SwitchFirstAsync(status => SendOkAsync(status), this.HandleFailure);
    }
}
