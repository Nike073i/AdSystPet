using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdSyst.Common.BusinessLayer.Exceptions;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Controllers;
using AdSyst.Notifications.Api.Logging;
using AdSyst.Notifications.Application.NotificationTypes.Commands.RemoveNotificationTypeCommand;
using AdSyst.Notifications.Application.NotificationTypes.Commands.SetNotificationTypeCommand;
using AdSyst.Notifications.Application.NotificationTypes.Quiries.GetUserNotificationSettings;
using AdSyst.Notifications.DAL.MongoDb.Enums;

namespace AdSyst.Notifications.Api.Controllers
{
    [Route("api/notificationTypes")]
    [Authorize(Roles = RoleNames.Client)]
    public class NotificationTypeController : BaseApiController
    {
        public NotificationTypeController(
            IMediator mediator,
            ILogger<NotificationTypeController> logger
        )
            : base(mediator, logger) { }

        [HttpPut("email")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> AddEmailNotifications()
        {
            if (string.IsNullOrEmpty(UserEmail))
            {
                Logger.EmailNotificationWasNotAddedDueToEmailIsEmptyEventLog(UserId);
                return BadRequest("Заполните Email в профиле");
            }
            var command = new SetNotificationTypeCommand(UserId, NotificationType.Email, UserEmail);
            await Mediator.Send(command, HttpContext.RequestAborted);
            Logger.EmailNotificationAddedEventLog(UserId);
            return NoContent();
        }

        [HttpDelete("{type}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> RemoveNotificationType(NotificationType type)
        {
            var command = new RemoveNotificationTypeCommand(UserId, type);
            await Mediator.Send(command, HttpContext.RequestAborted);
            Logger.EmailNotificationRemovedEventLog(UserId);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(
            typeof(GetUserNotificationSettingsQueryResponse),
            (int)HttpStatusCode.OK
        )]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCurrentUserNotificationSettings()
        {
            var query = new GetUserNotificationSettingsQuery(UserId);
            try
            {
                var response = await Mediator.Send(query, HttpContext.RequestAborted);
                Logger.UserNotificationSettingsReceivedEventLog(UserId);
                return Ok(response);
            }
            catch (NotFoundException)
            {
                Logger.UserNotificationSettingsWasNotFoundEventLog(UserId);
                return NotFound("Пользователь не имеет подключенных способов уведомления");
            }
        }
    }
}
