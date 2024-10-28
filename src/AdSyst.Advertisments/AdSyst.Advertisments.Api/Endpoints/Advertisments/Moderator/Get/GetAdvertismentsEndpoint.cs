using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Api.Endpoints.Categories.Common;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.BusinessLayer.Enums;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Common.Presentation.Endpoints.Shared.Pagination;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Moderator.Get
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
            Group<AdvertismentModeratorEndpointGroup>();
        }

        public override Task HandleAsync(PageRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .Then(
                    r =>
                        GetAdvertismentListQuery.CreateQuery(
                            sortField: SortAdvertismentField.DateOfPublish,
                            sortDirection: SortDirection.Asc,
                            pageSize: r?.PageSize,
                            pageNumber: r?.PageNumber,
                            advertismentStatus: AdvertismentStatus.Moderation
                        )
                )
                .ThenAsync(query => _sender.Send(query, ct))
                .Then(page => new PageResponse<AdvertismentViewModel>(page.PageNumber, page.Items))
                .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
    }
}
