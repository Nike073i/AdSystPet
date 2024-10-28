using ErrorOr;
using MediatR;

namespace AdSyst.AuthService.Application.Users.Queries.GetPersonalData
{
    public record GetPersonalDataQuery : IRequest<ErrorOr<UserPersonalData>>;
}
