using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.Categories.Queries.GetAllCategoriesQuery;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.Categories.Common.GetAll
{
    public class GetAllCategoriesEnpdoint : EndpointWithoutRequest<GetAllCategoriesResponse>
    {
        private readonly ISender _sender;

        public GetAllCategoriesEnpdoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Get("/");
            Group<CategoryCommonEndpointGroup>();
        }

        public override Task HandleAsync(CancellationToken ct) =>
            ErrorOrFactory
                .From(new GetCategoryListQuery())
                .ThenAsync(query => _sender.Send(query, ct))
                .Then(result => new GetAllCategoriesResponse(result.Select(FromEntity)))
                .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);

        private CategoryResponse FromEntity(CategoryViewModel model)
        {
            var root = new CategoryResponse(model.Id, model.Name);

            var stack = new Stack<(CategoryResponse, CategoryViewModel)>();
            stack.Push((root, model));
            while (stack.Any())
            {
                var (resNode, modelNode) = stack.Pop();
                foreach (var child in modelNode.Children)
                {
                    var node = new CategoryResponse(child.Id, child.Name);
                    resNode.Children.Add(node);
                    stack.Push((node, child));
                }
            }
            return root;
        }
    }
}
