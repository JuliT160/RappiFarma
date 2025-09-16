using System.ComponentModel.DataAnnotations;
using Farma.Shared.Enums;

namespace Farma.Shared.DTOs;

/// <summary>
/// Parámetros de búsqueda de farmacias cercanas
/// </summary>
public class BuscarFarmaciasDto
{
    [Range(-90, 90, ErrorMessage = "La latitud debe estar entre -90 y 90")]
    public double? Latitud { get; set; }
    
    [Range(-180, 180, ErrorMessage = "La longitud debe estar entre -180 y 180")]
    public double? Longitud { get; set; }
    
    [MaxLength(100, ErrorMessage = "La localidad no puede exceder 100 caracteres")]
    public string? Localidad { get; set; }
    
    [MaxLength(100, ErrorMessage = "La provincia no puede exceder 100 caracteres")]
    public string? Provincia { get; set; }
    
    [MaxLength(10, ErrorMessage = "El código postal no puede exceder 10 caracteres")]
    public string? CodigoPostal { get; set; }
    
    [Range(0.1, 100, ErrorMessage = "El radio debe estar entre 0.1 y 100 km")]
    public double Radio { get; set; } = 10.0;
    
    [Range(1, 50, ErrorMessage = "El límite debe estar entre 1 y 50")]
    public int Limite { get; set; } = 20;
}

/// <summary>
/// Respuesta de búsqueda de farmacias cercanas
/// </summary>
public class FarmaciasResponseDto
{
    public int Total { get; set; }
    public BuscarFarmaciasDto Parametros { get; set; } = new();
    public string Metodo { get; set; } = string.Empty;
    public List<FarmaciaDto> Farmacias { get; set; } = new();
}

/// <summary>
/// Parámetros de búsqueda de pedidos
/// </summary>
public class BuscarPedidosDto
{
    public EstadoPedido? Estado { get; set; }
    public int? UsuarioId { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
    
    [Range(1, 100, ErrorMessage = "El límite debe estar entre 1 y 100")]
    public int Limite { get; set; } = 20;
    
    [Range(1, int.MaxValue, ErrorMessage = "La página debe ser mayor a 0")]
    public int Pagina { get; set; } = 1;
}

/// <summary>
/// Respuesta de búsqueda de pedidos
/// </summary>
public class PedidosResponseDto
{
    public int Total { get; set; }
    public BuscarPedidosDto Parametros { get; set; } = new();
    public List<PedidoResumenDto> Pedidos { get; set; } = new();
}

/// <summary>
/// Parámetros de búsqueda de cotizaciones
/// </summary>
public class BuscarCotizacionesDto
{
    public int? PedidoId { get; set; }
    public int? FarmaciaId { get; set; }
    public EstadoCotizacion? Estado { get; set; }
    public bool? IncluirExpiradas { get; set; } = false;
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
    
    [Range(1, 100, ErrorMessage = "El límite debe estar entre 1 y 100")]
    public int Limite { get; set; } = 20;
}
