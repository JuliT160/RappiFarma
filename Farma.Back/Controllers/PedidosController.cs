using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using farma_back.Data;
using farma_back.Models;
using farma_back.DTOs;

namespace farma_back.Controllers;

[ApiController]
[Route("pedidos")]
public class PedidosController : ControllerBase
{
    private readonly FarmaContext _context;
    private readonly ILogger<PedidosController> _logger;

    public PedidosController(FarmaContext context, ILogger<PedidosController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Obtener un pedido por ID
    /// </summary>
    /// <param name="id">ID del pedido</param>
    /// <returns>Datos completos del pedido</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPedido(int id)
    {
        try
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Recetas)
                .Include(p => p.Cotizaciones)
                    .ThenInclude(c => c.Farmacia)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound(new { error = "Pedido no encontrado" });
            }

            var response = MapPedidoToDto(pedido);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener pedido {PedidoId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtener pedidos por estado
    /// </summary>
    /// <param name="estado">Estado del pedido (opcional)</param>
    /// <returns>Lista de pedidos filtrados</returns>
    [HttpGet]
    public async Task<IActionResult> ObtenerPedidos([FromQuery] string? estado = null)
    {
        try
        {
            var query = _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Recetas)
                .Include(p => p.Cotizaciones)
                    .ThenInclude(c => c.Farmacia)
                .AsQueryable();

            // Filtrar por estado si se proporciona
            if (!string.IsNullOrEmpty(estado))
            {
                if (Enum.TryParse<EstadoPedido>(estado.ToUpper(), out var estadoEnum))
                {
                    query = query.Where(p => p.Estado == estadoEnum);
                }
                else
                {
                    return BadRequest(new { error = "Estado de pedido no válido" });
                }
            }

            var pedidos = await query
                .OrderByDescending(p => p.FechaCreacion)
                .ToListAsync();

            var response = pedidos.Select(MapPedidoToDto).ToList();

            return Ok(new
            {
                total = response.Count,
                estado = estado,
                pedidos = response
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener pedidos con estado {Estado}", estado);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Cancelar un pedido
    /// </summary>
    /// <param name="id">ID del pedido</param>
    /// <returns>Confirmación de cancelación</returns>
    [HttpPost("{id}/cancelar")]
    public async Task<IActionResult> CancelarPedido(int id)
    {
        try
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound(new { error = "Pedido no encontrado" });
            }

            // Verificar que el pedido se pueda cancelar
            if (pedido.Estado == EstadoPedido.CANCELADO)
            {
                return BadRequest(new { error = "El pedido ya está cancelado" });
            }

            if (pedido.Estado == EstadoPedido.ENTREGADO)
            {
                return BadRequest(new { error = "No se puede cancelar un pedido ya entregado" });
            }

            var estadoAnterior = pedido.Estado;
            pedido.Estado = EstadoPedido.CANCELADO;
            pedido.FechaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Pedido {PedidoId} cancelado. Estado anterior: {EstadoAnterior}", 
                id, estadoAnterior);

            return Ok(new
            {
                pedidoId = pedido.Id,
                estadoAnterior = estadoAnterior.ToString(),
                estadoActual = pedido.Estado.ToString(),
                fechaCancelacion = pedido.FechaActualizacion,
                mensaje = "Pedido cancelado exitosamente"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cancelar pedido {PedidoId}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    private static PedidoResponseDto MapPedidoToDto(Pedido pedido)
    {
        return new PedidoResponseDto
        {
            Id = pedido.Id,
            Estado = pedido.Estado,
            Calle = pedido.Calle,
            Altura = pedido.Altura,
            Piso = pedido.Piso,
            Departamento = pedido.Departamento,
            Localidad = pedido.Localidad,
            Provincia = pedido.Provincia,
            CodigoPostal = pedido.CodigoPostal,
            Latitud = pedido.Latitud,
            Longitud = pedido.Longitud,
            FechaCreacion = pedido.FechaCreacion,
            FechaActualizacion = pedido.FechaActualizacion,
            UsuarioId = pedido.UsuarioId,
            UsuarioNombre = pedido.Usuario?.Nombre ?? "",
            UsuarioEmail = pedido.Usuario?.Email ?? "",
            Recetas = pedido.Recetas.Select(r => new RecetaResponseDto
            {
                Id = r.Id,
                NombreArchivoOriginal = r.NombreArchivoOriginal,
                TipoArchivo = r.TipoArchivo,
                TamanoArchivo = r.TamanoArchivo,
                FechaCreacion = r.FechaCreacion
            }).ToList(),
            Cotizaciones = pedido.Cotizaciones.Select(c => new CotizacionResponseDto
            {
                Id = c.Id,
                MontoTotal = c.MontoTotal,
                MontoMedicamentos = c.MontoMedicamentos,
                MontoEnvio = c.MontoEnvio,
                Estado = c.Estado,
                Observaciones = c.Observaciones,
                FechaCreacion = c.FechaCreacion,
                FechaExpiracion = c.FechaExpiracion,
                FarmaciaId = c.FarmaciaId,
                FarmaciaNombre = c.Farmacia?.Nombre ?? "",
                FarmaciaDireccion = c.Farmacia?.Direccion ?? ""
            }).ToList()
        };
    }
}
