using System.ComponentModel.DataAnnotations;

namespace farma_back.Models;

public class Receta
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string RutaArchivo { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(255)]
    public string NombreArchivoOriginal { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string TipoArchivo { get; set; } = string.Empty;
    
    public long TamanoArchivo { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    
    // Relación con Pedido
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;
}
