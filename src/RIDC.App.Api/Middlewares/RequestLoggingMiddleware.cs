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

    private const string Template = "{Method} {Path} {QueryString} {Protocol} with {Body} - {UserAgent} from {Ip} responded {StatusCode} in {RequestProcessTime} ms";

    private record RequestLog(string Method, string Ip, string Path, string QueryString, string Body, string Protocol, string UserAgent);
    private record ResponseLog(int StatusCode, string Time);

    public async Task Invoke(HttpContext context)
    {

        var request = context.Request;
        var requestLog = await FormatRequest(request);
        var (method, ip, path, queryString, body, protocol, userAgent) = requestLog;

        if (requestLog.UserAgent is "UNKNOWN-USER-AGENT")
        {
            await context.Response.WriteAsync("User agent is not supported");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            _logger.LogWarning(Template, method, path, queryString, protocol, body, userAgent, ip,
                StatusCodes.Status400BadRequest, "0.00000");
            return;
        }

        var sw = new Stopwatch();
        sw.Start();

        await _next(context);

        sw.Stop();

        var response = context.Response;
        var responseLog = FormatResponse(response, sw.ElapsedTicks);
        var (statusCode, time) = responseLog;

        switch (responseLog.StatusCode / 100)
        {
            case 4:
                _logger.LogWarning(Template, method, path, queryString, protocol, body, userAgent, ip, statusCode, time);
                return;
            case 5:
                _logger.LogError(Template, method, path, queryString, protocol, body, userAgent, ip, statusCode, time);
                return;
            default:
                _logger.LogInformation(Template, method, path, queryString, protocol, body, userAgent, ip, statusCode, time);
                return;
        }
    }

    private static async Task<RequestLog> FormatRequest(HttpRequest request)
    {
        var path = request.Path.ToString();
        var query = request.QueryString.ToString();
        var method = request.Method;
        var protocol = request.Protocol.ToUpper();
        var userAgent = request.Headers["User-Agent"].ToString();
        var remoteIp = request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "UNKNOWN-IP";

        if (string.IsNullOrEmpty(userAgent))
        {
            userAgent = "UNKNOWN-USER-AGENT";
        }

        request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(false);
        var bodyAsText = Encoding.UTF8.GetString(buffer)
            .ReplaceLineEndings()
            .Replace("\\n", "")
            .Replace(" ", "");
        request.Body.Seek(0, SeekOrigin.Begin);

        return new RequestLog(method, remoteIp, path, query, bodyAsText, protocol, userAgent);
    }

    private static ResponseLog FormatResponse(HttpResponse response, long elapsedTicks)
    {
        return new ResponseLog(response.StatusCode,
            ((float)elapsedTicks / (TimeSpan.TicksPerMillisecond * 1000)).ToString("F5"));
    }
}
