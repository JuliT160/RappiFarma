using System.ComponentModel.DataAnnotations;
using Farma.Shared.Enums;

namespace Farma.Shared.DTOs;

public class CotizacionDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El monto total es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto total debe ser mayor a 0")]
    public decimal MontoTotal { get; set; }
    
    [Required(ErrorMessage = "El monto de medicamentos es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El monto de medicamentos debe ser mayor o igual a 0")]
    public decimal MontoMedicamentos { get; set; }
    
    [Required(ErrorMessage = "El monto de envío es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El monto de envío debe ser mayor o igual a 0")]
    public decimal MontoEnvio { get; set; }
    
    public EstadoCotizacion Estado { get; set; }
    
    [MaxLength(1000, ErrorMessage = "Las observaciones no pueden exceder 1000 caracteres")]
    public string? Observaciones { get; set; }
    
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaExpiracion { get; set; }
    
    // Datos del pedido
    public int PedidoId { get; set; }
    
    // Datos de la farmacia
    public int FarmaciaId { get; set; }
    public string FarmaciaNombre { get; set; } = string.Empty;
    public string FarmaciaDireccion { get; set; } = string.Empty;
    public string FarmaciaLocalidad { get; set; } = string.Empty;
    public string? FarmaciaTelefono { get; set; }
}

public class CotizacionResumenDto
{
    public int Id { get; set; }
    public decimal MontoTotal { get; set; }
    public EstadoCotizacion Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public string FarmaciaNombre { get; set; } = string.Empty;
    public bool Expirada => DateTime.UtcNow > FechaExpiracion;
}

public class CreateCotizacionDto
{
    [Required(ErrorMessage = "El monto total es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto total debe ser mayor a 0")]
    public decimal MontoTotal { get; set; }
    
    [Required(ErrorMessage = "El monto de medicamentos es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El monto de medicamentos debe ser mayor o igual a 0")]
    public decimal MontoMedicamentos { get; set; }
    
    [Required(ErrorMessage = "El monto de envío es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El monto de envío debe ser mayor o igual a 0")]
    public decimal MontoEnvio { get; set; }
    
    [MaxLength(1000, ErrorMessage = "Las observaciones no pueden exceder 1000 caracteres")]
    public string? Observaciones { get; set; }
    
    [Required(ErrorMessage = "El pedido ID es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El pedido ID debe ser mayor a 0")]
    public int PedidoId { get; set; }
    
    [Required(ErrorMessage = "La farmacia ID es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "La farmacia ID debe ser mayor a 0")]
    public int FarmaciaId { get; set; }
    
    [Range(1, 365, ErrorMessage = "Los días de expiración deben estar entre 1 y 365")]
    public int DiasExpiracion { get; set; } = 7;
}
