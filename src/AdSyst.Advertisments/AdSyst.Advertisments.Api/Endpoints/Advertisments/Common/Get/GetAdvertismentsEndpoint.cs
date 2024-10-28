using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Common.Presentation.Endpoints.Shared.Pagination;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.Get
{
    public class GetAdvertismentsEndpoint
        : Endpoint<PageRequest, PageResponse<AdvertismentViewModel>>
    {
        private readonly ISender _sender;

        public GetAdvertismentsEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Get("/");
            Group<AdvertismentCommonEndpointGroup>();
            AllowAnonymous();
        }

        public override Task HandleAsync(PageRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(
                    r =>
                        GetAdvertismentListQuery.CreateQuery(
                            pageSize: r?.PageSize,
                            pageNumber: r?.PageNumber,
                            advertismentStatus: AdvertismentStatus.Active
                        )
                )
                .ThenAsync(query => _sender.Send(query, ct))
                .Then(page => new PageResponse<AdvertismentViewModel>(page.PageNumber, page.Items))
                .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
    }
}
