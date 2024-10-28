using AdSyst.WebFiles.BusinessLayer.Interfaces;
using AdSyst.WebFiles.BusinessLayer.Models;
using AdSyst.WebFiles.BusinessLayer.Settings;
using AdSyst.WebFiles.DAL.MongoDb.Enums;

namespace AdSyst.WebFiles.BusinessLayer.Services
{
    public class ImageCompressor : IImageCompressor
    {
        private readonly ImageCompressSettings _settings;

        private readonly IImageProcessor _imageProcessor;

        public ImageCompressor(
            ImageCompressSettings comressSettings,
            IImageProcessor imageProcessor
        )
        {
            _settings = comressSettings;
            _imageProcessor = imageProcessor;
        }

        public async Task<Stream> CompressAsync(
            Stream sourceImage,
            ImageSize imageSize,
            CancellationToken cancellationToken
        )
        {
            var newGeometry = GetSizeByEnum(imageSize);
            var compressedStream = await _imageProcessor.ResizeAsync(
                newGeometry,
                sourceImage,
                ignoreAspectRatio: _settings.IgnoreAspectRatio,
                cancellationToken: cancellationToken
            );
            return compressedStream;
        }

        private Size GetSizeByEnum(ImageSize imageSize) =>
            imageSize switch
            {
                ImageSize.Small => _settings.SmallSize,
                ImageSize.Source
                or _
                    => throw new InvalidOperationException(
                        $"Сжатие изображения в размер - {imageSize} невозможно"
                    )
            };
    }
}
