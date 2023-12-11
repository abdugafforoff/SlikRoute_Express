using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        using (LogContext.PushProperty("RequestPath", context.Request.Path))
        using (LogContext.PushProperty("RequestMethod", context.Request.Method))
        {
            Log.Information("Request received: {RequestPath}, {RequestMethod}, {RequestHeaders}, {QueryString}",
                context.Request.Path, context.Request.Method, context.Request.Headers, context.Request.QueryString);
        }
        await _next(context);

        using (LogContext.PushProperty("RequestPath", context.Request.Path))
        using (LogContext.PushProperty("RequestMethod", context.Request.Method))
        {
            Log.Information("Request processed: {RequestPath}, {RequestMethod}, {ResponseStatusCode}",
                context.Request.Path, context.Request.Method, context.Response.StatusCode);
        }
    }
}