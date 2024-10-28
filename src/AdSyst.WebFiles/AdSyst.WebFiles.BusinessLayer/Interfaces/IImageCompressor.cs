using AdSyst.WebFiles.DAL.MongoDb.Enums;

namespace AdSyst.WebFiles.BusinessLayer.Interfaces
{
    public interface IImageCompressor
    {
        Task<Stream> CompressAsync(
            Stream sourceImage,
            ImageSize imageSize,
            CancellationToken cancellationToken = default
        );
    }
}
