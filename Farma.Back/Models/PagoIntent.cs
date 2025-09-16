using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace farma_back.Models;

public enum EstadoPago
{
    PENDIENTE,
    PROCESANDO,
    COMPLETADO,
    FALLIDO,
    CANCELADO,
    REEMBOLSADO
}

public class PagoIntent
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string IntentId { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Monto { get; set; }
    
    [Required]
    [MaxLength(3)]
    public string Moneda { get; set; } = "ARS";
    
    [Required]
    public EstadoPago Estado { get; set; } = EstadoPago.PENDIENTE;
    
    [MaxLength(50)]
    public string? MetodoPago { get; set; }
    
    [MaxLength(500)]
    public string? UrlConfirmacion { get; set; }
    
    [MaxLength(1000)]
    public string? DatosAdicionales { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaActualizacion { get; set; }
    
    // Relación con Pedido
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;
}
