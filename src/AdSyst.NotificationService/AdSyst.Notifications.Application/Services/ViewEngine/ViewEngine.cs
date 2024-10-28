using HandlebarsDotNet;

namespace AdSyst.Notifications.Application.Services.ViewEngine
{
    public class ViewEngine : IViewEngine
    {
        private readonly IHandlebars _handlebars;

        public ViewEngine()
        {
            _handlebars = Handlebars.Create();
        }

        public async Task<string> CompileHtmlAsync(
            string templatePath,
            object data,
            CancellationToken cancellationToken
        )
        {
            string template = await File.ReadAllTextAsync(templatePath, cancellationToken);
            var templateFactory = _handlebars.Compile(template);
            return templateFactory(data);
        }
    }
}
