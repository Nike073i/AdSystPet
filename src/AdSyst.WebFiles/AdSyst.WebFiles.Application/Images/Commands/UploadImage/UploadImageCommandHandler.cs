using MediatR;
using AdSyst.WebFiles.BusinessLayer.Exceptions;
using AdSyst.WebFiles.BusinessLayer.Interfaces;
using AdSyst.WebFiles.BusinessLayer.Settings;

namespace AdSyst.WebFiles.Application.Images.Commands.UploadImage
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Guid>
    {
        private readonly IImageService _imageService;
        private readonly IImageProcessor _imageProcessor;
        private readonly FileStorageSettings _fileStorageSettings;

        public UploadImageCommandHandler(
            IImageService imageService,
            IImageProcessor imageProcessor,
            FileStorageSettings fileStorageSettings
        )
        {
            _imageService = imageService;
            _imageProcessor = imageProcessor;
            _fileStorageSettings = fileStorageSettings;
        }

        public Task<Guid> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var imageSize = _imageProcessor.GetSize(request.ImageStream);
            int maxWidth = _fileStorageSettings.MaxImageWidth;
            int maxHeight = _fileStorageSettings.MaxImageHeight;

            if (imageSize.Width > maxWidth || imageSize.Height > maxHeight)
            {
                throw new InvalidFileDataException(
                    $"Размеры изображения превышают разрешенные {maxWidth}x{maxHeight}"
                );
            }

            string imageExtension = Path.GetExtension(request.FileName);
            return _imageService.SaveAsync(request.ImageStream, imageExtension, cancellationToken);
        }
    }
}
