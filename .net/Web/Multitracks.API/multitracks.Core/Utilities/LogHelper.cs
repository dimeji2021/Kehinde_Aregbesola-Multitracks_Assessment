using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace multitracks.Core.Utilities
{
    public static class LogHelper
    {
        public static string RequestBody = "";

        public static async void EnrichFromRequestAsync(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;
            var response = httpContext.Response;

            var ResponseBody = await FormatResponse(response);

            IPAddress addr = IPAddress.Parse(httpContext.Connection.RemoteIpAddress.ToString());
            IPHostEntry entry = Dns.GetHostEntry(addr);
            var hostName = entry.HostName;


            diagnosticContext.Set("RequestBody", RequestBody);
            diagnosticContext.Set("ResponseBody", ResponseBody);
            diagnosticContext.Set("RequestPath", request.Path);
            diagnosticContext.Set("RequestMethod", request.Method);
            diagnosticContext.Set("MachineName", hostName);
            diagnosticContext.Set("ClientIp", httpContext.Connection.RemoteIpAddress);


            diagnosticContext.Set("Host", request.Host);
            diagnosticContext.Set("Protocol", request.Protocol);
            diagnosticContext.Set("Scheme", request.Scheme);
        }

        private static async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"{response.StatusCode}, Body: {text}";
        }
    }
}
