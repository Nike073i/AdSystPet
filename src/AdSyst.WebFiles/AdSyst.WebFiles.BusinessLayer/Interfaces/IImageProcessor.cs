using AdSyst.WebFiles.BusinessLayer.Models;

namespace AdSyst.WebFiles.BusinessLayer.Interfaces
{
    public interface IImageProcessor
    {
        Task<Stream> ResizeAsync(
            Size size,
            Stream imageStream,
            bool ignoreAspectRatio = true,
            CancellationToken cancellationToken = default
        );
        Size GetSize(Stream imageStream);
    }
}
