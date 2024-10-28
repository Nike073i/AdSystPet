using AdSyst.WebFiles.BusinessLayer.Interfaces;
using AdSyst.WebFiles.BusinessLayer.Settings;

namespace AdSyst.WebFiles.BusinessLayer.Services
{
    public class FileService : IFileService
    {
        private readonly FileStorageSettings _settings;

        public FileService(FileStorageSettings settings)
        {
            _settings = settings;
        }

        public async Task SaveFileAsync(
            Guid resourceId,
            string fileName,
            Stream stream,
            CancellationToken cancellationToken
        )
        {
            string dir = GetFileLocation(resourceId, _settings.BaseImageDirectory);
            CreateFolder(dir);
            using var fileStream = new FileStream(Path.Combine(dir, fileName), FileMode.Create);
            await stream.CopyToAsync(fileStream, cancellationToken);
        }

        private static string GetFileLocation(Guid resourceId, string filesDirectory)
        {
            string resourcePath = resourceId.ToString();
            return Path.Combine(
                Environment.CurrentDirectory,
                filesDirectory,
                resourcePath[..2],
                resourcePath[..4],
                resourcePath
            );
        }

        private static void CreateFolder(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }
}
