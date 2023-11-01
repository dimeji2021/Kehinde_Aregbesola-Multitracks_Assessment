using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using multitracks.Core.Utilities;
using System.Threading.Tasks;

namespace multitracks.API
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestResponseLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestResponseLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            string requestBodyPayload = await FormatRequest(httpContext.Request);
            LogHelper.RequestBody = requestBodyPayload;

            var originalResponseBodyStream = httpContext.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                httpContext.Response.Body = responseBody;
                await _next(httpContext);
                await responseBody.CopyToAsync(originalResponseBodyStream);
            }
        }
        private static async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return $"{request.Method} {request.Path}{request.QueryString}, Body: {body}";
        }
    }

    public static class RequestResponseLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLoggerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggerMiddleware>();
        }
    }
}
