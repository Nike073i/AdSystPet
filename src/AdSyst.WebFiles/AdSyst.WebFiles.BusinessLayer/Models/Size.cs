namespace AdSyst.WebFiles.BusinessLayer.Models
{
    public record struct Size(int Width, int Height)
    {
        public Size(int widthAndHeight)
            : this(widthAndHeight, widthAndHeight) { }
    }
}
