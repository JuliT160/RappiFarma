using System.ComponentModel.DataAnnotations;
using Farma.Shared.Enums;

namespace Farma.Shared.DTOs;

public class PagoIntentDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El Intent ID es obligatorio")]
    [MaxLength(100, ErrorMessage = "El Intent ID no puede exceder 100 caracteres")]
    public string IntentId { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El monto es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
    public decimal Monto { get; set; }
    
    [Required(ErrorMessage = "La moneda es obligatoria")]
    [MaxLength(3, ErrorMessage = "La moneda debe ser de 3 caracteres")]
    public string Moneda { get; set; } = "ARS";
    
    public EstadoPago Estado { get; set; }
    
    [MaxLength(50, ErrorMessage = "El método de pago no puede exceder 50 caracteres")]
    public string? MetodoPago { get; set; }
    
    [MaxLength(500, ErrorMessage = "La URL de confirmación no puede exceder 500 caracteres")]
    public string? UrlConfirmacion { get; set; }
    
    [MaxLength(1000, ErrorMessage = "Los datos adicionales no pueden exceder 1000 caracteres")]
    public string? DatosAdicionales { get; set; }
    
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    
    // Datos del pedido
    public int PedidoId { get; set; }
}

public class PagoIntentResumenDto
{
    public int Id { get; set; }
    public string IntentId { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public string Moneda { get; set; } = string.Empty;
    public EstadoPago Estado { get; set; }
    public string? MetodoPago { get; set; }
    public DateTime FechaCreacion { get; set; }
}

public class CreatePagoIntentDto
{
    [Required(ErrorMessage = "El monto es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
    public decimal Monto { get; set; }
    
    [MaxLength(3, ErrorMessage = "La moneda debe ser de 3 caracteres")]
    public string Moneda { get; set; } = "ARS";
    
    [MaxLength(50, ErrorMessage = "El método de pago no puede exceder 50 caracteres")]
    public string? MetodoPago { get; set; }
    
    [MaxLength(500, ErrorMessage = "La URL de confirmación no puede exceder 500 caracteres")]
    public string? UrlConfirmacion { get; set; }
    
    [MaxLength(1000, ErrorMessage = "Los datos adicionales no pueden exceder 1000 caracteres")]
    public string? DatosAdicionales { get; set; }
    
    [Required(ErrorMessage = "El pedido ID es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El pedido ID debe ser mayor a 0")]
    public int PedidoId { get; set; }
}

public class UpdatePagoIntentDto
{
    [Required(ErrorMessage = "El Intent ID es obligatorio")]
    [MaxLength(100, ErrorMessage = "El Intent ID no puede exceder 100 caracteres")]
    public string IntentId { get; set; } = string.Empty;
    
    public EstadoPago Estado { get; set; }
    
    [MaxLength(50, ErrorMessage = "El método de pago no puede exceder 50 caracteres")]
    public string? MetodoPago { get; set; }
    
    [MaxLength(1000, ErrorMessage = "Los datos adicionales no pueden exceder 1000 caracteres")]
    public string? DatosAdicionales { get; set; }
}
