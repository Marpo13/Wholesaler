using Serilog;
using System.Diagnostics;

namespace Wholesaler.Backend.Api
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using (var activity = new Activity("activity"))
            {
                activity.Start();
                var traceId = activity.TraceId;

                var requestBody = string.Empty;
                using (var reader = new StreamReader(context.Request.Body))
                {
                    requestBody = await reader.ReadToEndAsync();
                }

                Log.Information(
                    $"\nTrace ID: {traceId}" +
                    $"\nRequest method: {context.Request.Method} " +
                    $"\nRequest path: {context.Request.Path} {context.Request.QueryString}" +
                    $"\nRequest body: {requestBody}");
            }

            await next(context);
        }
    }
}
