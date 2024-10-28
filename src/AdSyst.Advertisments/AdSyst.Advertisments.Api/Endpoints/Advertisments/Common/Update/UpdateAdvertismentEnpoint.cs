using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.Advertisments.Commands.UpdateAdvertisment;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.Update
{
    public class UpdateAdvertismentEndpoint : Endpoint<UpdateAdvertismentRequest, Guid>
    {
        private readonly ISender _sender;

        public UpdateAdvertismentEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Patch("{id}");
            Group<AdvertismentCommonEndpointGroup>();
            Roles(RoleNames.Client);
            Description(b => b.ProducesProblem(HttpStatusCode.NotFound));
        }

        public override Task HandleAsync(UpdateAdvertismentRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(
                    r =>
                        new UpdateAdvertismentCommand(r.Id)
                        {
                            Title = r.Title,
                            Description = r.Description,
                            Price = r.Price,
                            AdvertismentTypeId = r.AdvertismentTypeId,
                            CategoryId = r.CategoryId,
                            ImageIds = r.ImageIds
                        }
                )
                .ThenAsync(command => _sender.Send(command, ct))
                .SwitchFirstAsync(id => SendOkAsync(id), this.HandleFailure);
    }
}
