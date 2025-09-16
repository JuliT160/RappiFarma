using System.ComponentModel.DataAnnotations;

namespace farma_back.DTOs;

public class RecetaRequestDto
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
    public int UsuarioId { get; set; }
}
