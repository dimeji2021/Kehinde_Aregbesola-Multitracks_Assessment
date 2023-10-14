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
            // Read and log request body data
            string requestBodyPayload = await FormatRequest(httpContext.Request);
            LogHelper.RequestBody = requestBodyPayload;

            // Read and log response body data
            // Copy a pointer to the original response body stream
            var originalResponseBodyStream = httpContext.Response.Body;

            // Create a new memory stream...
            using (var responseBodyStream = new MemoryStream())
            {
                // ...and use that for the temporary response body
                httpContext.Response.Body = responseBodyStream;

                // Continue down the Middleware pipeline, eventually returning to this class
                await _next(httpContext);

                // Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
            }
        }
        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return $"{request.Method} {request.Path}{request.QueryString}, Body: {body}";
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestResponseLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLoggerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggerMiddleware>();
        }
    }
}
