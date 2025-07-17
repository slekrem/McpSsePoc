// Program.cs
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddHttpContextAccessor()
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

var app = builder.Build();

app.MapMcp();

app.Run("http://localhost:5267");

[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message)
    {
        return $"echo from McpSsePoc: {message}";
    }

    [McpServerTool, Description("Echo Query String")]
    public static string EchoQueryString(IHttpContextAccessor httpContext)
    {
        var queryString = httpContext?.HttpContext?.Request?.QueryString.ToString() ?? "";
        return $"echo from QueryString: {queryString}";
    }
}
