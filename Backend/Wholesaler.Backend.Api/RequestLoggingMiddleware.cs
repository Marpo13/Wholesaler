using System.Diagnostics;
using Serilog.Context;
using Serilog.Core.Enrichers;

namespace Wholesaler.Backend.Api;

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
                requestBody = await reader.ReadToEndAsync();

            using (LogContext.Push(
                new PropertyEnricher("TraceId", Guid.NewGuid()),
                new PropertyEnricher("CorrelationId", traceId),
                new PropertyEnricher("RequestMethod", context.Request.Method),
                new PropertyEnricher("Request path", context.Request.Path),
                new PropertyEnricher("Request parameters", context.Request.QueryString),
                new PropertyEnricher("Request body", requestBody)))
            {
                await next(context);
            }
        }
    }
}
