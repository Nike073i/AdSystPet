using System.Net.Mime;
using FluentValidation;
using AdSyst.WebFiles.Api.Models;
using AdSyst.WebFiles.BusinessLayer.Settings;

namespace AdSyst.WebFiles.Api.Validators
{
    public class UploadImageRequestValidator : AbstractValidator<UploadImageRequest>
    {
        private readonly string[] _allowedTypes = new[] { MediaTypeNames.Image.Jpeg, "image/png" };

        public UploadImageRequestValidator(FileStorageSettings fileStorageSettings)
        {
            RuleFor(req => req.FormFile.Length)
                .LessThanOrEqualTo(fileStorageSettings.MaxFileSizeBytes)
                .WithMessage("Файл должен быть меньше максимально допустимого размер");
            RuleFor(req => req.FormFile.ContentType)
                .Must(type => _allowedTypes.Contains(type))
                .WithMessage("Тип файла не поддерживается сервером");
        }
    }
}
