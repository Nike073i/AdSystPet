using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.Advertisments.Commands.CreateAdvertisment;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.Create
{
    public class CreateAdvertismentEndpoint : Endpoint<CreateAdvertismentRequest, Guid>
    {
        private readonly ISender _sender;

        public CreateAdvertismentEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Post("/");
            Group<AdvertismentCommonEndpointGroup>();
            Roles(RoleNames.Client);
            Description(b => b.ProducesProblem(HttpStatusCode.NotFound));
        }

        public override Task HandleAsync(CreateAdvertismentRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(
                    r =>
                        new CreateAdvertismentCommand(
                            r.Title,
                            r.Description,
                            r.CategoryId,
                            r.AdvertismentTypeId,
                            r.Price,
                            r.ImageIds
                        )
                )
                .ThenAsync(command => _sender.Send(command, ct))
                .SwitchFirstAsync(id => SendOkAsync(id), this.HandleFailure);
    }
}
