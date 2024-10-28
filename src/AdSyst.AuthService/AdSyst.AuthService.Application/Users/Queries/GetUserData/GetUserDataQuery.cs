using ErrorOr;
using MediatR;

namespace AdSyst.AuthService.Application.Users.Queries.GetUserData
{
    public record GetUserDataQuery(string UserId) : IRequest<ErrorOr<UserDataDto>>;
}
