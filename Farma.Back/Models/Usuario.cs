using System.ComponentModel.DataAnnotations;

namespace farma_back.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string Telefono { get; set; } = string.Empty;
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    
    // Navegación
    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
