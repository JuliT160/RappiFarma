using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using farma_back.Data;
using farma_back.Models;
using farma_back.DTOs;
using farma_back.Services;
using System.ComponentModel.DataAnnotations;

namespace farma_back.Controllers;

[ApiController]
[Route("recetas")]
public class RecetasController : ControllerBase
{
    private readonly FarmaContext _context;
    private readonly IFileService _fileService;
    private readonly ILogger<RecetasController> _logger;

    public RecetasController(FarmaContext context, IFileService fileService, ILogger<RecetasController> logger)
    {
        _context = context;
        _fileService = fileService;
        _logger = logger;
    }

    /// <summary>
    /// Crear una nueva receta con archivo adjunto y datos de dirección
    /// </summary>
    /// <param name="archivo">Archivo de la receta</param>
    /// <param name="calle">Calle de entrega</param>
    /// <param name="altura">Altura de entrega</param>
    /// <param name="piso">Piso (opcional)</param>
    /// <param name="depto">Departamento (opcional)</param>
    /// <param name="localidad">Localidad de entrega</param>
    /// <param name="provincia">Provincia de entrega</param>
    /// <param name="codigoPostal">Código postal</param>
    /// <param name="lat">Latitud (opcional)</param>
    /// <param name="lon">Longitud (opcional)</param>
    /// <param name="usuarioId">ID del usuario</param>
    /// <returns>Datos del pedido creado</returns>
    [HttpPost]
    public async Task<IActionResult> CrearReceta(
        [FromForm] IFormFile archivo,
        [FromForm, Required] string calle,
        [FromForm, Required] string altura,
        [FromForm] string? piso,
        [FromForm] string? depto,
        [FromForm, Required] string localidad,
        [FromForm, Required] string provincia,
        [FromForm, Required] string codigoPostal,
        [FromForm] double? lat,
        [FromForm] double? lon,
        [FromForm, Required] int usuarioId)
    {
        try
        {
            // Validar archivo
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest(new { error = "El archivo de receta es obligatorio" });
            }

            if (!_fileService.IsValidFileType(archivo.FileName))
            {
                return BadRequest(new { error = "Tipo de archivo no permitido. Use: .jpg, .jpeg, .png, .pdf, .doc, .docx" });
            }

            if (!_fileService.IsValidFileSize(archivo.Length))
            {
                return BadRequest(new { error = "El archivo excede el tamaño máximo permitido (10MB)" });
            }

            // Crear DTO para validación
            var recetaDto = new RecetaRequestDto
            {
                Calle = calle,
                Altura = altura,
                Piso = piso,
                Departamento = depto,
                Localidad = localidad,
                Provincia = provincia,
                CodigoPostal = codigoPostal,
                Latitud = lat,
                Longitud = lon,
                UsuarioId = usuarioId
            };

            // Validar DTO
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(recetaDto);
            if (!Validator.TryValidateObject(recetaDto, validationContext, validationResults, true))
            {
                var errors = validationResults.Select(vr => vr.ErrorMessage).ToList();
                return BadRequest(new { error = "Datos de validación incorrectos", details = errors });
            }

            // Verificar que el usuario existe
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return BadRequest(new { error = "Usuario no encontrado" });
            }

            var fechaActual = DateTime.UtcNow;

            // Guardar archivo
            string rutaArchivo;
            try
            {
                rutaArchivo = await _fileService.SaveRecetaFileAsync(archivo, fechaActual);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar archivo de receta");
                return StatusCode(500, new { error = "Error al guardar el archivo" });
            }

            // Crear pedido
            var pedido = new Pedido
            {
                Estado = EstadoPedido.CREADO,
                Calle = calle,
                Altura = altura,
                Piso = piso,
                Departamento = depto,
                Localidad = localidad,
                Provincia = provincia,
                CodigoPostal = codigoPostal,
                Latitud = lat,
                Longitud = lon,
                UsuarioId = usuarioId,
                FechaCreacion = fechaActual
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            // Crear receta asociada al pedido
            var receta = new Receta
            {
                RutaArchivo = rutaArchivo,
                NombreArchivoOriginal = archivo.FileName,
                TipoArchivo = Path.GetExtension(archivo.FileName),
                TamanoArchivo = archivo.Length,
                PedidoId = pedido.Id,
                FechaCreacion = fechaActual
            };

            _context.Recetas.Add(receta);

            // Cambiar estado del pedido a EN_COTIZACION
            pedido.Estado = EstadoPedido.EN_COTIZACION;
            pedido.FechaActualizacion = fechaActual;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Receta creada exitosamente. PedidoId: {PedidoId}, RecetaId: {RecetaId}", pedido.Id, receta.Id);

            // Retornar respuesta
            var response = new
            {
                pedidoId = pedido.Id,
                recetaId = receta.Id,
                estado = pedido.Estado.ToString(),
                mensaje = "Receta y pedido creados exitosamente",
                fechaCreacion = pedido.FechaCreacion,
                direccionEntrega = new
                {
                    calle = pedido.Calle,
                    altura = pedido.Altura,
                    piso = pedido.Piso,
                    departamento = pedido.Departamento,
                    localidad = pedido.Localidad,
                    provincia = pedido.Provincia,
                    codigoPostal = pedido.CodigoPostal,
                    latitud = pedido.Latitud,
                    longitud = pedido.Longitud
                },
                archivo = new
                {
                    nombre = receta.NombreArchivoOriginal,
                    tipo = receta.TipoArchivo,
                    tamano = receta.TamanoArchivo
                }
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al crear receta");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }
}
