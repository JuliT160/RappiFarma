using System.Text;

namespace farma_back.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationMiddleware> _logger;

    // Credenciales hardcodeadas para PoC
    private const string VALID_USERNAME = "admin";
    private const string VALID_PASSWORD = "admin123";
    private const string VALID_API_KEY = "farma-api-key-2025";

    public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<AuthenticationMiddleware> logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Rutas que no requieren autenticación
        var path = context.Request.Path.Value?.ToLower() ?? "";
        if (path.StartsWith("/openapi") || path.StartsWith("/swagger") || path == "/")
        {
            await _next(context);
            return;
        }

        var authHeader = context.Request.Headers.Authorization.FirstOrDefault();
        
        if (authHeader == null)
        {
            await UnauthorizedResponse(context, "Missing Authorization header");
            return;
        }

        try
        {
            if (authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                if (!ValidateBasicAuth(authHeader))
                {
                    await UnauthorizedResponse(context, "Invalid Basic Auth credentials");
                    return;
                }
            }
            else if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                if (!ValidateBearerToken(authHeader))
                {
                    await UnauthorizedResponse(context, "Invalid Bearer token");
                    return;
                }
            }
            else
            {
                await UnauthorizedResponse(context, "Invalid Authorization scheme");
                return;
            }

            _logger.LogInformation("Authentication successful for {Path}", context.Request.Path);
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Authentication error");
            await UnauthorizedResponse(context, "Authentication error");
        }
    }

    private bool ValidateBasicAuth(string authHeader)
    {
        try
        {
            var token = authHeader.Substring("Basic ".Length).Trim();
            var credentialBytes = Convert.FromBase64String(token);
            var credentials = Encoding.UTF8.GetString(credentialBytes);
            var parts = credentials.Split(':', 2);

            if (parts.Length != 2)
                return false;

            var username = parts[0];
            var password = parts[1];

            return username == VALID_USERNAME && password == VALID_PASSWORD;
        }
        catch
        {
            return false;
        }
    }

    private bool ValidateBearerToken(string authHeader)
    {
        try
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            return token == VALID_API_KEY;
        }
        catch
        {
            return false;
        }
    }

    private async Task UnauthorizedResponse(HttpContext context, string message)
    {
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";
        
        var response = new
        {
            error = "Unauthorized",
            message = message,
            timestamp = DateTime.UtcNow
        };

        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}
