using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Api.Endpoints.Categories.Common;
using AdSyst.Advertisments.Application.AdvertismentTypes.Queries.GetAdvertismentTypeList;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Advertisments.Api.Endpoints.AdvertismentTypes.Common.GetAll
{
    public class GetAllTypesEnpdoint : EndpointWithoutRequest<GetAllTypesResponse>
    {
        private readonly ISender _sender;

        public GetAllTypesEnpdoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Get("/");
            Group<AdvertismentTypeCommonEndpointGroup>();
        }

        public override Task HandleAsync(CancellationToken ct) =>
            ErrorOrFactory
                .From(new GetAdvertismentTypeListQuery())
                .ThenAsync(query => _sender.Send(query, ct))
                .Then(res => new GetAllTypesResponse(res))
                .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
    }
}
