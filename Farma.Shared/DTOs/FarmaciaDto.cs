using System.ComponentModel.DataAnnotations;

namespace Farma.Shared.DTOs;

public class FarmaciaDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
    public string Nombre { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La dirección es obligatoria")]
    [MaxLength(300, ErrorMessage = "La dirección no puede exceder 300 caracteres")]
    public string Direccion { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La localidad es obligatoria")]
    [MaxLength(100, ErrorMessage = "La localidad no puede exceder 100 caracteres")]
    public string Localidad { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La provincia es obligatoria")]
    [MaxLength(100, ErrorMessage = "La provincia no puede exceder 100 caracteres")]
    public string Provincia { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El código postal es obligatorio")]
    [MaxLength(10, ErrorMessage = "El código postal no puede exceder 10 caracteres")]
    public string CodigoPostal { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "La latitud es obligatoria")]
    [Range(-90, 90, ErrorMessage = "La latitud debe estar entre -90 y 90")]
    public double Latitud { get; set; }
    
    [Required(ErrorMessage = "La longitud es obligatoria")]
    [Range(-180, 180, ErrorMessage = "La longitud debe estar entre -180 y 180")]
    public double Longitud { get; set; }
    
    [MaxLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
    public string? Telefono { get; set; }
    
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [MaxLength(255, ErrorMessage = "El email no puede exceder 255 caracteres")]
    public string? Email { get; set; }
    
    public bool Activa { get; set; } = true;
    public DateTime FechaCreacion { get; set; }
    
    // Propiedad calculada para búsquedas por distancia
    public double? DistanciaKm { get; set; }
}

public class FarmaciaResumenDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Localidad { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;
    public double? DistanciaKm { get; set; }
}
