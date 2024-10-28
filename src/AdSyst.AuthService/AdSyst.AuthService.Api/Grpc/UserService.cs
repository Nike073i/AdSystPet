using ErrorOr;
using Grpc.Core;
using MediatR;
using AdSyst.AuthService.Application.Users.Queries.GetUserData;

namespace AdSyst.AuthService.Api.Grpc
{
    public class UserService : Users.UsersBase
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override Task<GetUserDataByIdResponse> GetUserDataById(
            GetUserDataByIdRequest request,
            ServerCallContext context
        ) =>
            request
                .ToErrorOr()
                .ThenAsync(
                    r =>
                        _mediator.Send(
                            new GetUserDataQuery(request.UserId),
                            context.CancellationToken
                        )
                )
                .MatchFirst(
                    user =>
                        new GetUserDataByIdResponse()
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        },
                    error =>
                        throw new RpcException(new Status(StatusCode.NotFound, error.Description))
                );
    }
}
