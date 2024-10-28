using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using AdSyst.Common.Application.Abstractions.Authentication;
using Serilog.Context;

namespace AdSyst.Common.Application.Abstractions.Behaviors
{
    internal class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IUserContext _userContext;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            IUserContext userContext
        )
        {
            _logger = logger;
            _userContext = userContext;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            string requestName = request.GetType().Name;
            var userId = _userContext.UserId;

            using (LogContext.PushProperty("User", userId, true))
            {
                try
                {
                    _logger.RequestReceived(requestName);

                    var result = await next();

                    if (result.IsError)
                    {
                        using (LogContext.PushProperty("Errors", result.Errors, true))
                        {
                            _logger.RequestCompletedWithError(requestName);
                        }
                    }
                    else
                    {
                        _logger.RequestCompletedSuccessfully(requestName);
                    }

                    return result;
                }
                catch (Exception exception)
                {
                    _logger.RequestFailed(requestName, exception);
                    throw;
                }
            }
        }
    }
}
