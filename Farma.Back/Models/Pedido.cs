using System.ComponentModel.DataAnnotations;

namespace farma_back.Models;

public enum EstadoPedido
{
    CREADO,
    EN_COTIZACION,
    COTIZADO,
    CONFIRMADO,
    EN_PREPARACION,
    LISTO_PARA_ENTREGA,
    EN_ENTREGA,
    ENTREGADO,
    CANCELADO
}

public class Pedido
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public EstadoPedido Estado { get; set; } = EstadoPedido.CREADO;
    
    // Dirección de entrega
    [Required]
    [MaxLength(300)]
    public string Calle { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(10)]
    public string Altura { get; set; } = string.Empty;
    
    [MaxLength(10)]
    public string? Piso { get; set; }
    
    [MaxLength(10)]
    public string? Departamento { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Localidad { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Provincia { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(10)]
    public string CodigoPostal { get; set; } = string.Empty;
    
    public double? Latitud { get; set; }
    public double? Longitud { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaActualizacion { get; set; }
    
    // Relaciones
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    public ICollection<Receta> Recetas { get; set; } = new List<Receta>();
    public ICollection<Cotizacion> Cotizaciones { get; set; } = new List<Cotizacion>();
    public ICollection<PagoIntent> PagoIntents { get; set; } = new List<PagoIntent>();
}
