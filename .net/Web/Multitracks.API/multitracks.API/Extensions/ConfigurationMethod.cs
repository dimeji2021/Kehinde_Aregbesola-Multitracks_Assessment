using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics;
using multitracks.Core.Dtos;
using Newtonsoft.Json;
using System.Net.Mime;
using Serilog;

namespace multitracks.API.Extensions
{
    public class ConfigurationMethod
    {
        public static void ConfigureGlobalExceptionHandler(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)
                    {
                        Log.Error($"Something went wrong in the {contextFeature.Error}"); // Install serilog
                        await context.Response.WriteAsync(new ResponseDto<string>
                        {
                            StatusCode = context.Response.StatusCode,
                            Status = false,
                            Message = "An Internal Server Error has occurred. Please refer to the log file for additional details regarding this error.",
                            //Data = contextFeature.Error.ToString()
                        }.ToString());
                    }
                });
            });
        }
        public static void ConfigiureHealthChecks(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains("ready"),
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonConvert.SerializeObject(
                        new
                        {
                            status = report.Status.ToString(),
                            checks = report.Entries.Select(entry => new
                            {
                                name = entry.Key,
                                status = entry.Value.Status.ToString(),
                                exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                                duration = entry.Value.Duration.ToString()
                            })
                        });
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });
            applicationBuilder.UseHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = (_) => false
            });
        }
    }
}
