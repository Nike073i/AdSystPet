using ErrorOr;
using MediatR;

namespace AdSyst.AuthService.Application.Users.Queries.GetUserInfoDetails
{
    public record GetUserInfoDetailsQuery(string UserId) : IRequest<ErrorOr<UserInfoDetailDto>>;
}
