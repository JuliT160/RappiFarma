using Microsoft.AspNetCore.Mvc;

namespace farma_back.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Endpoint de prueba para validar autenticación Basic Auth
    /// </summary>
    /// <returns>Respuesta de éxito si la autenticación es válida</returns>
    [HttpPost("basic-test")]
    public IActionResult BasicAuthTest()
    {
        _logger.LogInformation("Basic auth test endpoint called successfully");
        
        return Ok(new
        {
            message = "Autenticación exitosa",
            timestamp = DateTime.UtcNow,
            user = "admin",
            authType = "Basic"
        });
    }
}
