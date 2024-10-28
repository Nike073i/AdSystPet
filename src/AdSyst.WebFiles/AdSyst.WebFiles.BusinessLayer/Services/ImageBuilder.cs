using AdSyst.WebFiles.BusinessLayer.Exceptions;
using AdSyst.WebFiles.BusinessLayer.Interfaces;
using AdSyst.WebFiles.DAL.MongoDb.Enums;
using AdSyst.WebFiles.DAL.MongoDb.Models;

namespace AdSyst.WebFiles.BusinessLayer.Services
{
    public class ImageBuilder
    {
        private readonly IImageCompressor _imageCompressor;
        private readonly IFileService _fileService;
        private readonly Guid _resourceId;
        private readonly Dictionary<ImageSize, string> _imagePaths;
        private readonly Stream _sourceStream;
        private readonly string _extension;

        public ImageBuilder(
            IImageCompressor imageCompressor,
            IFileService fileService,
            Stream sourceImageStream,
            Guid resourceId,
            string imageExtension
        )
        {
            _imageCompressor = imageCompressor;
            _fileService = fileService;
            _resourceId = resourceId;
            _sourceStream = sourceImageStream;
            _extension = imageExtension;
            _imagePaths = new();
        }

        public async Task<ImageBuilder> AddImageSizeAsync(
            ImageSize imageSize,
            CancellationToken cancellationToken
        )
        {
            if (_imagePaths.ContainsKey(imageSize))
            {
                throw new BuildImageResourceException(
                    $"Изображение с размером {imageSize} уже было указано"
                );
            }

            if (imageSize == ImageSize.Source)
                return await AddSourceImageAsync(cancellationToken);

            using var imageStream = await _imageCompressor.CompressAsync(
                _sourceStream,
                imageSize,
                cancellationToken
            );
            _sourceStream.Position = 0;

            await SaveImageAsync(imageStream, imageSize, cancellationToken);

            return this;
        }

        public Image Build() => new(_resourceId) { Paths = _imagePaths };

        private async Task<ImageBuilder> AddSourceImageAsync(CancellationToken cancellationToken)
        {
            await SaveImageAsync(_sourceStream, ImageSize.Source, cancellationToken);
            _sourceStream.Position = 0;

            return this;
        }

        private string MakeFileName(ImageSize size) => size + _extension;

        private async Task SaveImageAsync(
            Stream imageStream,
            ImageSize imageSize,
            CancellationToken cancellationToken
        )
        {
            string fileName = MakeFileName(imageSize);
            await _fileService.SaveFileAsync(_resourceId, fileName, imageStream, cancellationToken);
            _imagePaths.Add(imageSize, fileName);
        }
    }
}
