namespace AdSyst.WebFiles.BusinessLayer.Interfaces
{
    public interface IImageService
    {
        Task<Guid> SaveAsync(
            Stream sourceStream,
            string imageExtension,
            CancellationToken cancellationToken = default
        );
    }
}
