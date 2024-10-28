using System.Net;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdSyst.Common.BusinessLayer.Exceptions;
using AdSyst.Common.Presentation.Extensions;

namespace AdSyst.Common.Presentation.Controllers
{
    /// <summary>
    /// Базовый API-контроллер
    /// </summary>
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IMediator Mediator { get; }

        protected ILogger Logger { get; }

        protected IAuthorizationService? AuthorizationService { get; }

        protected Guid UserId => User.GetUserId();

        protected string UserEmail =>
            User.Identity?.IsAuthenticated != true
                ? string.Empty
                : User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

        public BaseApiController(
            IMediator mediator,
            ILogger logger,
            IAuthorizationService? authorizationService = null
        )
        {
            Mediator = mediator;
            Logger = logger;
            AuthorizationService = authorizationService;
        }

        protected IActionResult NotFound(string message) =>
            CreateProblemResult(HttpStatusCode.NotFound, message);

        protected IActionResult BadRequest(string message) =>
            CreateProblemResult(HttpStatusCode.BadRequest, message);

        protected IActionResult UnprocessableEntity(string message) =>
            CreateProblemResult(HttpStatusCode.UnprocessableEntity, message);

        protected IActionResult ForbiddenAction(string message) =>
            CreateProblemResult(HttpStatusCode.Forbidden, message);

        protected IActionResult Conflict(string message) =>
            CreateProblemResult(HttpStatusCode.Conflict, message);

        private IActionResult CreateProblemResult(HttpStatusCode statusCode, string message) =>
            Problem(statusCode: (int)statusCode, detail: message);

        protected async Task AuthorizeAccessByResource<T>(string policyName, T resource)
        {
            if (AuthorizationService == null)
                throw new InvalidOperationException("Сервис авторизации не установлен");

            var authResult = await AuthorizationService.AuthorizeAsync(User, resource, policyName);
            if (!authResult.Succeeded)
                throw new ForbiddenActionException();
        }
    }
}
