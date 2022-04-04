using System.Diagnostics;
using System.Text;

namespace RIDC.App.Api.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;
        var path = request.Path;
        var query = request.QueryString;
        var method = request.Method;
        var scheme = request.Scheme.ToUpper();
        var remoteIp = request.HttpContext.Connection.RemoteIpAddress?.ToString();

        request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(false);
        var bodyAsText = Encoding.UTF8.GetString(buffer)
            .ReplaceLineEndings()
            .Replace("\\n", "")
            .Replace(" ", "");
        request.Body.Seek(0, SeekOrigin.Begin);

        var sw = new Stopwatch();
        sw.Start();
        await _next(context);
        sw.Stop();

        var response = context.Response;
        var statusCode = response.StatusCode;

        var time = ((float)sw.ElapsedTicks / (TimeSpan.TicksPerMillisecond * 1000)).ToString("F5");

        switch (context.Response.StatusCode)
        {
            case StatusCodes.Status200OK:
                _logger.LogDebug("{Code} {Schema} {Method} {Path} {Query} with {Body} from {Ip} responded in {Time} ms",
                    statusCode, scheme, method, path, query, bodyAsText, remoteIp, time);
                break;
            case StatusCodes.Status404NotFound or StatusCodes.Status500InternalServerError or StatusCodes.Status400BadRequest:
                _logger.LogError("{Code} {Schema} {Method} {Path} {Query} with {Body} from {Ip}  responded in {Time} ms",
                    statusCode, scheme, method, path, query, bodyAsText, remoteIp, time);
                break;
            default:
                _logger.LogWarning("{Code} {Schema} {Method} {Path} {Query} with {Body} from {Ip}  responded in {Time} ms",
                    statusCode, scheme, method, path, query, bodyAsText, remoteIp, time);
                break;
        }
    }
}
