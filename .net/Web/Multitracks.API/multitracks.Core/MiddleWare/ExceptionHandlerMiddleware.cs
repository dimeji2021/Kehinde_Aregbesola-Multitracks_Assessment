using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using multitracks.Core.Dtos;
using multitracks.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace multitracks.Core.MiddleWare
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)GetStatusCode(ex);
            var errorStatusCode = httpContext.Response.StatusCode;

            _logger.LogError("Error Processing Request", ex.ToString());

            string errorMessage = ex.InnerException?.Message ?? ex.Message;

            var res = new
            {
                ErrorMessage = errorMessage,
                IsSuccess = false,
                ErrorSource = ex.Source,
                Type = ex.GetType(),
                ErrorStatusCode = errorStatusCode
            };

            _logger.LogInformation($"System encountered an error:\n\r {ex} \n\r {ex.StackTrace}");
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(res));
        }
        private static HttpStatusCode GetStatusCode(Exception ex)
        {
            if (ex is not BaseException internalException)
            {
                return HttpStatusCode.InternalServerError;
            }
            return internalException.StatusCode;
        }
    }
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
