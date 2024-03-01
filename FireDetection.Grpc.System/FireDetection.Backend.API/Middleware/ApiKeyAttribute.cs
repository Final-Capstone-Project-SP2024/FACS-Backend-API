using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FireDetection.Backend.API.Middleware
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "x-api-key";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var headers))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api key was not provided",
                    ContentType = "application/json"
                };
                return;
            }
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(APIKEYNAME);
            if (!apiKey.Equals(headers))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api key was not provided",
                    ContentType = "application/json"
                };
                return;
            }
            await next();
        }
    }
}
