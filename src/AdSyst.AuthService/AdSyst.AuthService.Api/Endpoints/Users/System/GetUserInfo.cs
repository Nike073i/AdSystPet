using ErrorOr;
using FastEndpoints;
using MediatR;
using AdSyst.AuthService.Application.Users.Queries.GetUserInfoList;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;
using AdSyst.Common.Presentation.Endpoints.Shared.Pagination;

namespace AdSyst.AuthService.Api.Endpoints.Users.System
{
    public static class GetUserInfo
    {
        public class Endpoint : Endpoint<PageRequest, Response>
        {
            private readonly ISender _sender;

            public Endpoint(ISender sender)
            {
                _sender = sender;
            }

            public override void Configure()
            {
                Get("/");
                Group<UserSystemEndpointGroup>();
            }

            public override Task HandleAsync(PageRequest req, CancellationToken ct) =>
                req.ToErrorOr()
                    .Then(
                        r =>
                            GetUserInfoListQuery.CreateQuery(
                                pageSize: req.PageSize,
                                pageNumber: req.PageNumber
                            )
                    )
                    .ThenAsync(q => _sender.Send(q, ct))
                    .Then(res => new Response(res.PageNumber, res.Items))
                    .SwitchFirstAsync(res => SendOkAsync(res), this.HandleFailure);
        }

        public record Response(int PageNumber, IEnumerable<UserInfoDto> Users);
    }
}
