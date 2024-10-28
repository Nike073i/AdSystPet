using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentList;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Common.Presentation.Endpoints.Shared.Pagination;

namespace AdSyst.Advertisments.Api.Endpoints.Advertisments.Common.GetUserAdsByFilter
{
    public class GetUserAdsByFilterEndpoint
        : Endpoint<GetUserAdsByFilterRequest, PageResponse<AdvertismentViewModel>>
    {
        private readonly ISender _sender;
        private readonly IUserContext _userContext;

        public GetUserAdsByFilterEndpoint(ISender sender, IUserContext userContext)
        {
            _sender = sender;
            _userContext = userContext;
        }

        public override void Configure()
        {
            Post("/my");
            Group<AdvertismentCommonEndpointGroup>();
            Roles(RoleNames.Client);
        }

        public override Task HandleAsync(GetUserAdsByFilterRequest req, CancellationToken ct) =>
            req.ToErrorOr()
                .FailIf(r => _userContext.UserId is null, AdvertismentErrors.Unauthorized)
                .Then(
                    r =>
                        GetAdvertismentListQuery.CreateQuery(
                            sortField: r?.SortField,
                            sortDirection: r?.SortDirection,
                            pageSize: r?.PageSize,
                            pageNumber: r?.PageNumber,
                            userId: _userContext.UserId,
                            categoryId: r?.CategoryId,
                            search: r?.Search,
                            periodEnd: r?.PeriodEnd,
                            periodStart: r?.PeriodStart
                        )
                )
                .ThenAsync(query => _sender.Send(query, ct))
                .Then(page => new PageResponse<AdvertismentViewModel>(page.PageNumber, page.Items))
                .SwitchFirstAsync(response => SendOkAsync(response), this.HandleFailure);
    }
}
