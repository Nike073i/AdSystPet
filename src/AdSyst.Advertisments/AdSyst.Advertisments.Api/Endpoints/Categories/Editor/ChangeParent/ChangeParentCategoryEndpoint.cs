using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Api.Endpoints.Categories.Common;
using AdSyst.Advertisments.Application.Categories.Commands.ChangeParent;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Editor.ChangeParent
{
    public class ChangeParentCategoryEndpoint : Endpoint<ChangeParentCategoryRequest>
    {
        private readonly ISender _sender;

        public ChangeParentCategoryEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Put("{id}/parent");
            Group<CategoryEditorEndpointGroup>();
            Description(b => b.ProducesProblem(HttpStatusCode.NotFound));
        }

        public override Task HandleAsync(ChangeParentCategoryRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new ChangeParentCategoryCommand(r.Id, r.ParentCategoryId))
                .ThenAsync(command => _sender.Send(command, ct))
                .SwitchFirstAsync(_ => SendNoContentAsync(), this.HandleFailure);
    }
}
