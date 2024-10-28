using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.AdvertismentTypes.Commands.CreateAdvertismentType;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Editor.Create
{
    public class CreateAdvertismentTypeEndpoint : Endpoint<CreateAdvertismentTypeRequest>
    {
        private readonly ISender _sender;

        public CreateAdvertismentTypeEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Post("/");
            Group<AdvertismentTypeEditorEndpointGroup>();
        }

        public override Task HandleAsync(CreateAdvertismentTypeRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new CreateAdvertismentTypeCommand(r.Name))
                .ThenAsync(command => _sender.Send(command, ct))
                .SwitchFirstAsync(id => SendOkAsync(id), this.HandleFailure);
    }
}
