using ImageMagick;
using AdSyst.WebFiles.BusinessLayer.Interfaces;
using AdSyst.WebFiles.BusinessLayer.Models;

namespace AdSyst.WebFiles.BusinessLayer.Services
{
    public class ImageProcessor : IImageProcessor
    {
        public Size GetSize(Stream imageStream)
        {
            using var image = new MagickImage(imageStream);
            imageStream.Position = 0;
            return new(image.Width, image.Height);
        }

        public async Task<Stream> ResizeAsync(
            Size size,
            Stream imageStream,
            bool ignoreAspectRatio,
            CancellationToken cancellationToken
        )
        {
            using var image = new MagickImage(imageStream);
            var geometry = new MagickGeometry(size.Width, size.Height)
            {
                IgnoreAspectRatio = ignoreAspectRatio
            };
            image.Resize(geometry);
            var memoryStream = new MemoryStream();
            await image.WriteAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
