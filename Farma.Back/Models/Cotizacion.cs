using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace farma_back.Models;

public enum EstadoCotizacion
{
    PENDIENTE,
    ENVIADA,
    ACEPTADA,
    RECHAZADA,
    EXPIRADA
}

public class Cotizacion
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal MontoTotal { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal MontoMedicamentos { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal MontoEnvio { get; set; }
    
    [Required]
    public EstadoCotizacion Estado { get; set; } = EstadoCotizacion.PENDIENTE;
    
    [MaxLength(1000)]
    public string? Observaciones { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaExpiracion { get; set; }
    
    // Relaciones
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;
    
    public int FarmaciaId { get; set; }
    public Farmacia Farmacia { get; set; } = null!;
}
