using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Common.Presentation.Endpoints.Shared.Pagination;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.GetByFilter
{
    public class GetAdvertismentsByFilterEndpoint
        : Endpoint<GetAdvertismentsByFilterRequest, PageResponse<AdvertismentViewModel>>
    {
        private readonly ISender _sender;

        public GetAdvertismentsByFilterEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Post("/filter");
            Group<AdvertismentCommonEndpointGroup>();
            AllowAnonymous();
        }

        public override Task HandleAsync(
            GetAdvertismentsByFilterRequest req,
            CancellationToken ct
        ) =>
            req.ToErrorOr()
                .Then(
                    r =>
                        GetAdvertismentListQuery.CreateQuery(
                            sortField: r.SortField,
                            sortDirection: r.SortDirection,
                            pageSize: r.PageSize,
                            pageNumber: r.PageNumber,
                            categoryId: r.CategoryId,
                            search: r.Search,
                            periodEnd: r.PeriodEnd,
                            periodStart: r.PeriodStart,
                            advertismentStatus: AdvertismentStatus.Active
                        )
                )
                .ThenAsync(query => _sender.Send(query, ct))
                .Then(page => new PageResponse<AdvertismentViewModel>(page.PageNumber, page.Items))
                .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
    }
}
