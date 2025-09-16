using System.ComponentModel.DataAnnotations;

namespace farma_back.Models;

public class Farmacia
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(300)]
    public string Direccion { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Localidad { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Provincia { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(10)]
    public string CodigoPostal { get; set; } = string.Empty;
    
    [Required]
    public double Latitud { get; set; }
    
    [Required]
    public double Longitud { get; set; }
    
    [MaxLength(20)]
    public string? Telefono { get; set; }
    
    [EmailAddress]
    [MaxLength(255)]
    public string? Email { get; set; }
    
    public bool Activa { get; set; } = true;
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    
    // Navegación
    public ICollection<Cotizacion> Cotizaciones { get; set; } = new List<Cotizacion>();
}
