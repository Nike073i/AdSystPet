using Microsoft.AspNetCore.Builder;

namespace AdSyst.Common.Presentation.Middlewares.OperationCanceled
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseOperationCanceledHandler(this IApplicationBuilder app) =>
            app.UseMiddleware<OperationCanceledMiddleware>();
    }
}
