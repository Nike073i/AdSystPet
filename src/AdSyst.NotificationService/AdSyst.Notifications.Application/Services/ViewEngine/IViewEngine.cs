namespace AdSyst.Notifications.Application.Services.ViewEngine
{
    public interface IViewEngine
    {
        Task<string> CompileHtmlAsync(
            string templatePath,
            object data,
            CancellationToken cancellationToken = default
        );
    }
}
