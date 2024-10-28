using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdSyst.Common.Presentation.Controllers;
using AdSyst.WebFiles.Api.Logging;
using AdSyst.WebFiles.Api.Models;
using AdSyst.WebFiles.Application.Images.Commands.UploadImage;
using AdSyst.WebFiles.BusinessLayer.Exceptions;

namespace AdSyst.WebFiles.Api.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия с изображениями
    /// </summary>
    [Route("api/files/images")]
    public class ImageController : BaseApiController
    {
        public ImageController(IMediator mediator, ILogger<ImageController> logger)
            : base(mediator, logger) { }

        /// <summary>
        /// Загрузить изображение
        /// </summary>
        /// <param name="request">Модель данных для загрузки изображения</param>
        /// <returns>Идентификатор изображения</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="401">Пользователь должен быть аутентифицирован</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            var file = request.FormFile;
            using var fileStream = file.OpenReadStream();
            var command = new UploadImageCommand(fileStream, file.FileName);
            try
            {
                var response = await Mediator.Send(command, HttpContext.RequestAborted);
                Logger.ImageUploadedEventLog(UserId, response);
                return Ok(response);
            }
            catch (InvalidFileDataException ex)
            {
                Logger.ImageWasNotUploadedDueInvalidFileDataEventLog(
                    command.FileName,
                    UserId,
                    ex.Message
                );
                return BadRequest(ex.Message);
            }
        }
    }
}
