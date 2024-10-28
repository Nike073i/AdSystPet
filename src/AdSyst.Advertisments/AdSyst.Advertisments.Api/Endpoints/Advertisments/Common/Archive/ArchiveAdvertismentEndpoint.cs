using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.Advertisments.Commands.ArchiveAdvertisment;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.Archive
{
    public class ArchiveAdvertismentEndpoint
        : Endpoint<ArchiveAdvertismentRequest, AdvertismentStatus>
    {
        private readonly ISender _sender;

        public ArchiveAdvertismentEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Put("/{id}/archive");
            Group<AdvertismentCommonEndpointGroup>();
            Roles(RoleNames.Client);
            Description(b =>
            {
                b.ProducesProblem(HttpStatusCode.NotFound);
                b.ProducesProblem(HttpStatusCode.UnprocessableEntity);
            });
        }

        public override Task HandleAsync(ArchiveAdvertismentRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new ArchiveAdvertismentCommand(req.Id))
                .ThenAsync(command => _sender.Send(command, ct))
                .MatchFirst(status => SendOkAsync(status), this.HandleFailure);
    }
}
