namespace AdSyst.WebFiles.BusinessLayer.Interfaces
{
    public interface IFileService
    {
        Task SaveFileAsync(
            Guid resourceId,
            string fileName,
            Stream stream,
            CancellationToken cancellationToken = default
        );
    }
}
