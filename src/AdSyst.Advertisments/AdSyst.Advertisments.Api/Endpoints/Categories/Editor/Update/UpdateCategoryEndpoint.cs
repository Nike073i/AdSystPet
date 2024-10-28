using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Api.Endpoints.Categories.Common;
using AdSyst.Advertisments.Application.Categories.Commands.UpdateCategory;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Editor.Update
{
    public class UpdateCategoryEndpoint : Endpoint<UpdateCategoryRequest, Guid>
    {
        private readonly ISender _sender;

        public UpdateCategoryEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Patch("{id}");
            Group<CategoryEditorEndpointGroup>();
            Description(b => b.ProducesProblem(HttpStatusCode.NotFound));
        }

        public override Task HandleAsync(UpdateCategoryRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new UpdateCategoryCommand(r.Id, r.Name))
                .ThenAsync(command => _sender.Send(command, ct))
                .SwitchFirstAsync(id => SendOkAsync(id), this.HandleFailure);
    }
}
