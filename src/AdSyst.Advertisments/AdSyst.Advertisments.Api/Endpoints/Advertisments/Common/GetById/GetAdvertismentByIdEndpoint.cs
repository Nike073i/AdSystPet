using System.Net;
using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Api.Endpoints.Advertisments.Common;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentDetail;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.GetById
{
    public class GetAdvertismentByIdEndpoint
        : Endpoint<GetAdvertismentByIdRequest, GetAdvertismentByIdResponse>
    {
        private readonly ISender _sender;

        public GetAdvertismentByIdEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Get("{id}");
            Group<AdvertismentCommonEndpointGroup>();
            AllowAnonymous();
            Description(b => b.ProducesProblem(HttpStatusCode.NotFound));
        }

        public override Task HandleAsync(GetAdvertismentByIdRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(r => new GetAdvertismentDetailQuery(r.Id))
                .ThenAsync(query => _sender.Send(query, ct))
                .Then(
                    model =>
                        new GetAdvertismentByIdResponse(
                            model.Id,
                            model.Title,
                            model.Description,
                            model.AdvertismentTypeId,
                            model.AdvertismentTypeName,
                            model.CategoryId,
                            model.CategoryName,
                            model.Price,
                            model.CreatedAt,
                            model.Status,
                            model.UserId,
                            model.ImageIds
                        )
                )
                .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
    }
}
