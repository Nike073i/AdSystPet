using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Api.Endpoints.Categories.Common;
using AdSyst.Advertisments.Application.Categories.Commands.RemoveCategory;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Editor.Remove
{
    public class RemoveCategoryEndpoint : Endpoint<RemoveCategoryRequest>
    {
        private readonly ISender _sender;

        public RemoveCategoryEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Delete("{id}");
            Group<CategoryEditorEndpointGroup>();
            Description(b =>
            {
                b.ProducesProblem(HttpStatusCode.NotFound);
                b.ProducesProblem(HttpStatusCode.UnprocessableEntity);
            });
        }

        public override Task HandleAsync(RemoveCategoryRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new RemoveCategoryCommand(r.Id))
                .ThenAsync(command => _sender.Send(command, ct))
                .SwitchFirstAsync(_ => SendNoContentAsync(), this.HandleFailure);
    }
}
