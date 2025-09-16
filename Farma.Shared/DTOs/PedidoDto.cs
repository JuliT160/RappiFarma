using System.ComponentModel.DataAnnotations;
using Farma.Shared.Enums;

namespace Farma.Shared.DTOs;

public class PedidoDto
{
    public int Id { get; set; }
    public EstadoPedido Estado { get; set; }
    
    [Required(ErrorMessage = "La calle es obligatoria")]
    [MaxLength(300, ErrorMessage = "La calle no puede exceder 300 caracteres")]
    public string Calle { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La altura es obligatoria")]
    [MaxLength(10, ErrorMessage = "La altura no puede exceder 10 caracteres")]
    public string Altura { get; set; } = string.Empty;
    
    [MaxLength(10, ErrorMessage = "El piso no puede exceder 10 caracteres")]
    public string? Piso { get; set; }
    
    [MaxLength(10, ErrorMessage = "El departamento no puede exceder 10 caracteres")]
    public string? Departamento { get; set; }
    
    [Required(ErrorMessage = "La localidad es obligatoria")]
    [MaxLength(100, ErrorMessage = "La localidad no puede exceder 100 caracteres")]
    public string Localidad { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La provincia es obligatoria")]
    [MaxLength(100, ErrorMessage = "La provincia no puede exceder 100 caracteres")]
    public string Provincia { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El código postal es obligatorio")]
    [MaxLength(10, ErrorMessage = "El código postal no puede exceder 10 caracteres")]
    public string CodigoPostal { get; set; } = string.Empty;
    
    [Range(-90, 90, ErrorMessage = "La latitud debe estar entre -90 y 90")]
    public double? Latitud { get; set; }
    
    [Range(-180, 180, ErrorMessage = "La longitud debe estar entre -180 y 180")]
    public double? Longitud { get; set; }
    
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    
    // Datos del usuario
    public int UsuarioId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
    public string UsuarioEmail { get; set; } = string.Empty;
    
    // Colecciones relacionadas
    public List<RecetaResumenDto> Recetas { get; set; } = new();
    public List<CotizacionDto> Cotizaciones { get; set; } = new();
}

public class PedidoResumenDto
{
    public int Id { get; set; }
    public EstadoPedido Estado { get; set; }
    public string DireccionCompleta { get; set; } = string.Empty;
    public string Localidad { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public int CantidadRecetas { get; set; }
    public int CantidadCotizaciones { get; set; }
}

public class CreatePedidoDto
{
    [Required(ErrorMessage = "La calle es obligatoria")]
    [MaxLength(300, ErrorMessage = "La calle no puede exceder 300 caracteres")]
    public string Calle { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La altura es obligatoria")]
    [MaxLength(10, ErrorMessage = "La altura no puede exceder 10 caracteres")]
    public string Altura { get; set; } = string.Empty;
    
    [MaxLength(10, ErrorMessage = "El piso no puede exceder 10 caracteres")]
    public string? Piso { get; set; }
    
    [MaxLength(10, ErrorMessage = "El departamento no puede exceder 10 caracteres")]
    public string? Departamento { get; set; }
    
    [Required(ErrorMessage = "La localidad es obligatoria")]
    [MaxLength(100, ErrorMessage = "La localidad no puede exceder 100 caracteres")]
    public string Localidad { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La provincia es obligatoria")]
    [MaxLength(100, ErrorMessage = "La provincia no puede exceder 100 caracteres")]
    public string Provincia { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El código postal es obligatorio")]
    [MaxLength(10, ErrorMessage = "El código postal no puede exceder 10 caracteres")]
    public string CodigoPostal { get; set; } = string.Empty;
    
    [Range(-90, 90, ErrorMessage = "La latitud debe estar entre -90 y 90")]
    public double? Latitud { get; set; }
    
    [Range(-180, 180, ErrorMessage = "La longitud debe estar entre -180 y 180")]
    public double? Longitud { get; set; }
    
    [Required(ErrorMessage = "El usuario ID es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El usuario ID debe ser mayor a 0")]
    public int UsuarioId { get; set; }
}
