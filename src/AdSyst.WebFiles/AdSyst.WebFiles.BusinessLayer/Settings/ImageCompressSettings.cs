using AdSyst.WebFiles.BusinessLayer.Models;

namespace AdSyst.WebFiles.BusinessLayer.Settings
{
    public class ImageCompressSettings
    {
        public Size SmallSize { get; set; } = new(300);

        public bool IgnoreAspectRatio { get; set; } = true;
    }
}
