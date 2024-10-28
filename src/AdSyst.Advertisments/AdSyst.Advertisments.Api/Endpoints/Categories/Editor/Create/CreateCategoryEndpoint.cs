using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Api.Endpoints.Categories.Common;
using AdSyst.Advertisments.Application.Categories.Commands.CreateCategory;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Editor.Create
{
    public class CreateCategoryEndpoint : Endpoint<CreateCategoryRequest, Guid>
    {
        private readonly ISender _sender;

        public CreateCategoryEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Post("/");
            Group<CategoryEditorEndpointGroup>();
            Description(b => b.ProducesProblem(HttpStatusCode.NotFound));
        }

        public override Task HandleAsync(CreateCategoryRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new CreateCategoryCommand(r.Name, r.ParentCategoryId))
                .ThenAsync(command => _sender.Send(command, ct))
                .SwitchFirstAsync(id => SendOkAsync(id), this.HandleFailure);
    }
}
