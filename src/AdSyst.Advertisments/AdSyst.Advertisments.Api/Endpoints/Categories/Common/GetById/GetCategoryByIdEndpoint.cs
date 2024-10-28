using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.Categories.Queries.GetCategoryById;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Common.GetById
{
    public class GetCategoryByIdEndpoint : Endpoint<GetCategoryByIdRequest, GetCategoryByIdResponse>
    {
        private readonly ISender _sender;

        public GetCategoryByIdEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Get("/{id}");
            Group<CategoryCommonEndpointGroup>();
            Description(b => b.ProducesProblem(HttpStatusCode.NotFound));
        }

        public override Task HandleAsync(GetCategoryByIdRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new GetCategoryByIdQuery(req.Id))
                .ThenAsync(query => _sender.Send(query, ct))
                .Then(
                    result =>
                        new GetCategoryByIdResponse(
                            result.Id,
                            result.Name,
                            result.Children.Select(c => new CategoryResponse(c.Id, c.Name))
                        )
                )
                .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
    }
}
