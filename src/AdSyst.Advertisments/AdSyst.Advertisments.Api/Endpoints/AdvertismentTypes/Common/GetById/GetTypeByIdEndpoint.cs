using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Api.Endpoints.Categories.Common;
using AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeById;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Common.GetById
{
    public class GetTypeByIdEndpoint : Endpoint<GetTypeByIdRequest, GetTypeByIdResponse>
    {
        private readonly ISender _sender;

        public GetTypeByIdEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Get("/{id}");
            Group<AdvertismentTypeCommonEndpointGroup>();
            Description(b => b.ProducesProblem(HttpStatusCode.NotFound));
        }

        public override Task HandleAsync(GetTypeByIdRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new GetAdvertismentTypeByIdQuery(req.Id))
                .ThenAsync(query => _sender.Send(query, ct))
                .Then(result => new GetTypeByIdResponse(result.Id, result.Name))
                .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
    }
}
