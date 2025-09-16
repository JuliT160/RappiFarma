using System.ComponentModel.DataAnnotations;

namespace Farma.Shared.DTOs;

public class RecetaDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "La ruta del archivo es obligatoria")]
    [MaxLength(500, ErrorMessage = "La ruta del archivo no puede exceder 500 caracteres")]
    public string RutaArchivo { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El nombre del archivo original es obligatorio")]
    [MaxLength(255, ErrorMessage = "El nombre del archivo no puede exceder 255 caracteres")]
    public string NombreArchivoOriginal { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El tipo de archivo es obligatorio")]
    [MaxLength(50, ErrorMessage = "El tipo de archivo no puede exceder 50 caracteres")]
    public string TipoArchivo { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El tamaño del archivo es obligatorio")]
    [Range(1, long.MaxValue, ErrorMessage = "El tamaño del archivo debe ser mayor a 0")]
    public long TamanoArchivo { get; set; }
    
    public DateTime FechaCreacion { get; set; }
    public int PedidoId { get; set; }
}

public class RecetaResumenDto
{
    public int Id { get; set; }
    public string NombreArchivoOriginal { get; set; } = string.Empty;
    public string TipoArchivo { get; set; } = string.Empty;
    public long TamanoArchivo { get; set; }
    public DateTime FechaCreacion { get; set; }
}

public class CreateRecetaRequestDto
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
    [Range(1, int.MaxValue, ErrorMessage = "El usuario ID debe ser mayor a 0")]
    public int UsuarioId { get; set; }
}
