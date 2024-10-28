namespace AdSyst.WebFiles.BusinessLayer.Settings
{
    public class FileStorageSettings
    {
        public string BaseImageDirectory { get; set; } = "files/images";

        public long MaxFileSizeBytes { get; set; } = 1024 * 1024 * 10;

        public int MaxImageWidth { get; set; } = 1000;

        public int MaxImageHeight { get; set; } = 1000;
    }
}
