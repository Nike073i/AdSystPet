using System.Net;
using ErrorOr;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using AdSyst.Common.Presentation.Endpoints.Shared.Extensions;

namespace AdSyst.Common.Presentation.Endpoints.Shared.Extensions
{
    public static class FastEndpointsExtensions
    {
        public static async Task HandleFailure(this IEndpoint ep, Error error)
        {
            int statusCode = error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status422UnprocessableEntity,
                ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError,
            };

            await ep.HttpContext
                .Response
                .SendResultAsync(
                    Results.Problem(statusCode: statusCode, detail: error.Description)
                );
        }

        public static RouteHandlerBuilder ProducesProblem(
            this RouteHandlerBuilder builder,
            HttpStatusCode statusCode
        ) => builder.ProducesProblem((int)statusCode);
    }
}
